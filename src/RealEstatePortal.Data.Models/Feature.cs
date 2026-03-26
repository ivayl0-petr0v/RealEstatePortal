namespace RealEstatePortal.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Feature
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<RealEstateFeature> RealEstateFeatures { get; set; }
            = new HashSet<RealEstateFeature>();
    }
}
