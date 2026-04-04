using RealEstatePortal.Web.ViewModels.Agent;
using RealEstatePortal.Web.ViewModels.RealEstate;

namespace RealEstatePortal.Web.ViewModels.Home;

public class HomeViewModel
{
    public IEnumerable<AllRealEstatesViewModel> RealEstates { get; set; }
        = new List<AllRealEstatesViewModel>();

    public IEnumerable<AllAgentsViewModel> Agents { get; set; }
        = new List<AllAgentsViewModel>();
}
