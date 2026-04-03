namespace RealEstatePortal.Services.Core;

using Contracts;
using Data.Models;
using Data.Models.Enums;
using Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Web.ViewModels.RealEstate;
using Web.ViewModels.RealEstate.Common;

public class RealEstateService : IRealEstateService
{
    private readonly IBaseRepository baseRepository;

    public RealEstateService(IBaseRepository baseRepository)
    {
        this.baseRepository = baseRepository;
    }

    public async Task<IEnumerable<AllRealEstatesViewModel>> GetAllRealEstatesAsync()
    {
        var allRealEstates = await baseRepository
            .AllReadonly<RealEstate>()
            .Where(re => re.IsDeleted == false)
            .OrderByDescending(re => re.Id)
            .Select(re => new AllRealEstatesViewModel
            {
                Id = re.Id.ToString(),
                ImageUrl = re.RealEstateImages.Select(img => img.ImageUrl).FirstOrDefault() ?? "/images/default-property.jpg",
                Price = re.Price,
                Title = re.Category.Name,
                Area = re.Area,
                Address = $"{re.City.Name}, {re.Address}",
                RoomsCount = re.RoomsCount,
                BedroomsCount = re.BedroomsCount,
                BathroomsCount = re.BathroomsCount
            })
            .ToArrayAsync();

        return allRealEstates;
    }

    public async Task<IEnumerable<SelectListItemViewModel>> GetAllCategoriesAsync()
    {
        return await baseRepository
            .AllReadonly<Category>()
            .OrderBy(c => c.Name)
            .Select(c => new SelectListItemViewModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<SelectListItemViewModel>> GetAllCitiesAsync()
    {
        return await baseRepository
            .AllReadonly<City>()
            .OrderBy(c => c.Name)
            .Select(c => new SelectListItemViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<FeatureCheckboxViewModel>> GetAllFeaturesAsync()
    {
        return await baseRepository
            .AllReadonly<Feature>()
            .OrderBy(c => c.Name)
            .Select(f => new FeatureCheckboxViewModel
            {
                Id = f.Id,
                Name = f.Name
            })
            .ToListAsync();

    }

    public async Task<string> CreateRealEstateAsync(RealEstateFormModel model, string agentId, string imageFolderPath)
    {
        var realEstate = new RealEstate
        {
            Price = model.Price,
            Area = model.Area,
            TransactionType = model.TransactionType,
            Address = model.Address,
            RoomsCount = model.RoomsCount,
            BedroomsCount = model.BedroomsCount,
            BathroomsCount = model.BathroomsCount,
            ConstructionType = model.ConstructionType,
            ConstructionYear = model.ConstructionYear,
            CompletionStatus = model.CompletionStatus,
            Furnishing = model.Furnishing,
            TotalFloors = model.TotalFloors,
            RealEstateFloor = model.RealEstateFloor,
            Exposure = model.SelectedExposures.Any() ? string.Join(", ", model.SelectedExposures) : null,
            Description = model.Description,
            CategoryId = model.CategoryId,
            CityId = model.CityId,
            AgentId = Guid.Parse(agentId),
            Status = Status.Available
        };

        foreach (var featureId in model.SelectedFeatureIds)
        {
            realEstate.RealEstateFeatures.Add(new RealEstateFeature
            {
                FeatureId = featureId
            });
        }

        if (!Directory.Exists(imageFolderPath))
        {
            Directory.CreateDirectory(imageFolderPath);
        }

        if (model.Images != null && model.Images.Any())
        {
            foreach (var image in model.Images)
            {
                if (image.Length > 0)
                {
                    string extension = Path.GetExtension(image.FileName);
                    string uniqueFileName = $"{Guid.NewGuid()}{extension}";
                    string filePath = Path.Combine(imageFolderPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    realEstate.RealEstateImages.Add(new RealEstateImage
                    {
                        ImageUrl = $"/images/real-estates/{uniqueFileName}"
                    });
                }
            }
        }

        await baseRepository.AddAsync(realEstate);
        await baseRepository.SaveChangesAsync();

        return realEstate.Id.ToString();
    }

    public async Task<RealEstateDetailsViewModel?> GetDetailsByIdAsync(string id)
    {
        bool isValidGuid = Guid.TryParse(id, out Guid realEstateGuid);
        if (!isValidGuid) return null;

        return await baseRepository
            .AllReadonly<RealEstate>()
            .Where(re => re.Id == realEstateGuid && re.IsDeleted == false)
            .Select(re => new RealEstateDetailsViewModel
            {
                Id = re.Id.ToString(),
                Title = $"{re.Category.Name} in {re.City.Name}",
                Address = re.Address,
                Price = re.Price,
                Area = re.Area,
                TransactionType = re.TransactionType.ToString(),
                Description = re.Description,
                RoomsCount = re.RoomsCount,
                BedroomsCount = re.BedroomsCount,
                BathroomsCount = re.BathroomsCount,
                ConstructionType = re.ConstructionType,
                ConstructionYear = re.ConstructionYear,
                CompletionStatus = re.CompletionStatus,
                Furnishing = re.Furnishing,
                TotalFloors = re.TotalFloors,
                RealEstateFloor = re.RealEstateFloor,
                Exposure = re.Exposure,
                ImageUrls = re.RealEstateImages.Select(img => img.ImageUrl).ToList(),
                Features = re.RealEstateFeatures.Select(f => f.Feature.Name).ToList(),
                AgentId = re.AgentId.ToString(),
                AgentName = re.Agent.FullName,
                AgentPhoneNumber = re.Agent.PhoneNumber,
                AgentEmail = re.Agent.User.Email,
                AgentAvatarUrl = re.Agent.AvatarUrl
            })
            .FirstOrDefaultAsync();
    }
}
