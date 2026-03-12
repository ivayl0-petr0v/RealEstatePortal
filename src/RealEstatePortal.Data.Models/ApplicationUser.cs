namespace RealEstatePortal.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ICollection<UserFavoriteRealEstate> FavoriteRealEstates { get; set; }
            = new HashSet<UserFavoriteRealEstate>();

        public ICollection<Inquiry> Inquiries { get; set; }
            = new HashSet<Inquiry>();
    }
}