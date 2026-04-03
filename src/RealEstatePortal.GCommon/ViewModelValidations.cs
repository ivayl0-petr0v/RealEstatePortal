namespace RealEstatePortal.GCommon;

public static class ViewModelValidations
{
    public static class BecomeAgentViewModel
    {
        public const int FullNameMinLength = 2;
        public const int FullNameMaxLength = 200;

        public const int AvatarUrlMaxLength = 2048;

        public const int AddressMinLength = 5;
        public const int AddressMaxLength = 300;

        public const int PhoneNumberMinLength = 6;
        public const int PhoneNumberMaxLength = 20;

        public const int DescriptionMinLength = 10;
        public const int DescriptionMaxLength = 3000;

        public const int WorkingDaysMaxLength = 100;

        public const int RestDaysMaxLength = 80;

        public const int FacebookUrlMaxLength = 200;
        public const int WebsiteUrlMaxLength = 256;
        public const int InstagramUrlMaxLength = 200;
        public const int WebsiteUrlMinLength = 10;

        public const int EmailMaxLength = 256;
    }

    public static class CreateRealEstateViewModel
    {
        public const string PriceMinValue = "1";
        public const string PriceMaxValue = "100000000";

        public const string AreaMinValue = "1";
        public const string AreaMaxValue = "100000";

        public const int AddressMinLength = 5;
        public const int AddressMaxLength = 200;

        public const int ConstructionTypeMinLength = 2;
        public const int ConstructionTypeMaxLength = 50;

        public const int CompletionStatusMinLength = 2;
        public const int CompletionStatusMaxLength = 50;

        public const int FurnishingMinLength = 2;
        public const int FurnishingMaxLength = 50;

        public const int DescriptionMinLength = 20;
        public const int DescriptionMaxLength = 4000;

        public const int MinConstructionYear = 1800;
        public const int MaxConstructionYear = 2100;

        public const int FloorMinValue = -2;
        public const int FloorMaxValue = 200;

        public const int RoomCountMinValue = 0;
        public const int RoomCountMaxValue = 50;
    }
}
