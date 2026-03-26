namespace RealEstatePortal.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<UserFavoriteRealEstate> FavoriteRealEstates { get; set; }
            = new HashSet<UserFavoriteRealEstate>();

        public virtual ICollection<Inquiry> Inquiries { get; set; }
            = new HashSet<Inquiry>();
    }
}