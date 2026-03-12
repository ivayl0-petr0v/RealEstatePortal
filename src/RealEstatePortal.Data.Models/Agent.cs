namespace RealEstatePortal.Data.Models
{
    using System;

    public class Agent
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string? AvatarUrl { get; set; }

        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string? WorkingDays { get; set; }

        public string? RestDays { get; set; }

        public TimeSpan? WorkingHoursStart { get; set; }

        public TimeSpan? WorkingHoursEnd { get; set; }

        public string Description { get; set; } = null!;

        public string? FacebookUrl { get; set; }

        public string? WebsiteUrl { get; set; }

        public string? InstagramUrl { get; set; }

        public ICollection<RealEstate> RealEstates { get; set; }
            = new HashSet<RealEstate>();

        public ICollection<AgentLanguage> SpokenLanguages { get; set; }
            = new HashSet<AgentLanguage>();
    }
}
