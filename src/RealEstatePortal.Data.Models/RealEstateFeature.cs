namespace RealEstatePortal.Data.Models
{
    public class RealEstateFeature
    {
        public Guid RealEstateId { get; set; }
        public virtual RealEstate RealEstate { get; set; } = null!;

        public int FeatureId { get; set; }
        public virtual Feature Feature { get; set; } = null!;
    }
}
