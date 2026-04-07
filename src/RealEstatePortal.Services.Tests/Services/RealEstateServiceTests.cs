using Microsoft.EntityFrameworkCore;
using RealEstatePortal.Data;
using RealEstatePortal.Data.Models;
using RealEstatePortal.Data.Models.Enums;
using RealEstatePortal.Data.Repository;
using RealEstatePortal.Data.Repository.Contracts;
using RealEstatePortal.Services.Core;

namespace RealEstatePortal.Services.Tests.Services;

[TestFixture]
public class RealEstateServiceTests
{
    private RealEstateDbContext dbContext;
    private IBaseRepository repository;
    private RealEstateService realEstateService;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<RealEstateDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        dbContext = new RealEstateDbContext(options);

        repository = new BaseRepository(dbContext);

        realEstateService = new RealEstateService(repository);
    }

    [TearDown]
    public void TearDown()
    {
        if (repository is IDisposable disposableRepo)
        {
            disposableRepo.Dispose();
        }

        if (dbContext != null)
        {
            dbContext.Dispose();
        }

        repository = null!;
        dbContext = null!;
        realEstateService = null!;
    }

    [Test]
    public async Task ToggleFavoriteAsync_ShouldSuccessfullyAddFavorite()
    {
        var userId = "user123";
        var propertyId = Guid.NewGuid();

        var category = new Category { Name = "Apartment" };
        var city = new City { Name = "Sofia" };

        var agent = new Agent
        {
            Id = Guid.NewGuid(),
            UserId = "some-guid-string",
            FullName = "Ivan",
            Address = "Test Address",
            Description = "Test Description",
            PhoneNumber = "0888111222"
        };

        var property = new RealEstate
        {
            Id = propertyId,
            Price = 100000,
            Area = 75,
            TransactionType = TransactionType.Sale,
            Address = "Test Street 1",
            RoomsCount = 2,
            BedroomsCount = 1,
            BathroomsCount = 1,
            ConstructionType = "Brick",
            ConstructionYear = 2020,
            CompletionStatus = "Act 16",
            Furnishing = "Fully Furnished",
            TotalFloors = 5,
            RealEstateFloor = 3,
            Description = "This is a very long test description for the property...",
            IsDeleted = false,
            Category = category,
            City = city,
            Agent = agent
        };

        await dbContext.Agents.AddAsync(agent);
        await dbContext.RealEstates.AddAsync(property);
        await dbContext.SaveChangesAsync();

        var result = await realEstateService.ToggleFavoriteAsync(propertyId.ToString(), userId);

        Assert.IsTrue(result);
        var count = await dbContext.UserFavoriteRealEstates.CountAsync();
        Assert.That(count, Is.EqualTo(1));
    }

    [Test]
    public async Task ToggleFavoriteAsync_ShouldRemoveFavorite_WhenAlreadyExists()
    {
        var userId = "user123";
        var propertyId = Guid.NewGuid();

        var agent = new Agent { UserId = "agent-id", FullName = "Ivan", Address = "A", Description = "D", PhoneNumber = "08" };
        var property = new RealEstate { Id = propertyId, Agent = agent, Address = "A", Description = "D", Price = 10, ConstructionType = "B", CompletionStatus = "A", Furnishing = "F", Area = 1, TransactionType = TransactionType.Sale, Status = Status.Available };

        var favorite = new UserFavoriteRealEstate { UserId = userId, RealEstateId = propertyId };

        await dbContext.Agents.AddAsync(agent);
        await dbContext.RealEstates.AddAsync(property);
        await dbContext.UserFavoriteRealEstates.AddAsync(favorite);
        await dbContext.SaveChangesAsync();

        var result = await realEstateService.ToggleFavoriteAsync(propertyId.ToString(), userId);

        Assert.IsFalse(result);

        var count = await dbContext.UserFavoriteRealEstates.CountAsync();
        Assert.AreEqual(0, count);
    }

    [Test]
    public async Task ToggleFavoriteAsync_ShouldReturnFalse_WhenUserIsOwner()
    {
        var userId = "same-user-id";
        var propertyId = Guid.NewGuid();

        var agent = new Agent { UserId = userId, FullName = "Ivan", Address = "A", Description = "D", PhoneNumber = "08" };
        var property = new RealEstate { Id = propertyId, Agent = agent, Address = "A", Description = "D", Price = 10, ConstructionType = "B", CompletionStatus = "A", Furnishing = "F", Area = 1, TransactionType = TransactionType.Sale, Status = Status.Available };

        await dbContext.Agents.AddAsync(agent);
        await dbContext.RealEstates.AddAsync(property);
        await dbContext.SaveChangesAsync();

        var result = await realEstateService.ToggleFavoriteAsync(propertyId.ToString(), userId);

        Assert.IsFalse(result, "Собственикът не трябва да може да добавя имота в любими.");

        var count = await dbContext.UserFavoriteRealEstates.CountAsync();
        Assert.That(count, Is.EqualTo(0), "Не трябва да се добавя запис в базата.");
    }

    [Test]
    public async Task ToggleFavoriteAsync_ShouldReturnFalse_WhenPropertyDoesNotExist()
    {
        var userId = "user123";
        var nonExistentId = Guid.NewGuid().ToString();

        var result = await realEstateService.ToggleFavoriteAsync(nonExistentId, userId);

        Assert.IsFalse(result, "Методът трябва да върне false за несъществуващ имот.");
    }
}