namespace RealEstatePortal.Services.Core.Contracts;

using RealEstatePortal.Web.ViewModels.RealEstate;
using RealEstatePortal.Web.ViewModels.RealEstate.Common;

public interface IRealEstateService
{
    Task<IEnumerable<AllRealEstatesViewModel>> GetAllRealEstatesAsync();

    Task<IEnumerable<SelectListItemViewModel>> GetAllCategoriesAsync();

    Task<IEnumerable<SelectListItemViewModel>> GetAllCitiesAsync();

    Task<IEnumerable<FeatureCheckboxViewModel>> GetAllFeaturesAsync();

    Task<string> CreateRealEstateAsync(RealEstateFormModel model, string agentId, string imageFolderPath);

    Task<RealEstateDetailsViewModel?> GetDetailsByIdAsync(string id);
}
