namespace RealEstatePortal.Web.ViewModels.Agent
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static GCommon.ViewModelValidations.BecomeAgentViewModel;
    using static GCommon.OutputMessages.Agent;
    public class AgentFormModel
    {
        [Url]
        [MaxLength(AvatarUrlMaxLength, ErrorMessage = AvatarUrlMaxLengthMessage)]
        public string? AvatarUrl { get; set; }

        [Required(ErrorMessage = FullNameRequiredMessage)]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength, ErrorMessage = FullNameLengthMessage)]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = AddressRequiredMessage)]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength, ErrorMessage = AddressLengthMessage)]
        public string Address { get; set; } = null!;

        [StringLength(EmailMaxLength, ErrorMessage = EmailMaxLengthMessage)]
        [RegularExpression(EmailRegEx, ErrorMessage = EmailInvalidMessage)]
        public string? Email { get; set; }

        [Required(ErrorMessage = DescriptionRequiredMessage)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = DescriptionLengthMessage)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = PhoneNumberRequiredMessage)]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength, ErrorMessage = PhoneNumberLengthMessage)]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(WorkingDaysMaxLength, ErrorMessage = WorkDaysLengthMessage)]
        public string? WorkingDaysStart { get; set; }

        [StringLength(WorkingDaysMaxLength, ErrorMessage = WorkDaysLengthMessage)]
        public string? WorkingDaysEnd { get; set; }

        //[Range(typeof(TimeSpan), WorkHoursStart, WorkHoursEnd, ErrorMessage = WorkHoursMessage)]
        public TimeSpan? WorkingHoursStart { get; set; }

        //[Range(typeof(TimeSpan), WorkHoursStart, WorkHoursEnd, ErrorMessage = WorkHoursMessage)]
        public TimeSpan? WorkingHoursEnd { get; set; }

        [StringLength(RestDaysMaxLength, ErrorMessage = RestDaysLengthMessage)]
        public string? RestDays { get; set; }

        [Url]
        [StringLength(FacebookUrlMaxLength, ErrorMessage = FacebookUrlMaxLengthMessage)]
        public string? FacebookUrl { get; set; }

        [Url]
        [StringLength(WebsiteUrlMaxLength, ErrorMessage = WebsiteUrlMaxLengthMessage)]
        public string? WebsiteUrl { get; set; }

        [Url]
        [StringLength(InstagramUrlMaxLength, ErrorMessage = InstagramUrlMaxLengthMessage)]
        public string? InstagramUrl { get; set; }

        public string? SpokenLanguages { get; set; }
    }
}
