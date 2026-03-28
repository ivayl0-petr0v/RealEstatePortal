namespace RealEstatePortal.Data.Models
{
    using Enums;

    public class RealEstate
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public decimal Price { get; set; }

        public decimal Area { get; set; }

        public TransactionType TransactionType { get; set; }

        public string Address { get; set; } = null!;

        public int RoomsCount { get; set; }

        public int BedroomsCount { get; set; }

        public int BathroomsCount { get; set; }

        public string ConstructionType { get; set; } = null!;

        public int ConstructionYear { get; set; }

        public string CompletionStatus { get; set; } = null!;

        public string Furnishing { get; set; } = null!;

        public int TotalFloors { get; set; }

        public int RealEstateFloor { get; set; }

        public string? Exposure { get; set; }

        public string Description { get; set; } = null!;

        public Status Status { get; set; }

        // TODO: Add IsDeleted property for soft delete

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        public int CityId { get; set; }
        public virtual City City { get; set; } = null!;

        public Guid AgentId { get; set; }
        public virtual Agent Agent { get; set; } = null!;

        public virtual ICollection<RealEstateImage> RealEstateImages { get; set; }
            = new HashSet<RealEstateImage>();

        public virtual ICollection<RealEstateFeature> RealEstateFeatures { get; set; }
            = new HashSet<RealEstateFeature>();

        public virtual ICollection<Inquiry> Inquiries { get; set; }
            = new HashSet<Inquiry>();

        public virtual ICollection<UserFavoriteRealEstate> FavoriteRealEstates { get; set; }
            = new HashSet<UserFavoriteRealEstate>();
    }
}
