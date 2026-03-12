namespace RealEstatePortal.Data.Models
{
    public class RealEstateFeature
    {
        public Guid RealEstateId { get; set; }
        public RealEstate RealEstate { get; set; } = null!;

        public int FeatureId { get; set; }
        public Feature Feature { get; set; } = null!;
    }
}
