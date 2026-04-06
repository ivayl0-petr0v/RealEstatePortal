namespace RealEstatePortal.Services.Core.Contracts;

using RealEstatePortal.Web.ViewModels.Admin;
using RealEstatePortal.Web.ViewModels.RealEstate;
using RealEstatePortal.Web.ViewModels.RealEstate.Common;

public interface IRealEstateService
{
    Task<AllRealEstatesQueryModel> GetAllRealEstatesAsync(AllRealEstatesQueryModel queryModel, string? userId = null);

    Task<IEnumerable<SelectListItemViewModel>> GetAllCategoriesAsync();

    Task<IEnumerable<SelectListItemViewModel>> GetAllCitiesAsync();

    Task<IEnumerable<FeatureCheckboxViewModel>> GetAllFeaturesAsync();

    Task<string> CreateRealEstateAsync(RealEstateFormModel model, string agentId, string imageFolderPath);

    Task<RealEstateDetailsViewModel?> GetDetailsByIdAsync(string id, string? userId = null);

    Task<IEnumerable<AllRealEstatesViewModel>> GetTopThreeRealEstatesAsync();

    Task<RealEstateFormModel?> GetRealEstateForEditByIdAsync(string id);

    Task EditRealEstateAsync(string id, RealEstateFormModel model, string imageFolderPath);

    Task<RealEstateDeleteViewModel?> GetRealEstateForDeleteByIdAsync(string id);

    Task DeleteRealEstateAsync(string id);

    Task<bool> ExistsByIdAsync(string id);

    Task<bool> IsAgentIdOwnerOfRealEstateIdAsync(string realEstateId, string agentId);

    Task<IEnumerable<AdminRealEstateViewModel>> GetAllForAdminAsync();

    Task<bool> ToggleFavoriteAsync(string realEstateId, string userId);

    Task<bool> IsFavoriteAsync(string realEstateId, string userId);

    Task<IEnumerable<FavoriteRealEstateViewModel>> GetFavoritesByUserIdAsync(string userId);
}
