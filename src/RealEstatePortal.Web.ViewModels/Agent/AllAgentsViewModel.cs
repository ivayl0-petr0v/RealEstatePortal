namespace RealEstatePortal.Web.ViewModels.Agent
{
    public class AllAgentsViewModel
    {
        public string Id { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string? AvatarUrl { get; set; }
    }
}
