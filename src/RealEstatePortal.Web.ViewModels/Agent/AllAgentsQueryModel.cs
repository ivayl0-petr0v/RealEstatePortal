namespace RealEstatePortal.Web.ViewModels.Agent;

public class AllAgentsQueryModel
{
    public IEnumerable<AllAgentsViewModel> Agents { get; set; }
        = new List<AllAgentsViewModel>();

    public int CurrentPage { get; set; } = 1;

    public int AgentsPerPage { get; set; } = 6;

    public int TotalAgentsCount { get; set; }
}
