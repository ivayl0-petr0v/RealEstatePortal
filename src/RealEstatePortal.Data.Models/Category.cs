namespace RealEstatePortal.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<RealEstate> RealEstates { get; set; }
            = new HashSet<RealEstate>();
    }
}
