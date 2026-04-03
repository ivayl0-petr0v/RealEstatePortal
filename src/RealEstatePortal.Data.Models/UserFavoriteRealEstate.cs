namespace RealEstatePortal.Data.Models;

using System;
public class UserFavoriteRealEstate
{
    public string UserId { get; set; } = null!;
    public virtual ApplicationUser User { get; set; } = null!;

    public Guid RealEstateId { get; set; }
    public virtual RealEstate RealEstate { get; set; } = null!;

    //TODO:  Add IsDeleted property to allow users to remove real estate from favorites without deleting the record from the database
}
