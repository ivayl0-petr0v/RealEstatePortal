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

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int CityId { get; set; }
        public City City { get; set; } = null!;

        public Guid AgentId { get; set; }
        public Agent Agent { get; set; } = null!;

        public ICollection<RealEstateImage> RealEstateImages { get; set; }
            = new HashSet<RealEstateImage>();

        public ICollection<RealEstateFeature> RealEstateFeatures { get; set; }
            = new HashSet<RealEstateFeature>();

        public ICollection<Inquiry> Inquiries { get; set; }
            = new HashSet<Inquiry>();

        public ICollection<UserFavoriteRealEstate> FavoriteRealEstates { get; set; }
            = new HashSet<UserFavoriteRealEstate>();
    }
}
