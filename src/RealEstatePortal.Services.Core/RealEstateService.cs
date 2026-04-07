namespace RealEstatePortal.Services.Core;

using Contracts;
using Data.Models;
using Data.Models.Enums;
using Data.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstatePortal.Web.ViewModels.Admin;
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

    public async Task<AllRealEstatesQueryModel> GetAllRealEstatesAsync(AllRealEstatesQueryModel queryModel, string? userId = null)
    {
        var query = baseRepository
            .AllReadonly<RealEstate>()
            .Where(re => re.IsDeleted == false);

        if (!string.IsNullOrWhiteSpace(queryModel.TransactionType))
        {
            if (Enum.TryParse<TransactionType>(queryModel.TransactionType, out TransactionType parsedTransactionType))
            {
                query = query
                    .Where(re => re.TransactionType == parsedTransactionType);
            }
        }

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";
            query = query
                .Where(re => EF.Functions.Like(re.City.Name.ToLower(), wildCard) ||
                             EF.Functions.Like(re.Address.ToLower(), wildCard) ||
                             EF.Functions.Like(re.Category.Name.ToLower(), wildCard));
        }

        query = queryModel.Sorting switch
        {
            "price_asc" => query.OrderBy(re => re.Price),
            "price_desc" => query.OrderByDescending(re => re.Price),
            _ => query.OrderByDescending(re => re.Id)
        };

        queryModel.TotalRealEstatesCount = await query
            .CountAsync();

        queryModel.RealEstates = await query
            .Skip((queryModel.CurrentPage - 1) * queryModel.RealEstatesPerPage)
            .Take(queryModel.RealEstatesPerPage)
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
                BathroomsCount = re.BathroomsCount,
                IsFavorite = userId != null &&
                         re.FavoriteRealEstates.Any(f => f.UserId == userId)
            })
            .ToListAsync();

        return queryModel;
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

        await SaveImagesToRealEstateAsync(model.Images, imageFolderPath, realEstate);

        await baseRepository.AddAsync(realEstate);
        await baseRepository.SaveChangesAsync();

        return realEstate.Id.ToString();
    }

    public async Task<RealEstateDetailsViewModel?> GetDetailsByIdAsync(string id, string? userId = null)
    {
        bool isValidGuid = Guid.TryParse(id, out Guid realEstateGuid);
        if (!isValidGuid) return null;

        return await baseRepository
            .AllReadonly<RealEstate>()
            .Where(re => re.Id == realEstateGuid && re.IsDeleted == false)
            .Include(re => re.RealEstateImages)
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
                AgentAvatarUrl = re.Agent.AvatarUrl,
                IsFavorite = userId != null &&
                         re.FavoriteRealEstates.Any(f => f.UserId == userId)
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<AllRealEstatesViewModel>> GetTopThreeRealEstatesAsync(string? currentUserId = null)
    {
        return await baseRepository
            .AllReadonly<RealEstate>()
        .Where(r => r.IsDeleted == false)
        .OrderByDescending(r => r.Id)
        .Take(3)
        .Select(r => new AllRealEstatesViewModel
        {
            Id = r.Id.ToString(),
            ImageUrl = r.RealEstateImages
                .Select(img => img.ImageUrl)
                .FirstOrDefault() ?? "/images/default-property.jpg",
            Price = r.Price,
            Title = r.Category.Name,
            Area = r.Area,
            Address = $"{r.City.Name}, {r.Address}",
            RoomsCount = r.RoomsCount,
            BedroomsCount = r.BedroomsCount,
            BathroomsCount = r.BathroomsCount,
            IsFavorite = currentUserId != null && r.FavoriteRealEstates.Any(f => f.UserId == currentUserId)
        })
        .ToListAsync();
    }

    public async Task<RealEstateFormModel?> GetRealEstateForEditByIdAsync(string id)
    {
        var realEstate = await baseRepository
            .AllReadonly<RealEstate>()
            .Include(re => re.RealEstateImages)
            .Include(re => re.RealEstateFeatures)
            .Where(re => re.Id.ToString() == id && !re.IsDeleted)
            .FirstOrDefaultAsync();

        if (realEstate == null) return null;

        return new RealEstateFormModel
        {
            Price = realEstate.Price,
            Area = realEstate.Area,
            TransactionType = realEstate.TransactionType,
            Address = realEstate.Address,
            RoomsCount = realEstate.RoomsCount,
            BedroomsCount = realEstate.BedroomsCount,
            BathroomsCount = realEstate.BathroomsCount,
            ConstructionType = realEstate.ConstructionType,
            ConstructionYear = realEstate.ConstructionYear,
            CompletionStatus = realEstate.CompletionStatus,
            Furnishing = realEstate.Furnishing,
            TotalFloors = realEstate.TotalFloors,
            RealEstateFloor = realEstate.RealEstateFloor,
            Description = realEstate.Description,
            CategoryId = realEstate.CategoryId,
            CityId = realEstate.CityId,
            SelectedExposures = realEstate
                .Exposure?
                .Split(", ")
                .ToList() ?? new List<string>(),
            ExistingImageUrls = realEstate
                .RealEstateImages
                .Select(img => img.ImageUrl)
                .ToList(),
            SelectedFeatureIds = realEstate
                .RealEstateFeatures
                .Select(rf => rf.FeatureId)
                .ToList()
        };
    }

    public async Task EditRealEstateAsync(string id, RealEstateFormModel model, string imageFolderPath)
    {
        var realEstate = await baseRepository
            .All<RealEstate>()
            .Where(re => re.Id.ToString() == id && !re.IsDeleted)
            .Include(re => re.RealEstateImages)
            .Include(re => re.RealEstateFeatures)
            .FirstOrDefaultAsync();

        if (realEstate != null)
        {
            realEstate.Price = model.Price;
            realEstate.Area = model.Area;
            realEstate.TransactionType = model.TransactionType;
            realEstate.Address = model.Address;
            realEstate.RoomsCount = model.RoomsCount;
            realEstate.BedroomsCount = model.BedroomsCount;
            realEstate.BathroomsCount = model.BathroomsCount;
            realEstate.ConstructionType = model.ConstructionType;
            realEstate.ConstructionYear = model.ConstructionYear;
            realEstate.CompletionStatus = model.CompletionStatus;
            realEstate.Furnishing = model.Furnishing;
            realEstate.TotalFloors = model.TotalFloors;
            realEstate.RealEstateFloor = model.RealEstateFloor;
            realEstate.Description = model.Description;
            realEstate.CategoryId = model.CategoryId;
            realEstate.CityId = model.CityId;
            realEstate.Exposure = model
                .SelectedExposures
                .Any() ? string
                .Join(", ", model.SelectedExposures) : null;

            realEstate.RealEstateFeatures.Clear();
            foreach (var featureId in model.SelectedFeatureIds)
            {
                realEstate.RealEstateFeatures.Add(new RealEstateFeature { FeatureId = featureId });
            }

            if (!Directory.Exists(imageFolderPath))
            {
                Directory.CreateDirectory(imageFolderPath);
            }

            DeleteRemovedImages(model.RemovedImagesUrls, realEstate, imageFolderPath);

            await SaveImagesToRealEstateAsync(model.Images, imageFolderPath, realEstate);

            await baseRepository.SaveChangesAsync();
        }
    }

    public async Task<RealEstateDeleteViewModel?> GetRealEstateForDeleteByIdAsync(string id)
    {
        return await baseRepository
            .AllReadonly<RealEstate>()
            .Where(re => re.Id.ToString() == id && !re.IsDeleted)
            .Select(re => new RealEstateDeleteViewModel
            {
                Id = re.Id.ToString(),
                Title = re.Category.Name,
                Address = $"{re.City.Name}, {re.Address}",
                Price = re.Price,
                ImageUrl = re.RealEstateImages.Select(i => i.ImageUrl).FirstOrDefault() ?? "/images/default-property.jpg"
            })
            .FirstOrDefaultAsync();
    }

    public async Task DeleteRealEstateAsync(string id)
    {
        var realEstate = await baseRepository
            .All<RealEstate>()
            .Where(re => re.Id.ToString() == id)
            .FirstOrDefaultAsync();

        if (realEstate != null)
        {
            realEstate.IsDeleted = true;
            await baseRepository.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByIdAsync(string id)
    {
        return await baseRepository
            .AllReadonly<RealEstate>()
            .AnyAsync(re => re.Id.ToString() == id && re.IsDeleted == false);
    }

    public async Task<bool> IsAgentIdOwnerOfRealEstateIdAsync(string realEstateId, string agentId)
    {
        return await baseRepository
            .AllReadonly<RealEstate>()
            .AnyAsync(re => re.Id.ToString() == realEstateId && re.AgentId.ToString() == agentId);
    }

    public async Task<IEnumerable<AdminRealEstateViewModel>> GetAllForAdminAsync()
    {
        return await baseRepository.AllReadonly<RealEstate>()
        .Where(re => !re.IsDeleted)
        .Select(re => new AdminRealEstateViewModel
        {
            Id = re.Id.ToString(),
            Title = re.Category.Name,
            Price = re.Price,
            Address = re.City.Name + ", " + re.Address,
            AgentName = re.Agent.FullName,
            AgentEmail = re.Agent.User.Email
        })
        .ToListAsync();
    }

    public async Task<bool> ToggleFavoriteAsync(string realEstateId, string userId)
    {
        var property = await baseRepository
            .AllReadonly<RealEstate>()
            .Include(re => re.Agent)
            .FirstOrDefaultAsync(re => re.Id.ToString() == realEstateId);

        if (property == null)
        {
            return false;
        }

        if (property.Agent.UserId == userId)
        {
            return false;
        }

        var favorite = await baseRepository
            .All<UserFavoriteRealEstate>()
            .FirstOrDefaultAsync(f => f.RealEstateId.ToString() == realEstateId && f.UserId == userId);

        if (favorite == null)
        {
            
            await baseRepository.AddAsync(new UserFavoriteRealEstate
            {
                RealEstateId = Guid.Parse(realEstateId),
                UserId = userId
            });
            await baseRepository.SaveChangesAsync();
            return true;
        }
        else
        {
            baseRepository.Delete(favorite);
            await baseRepository.SaveChangesAsync();
            return false;
        }
    }

    public async Task<bool> IsFavoriteAsync(string realEstateId, string userId)
    {
        return await baseRepository
            .AllReadonly<UserFavoriteRealEstate>()
            .AnyAsync(f => f.RealEstateId.ToString() == realEstateId && f.UserId == userId);
    }

    public async Task<IEnumerable<FavoriteRealEstateViewModel>> GetFavoritesByUserIdAsync(string userId)
    {
        return await baseRepository.AllReadonly<UserFavoriteRealEstate>()
        .Where(f => f.UserId == userId && !f.RealEstate.IsDeleted)
        .Select(f => new FavoriteRealEstateViewModel
        {
            Id = f.RealEstateId.ToString(),
            Title = f.RealEstate.Category.Name,
            Price = f.RealEstate.Price,
            ImageUrl = f.RealEstate.RealEstateImages
                .OrderBy(img => img.Id)
                .Select(img => img.ImageUrl)
                .FirstOrDefault() ?? "/images/default-property.jpg",
            Address = f.RealEstate.Address,
            Area = f.RealEstate.Area
        })
        .ToListAsync();
    }

    private async Task SaveImagesToRealEstateAsync(IEnumerable<IFormFile>? images, string imageFolderPath, RealEstate realEstate)
    {
        if (images == null || !images.Any())
        {
            return;
        }

        if (!Directory.Exists(imageFolderPath))
        {
            Directory.CreateDirectory(imageFolderPath);
        }

        foreach (var image in images)
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

    private void DeleteRemovedImages(IEnumerable<string>? removedImagesUrls, RealEstate realEstate, string imageFolderPath)
    {
        if (removedImagesUrls == null || !removedImagesUrls.Any())
        {
            return;
        }

        var imagesToRemove = realEstate.RealEstateImages
            .Where(img => removedImagesUrls.Contains(img.ImageUrl))
            .ToList();

        foreach (var img in imagesToRemove)
        {
            string fileName = Path.GetFileName(img.ImageUrl);
            string physicalPath = Path.Combine(imageFolderPath, fileName);

            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
            }

            realEstate.RealEstateImages.Remove(img);

            baseRepository.Delete(img);
        }
    }
}
