namespace RealEstatePortal.Data.Common
{
    public static class EntityValidation
    {
        public static class RealEstate
        {
            public const string PriceColumnType = "DECIMAL(18,2)";
            public const string AreaColumnType = "DECIMAL(10,2)";
            public const int AddressMaxLength = 300;
            public const int ConstructionTypeMaxLength = 50;
            public const int CompletionStatusMaxLength = 80;
            public const int FurnishingMaxLength = 80;
            public const int ExposureMaxLength = 100;
            public const int DescriptionMaxLength = 6000;
        }

        public static class RealEstateImage
        {
            public const int ImageUrlMaxLength = 2048;
        }

        public static class Agent
        {
            public const int FullNameMaxLength = 200;
            public const int AvatarUrlMaxLength = 2048;
            public const int AddressMaxLength = 300;
            public const int PhoneNumberMaxLength = 20;
            public const int WorkingDaysMaxLength = 100;
            public const int RestDaysMaxLength = 80;
            public const int DescriptionMaxLength = 3000;
            public const int FacebookUrlMaxLength = 200;
            public const int WebsiteUrlMaxLength = 256;
            public const int InstagramUrlMaxLength = 200;
        }

        public static class Category
        {
            public const int NameMaxLength = 100;
        }

        public static class City
        {
            public const int NameMaxLength = 100;
        }

        public static class Feature
        {
            public const int NameMaxLength = 200;
        }

        public static class Language
        {
            public const int NameMaxLength = 100;
        }

        public static class Inquiry
        {
            public const int SenderNameMaxLength = 100;
            public const int SenderEmailMaxLength = 100;
            public const int SenderPhoneMaxLength = 20;
            public const int MessageMaxLength = 4000;
        }
    }
}
