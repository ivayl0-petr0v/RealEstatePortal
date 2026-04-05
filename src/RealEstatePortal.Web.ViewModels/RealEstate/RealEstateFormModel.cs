namespace RealEstatePortal.Web.ViewModels.RealEstate;

using Common;
using Data.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using static GCommon.OutputMessages.RealEstate;
using static GCommon.ViewModelValidations.CreateRealEstateViewModel;

public class RealEstateFormModel
{
    [Required(ErrorMessage = PriceRequiredMessage)]
    [Range(typeof(decimal), PriceMinValue, PriceMaxValue, ErrorMessage = PriceRangeMessage)]
    public decimal Price { get; set; }

    [Required(ErrorMessage = PriceRequiredMessage)]
    [Range(typeof(decimal), AreaMinValue, AreaMaxValue, ErrorMessage = AreaRangeMessage)]
    public decimal Area { get; set; }

    [Required(ErrorMessage = TransactionTypeRequiredMessage)]
    public TransactionType TransactionType { get; set; }

    [Required(ErrorMessage = AddressRequiredMessage)]
    [StringLength(AddressMaxLength, MinimumLength = AddressMinLength, ErrorMessage = AddressLengthMessage)]
    public string Address { get; set; } = null!;

    [Range(RoomCountMinValue, RoomCountMaxValue, ErrorMessage = RoomCountRangeMessage)]
    public int RoomsCount { get; set; }

    [Range(RoomCountMinValue, RoomCountMaxValue, ErrorMessage = BedroomCountRangeMessage)]
    public int BedroomsCount { get; set; }

    [Range(RoomCountMinValue, RoomCountMaxValue, ErrorMessage = BathroomCountRangeMessage)]
    public int BathroomsCount { get; set; }

    [Required(ErrorMessage = ConstructionTypeRequiredMessage)]
    [StringLength(ConstructionTypeMaxLength, MinimumLength = ConstructionTypeMinLength, ErrorMessage = ConstructionTypeLengthMessage)]
    public string ConstructionType { get; set; } = null!;

    [Range(MinConstructionYear, MaxConstructionYear, ErrorMessage = ConstructionYearRangeMessage)]
    public int ConstructionYear { get; set; }

    [Required(ErrorMessage = CompletionStatusRequiredMessage)]
    [StringLength(CompletionStatusMaxLength, MinimumLength = CompletionStatusMinLength, ErrorMessage = CompletionStatusLengthMessage)]
    public string CompletionStatus { get; set; } = null!;

    [StringLength(FurnishingMaxLength, MinimumLength = FurnishingMinLength, ErrorMessage = FurnishingLengthMessage)]
    public string Furnishing { get; set; } = null!;

    [Range(FloorMinValue, FloorMaxValue, ErrorMessage = FloorRangeMessage)]
    public int TotalFloors { get; set; }

    [Range(FloorMinValue, FloorMaxValue, ErrorMessage = FloorRangeMessage)]
    public int RealEstateFloor { get; set; }

    [Required(ErrorMessage = DescriptionRequiredMessage)]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = DescriptionLengthMessage)]
    public string Description { get; set; } = null!;

    public List<string> SelectedExposures { get; set; } = new List<string>();

    [Required(ErrorMessage = CategoryRequiredMessage)]
    public int CategoryId { get; set; }
    public IEnumerable<SelectListItemViewModel> Categories { get; set; } = new List<SelectListItemViewModel>();

    [Required(ErrorMessage = CityRequiredMessage)]
    public int CityId { get; set; }
    public IEnumerable<SelectListItemViewModel> Cities { get; set; } = new List<SelectListItemViewModel>();

    public List<int> SelectedFeatureIds { get; set; } = new List<int>();
    public IEnumerable<FeatureCheckboxViewModel> Features { get; set; } = new List<FeatureCheckboxViewModel>();

    public IEnumerable<IFormFile> Images { get; set; } = new List<IFormFile>();

    public List<string> ExistingImageUrls { get; set; } = new List<string>();

    public List<string> RemovedImagesUrls { get; set; } = new List<string>();
}
