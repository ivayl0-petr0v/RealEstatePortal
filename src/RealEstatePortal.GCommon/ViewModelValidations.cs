namespace RealEstatePortal.GCommon
{
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
    }
}
