namespace RealEstatePortal.GCommon
{
    public static class OutputMessages
    {
        public static class Agent
        {
            public const string AvatarUrlMaxLengthMessage = "Avatar URL cannot exceed {1} characters.";

            public const string FullNameRequiredMessage = "Full name is required.";
            public const string FullNameLengthMessage = "Full name must be between {2} and {1} characters.";

            public const string AddressRequiredMessage = "Address is required.";
            public const string AddressLengthMessage = "Address must be between {2} and {1} characters.";

            public const string EmailMaxLengthMessage = "Email cannot exceed {1} characters.";
            public const string EmailRegEx = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            public const string EmailInvalidMessage = "Please enter a valid email address.";

            public const string DescriptionRequiredMessage = "Description is required.";
            public const string DescriptionLengthMessage = "Description must be between {2} and {1} characters.";

            public const string PhoneNumberRequiredMessage = "Phone number is required.";
            public const string PhoneNumberLengthMessage = "Phone number must be between {2} and {1} characters.";

            public const string WorkDaysLengthMessage = "Working days information cannot exceed {1} characters.";

            public const string WorkHoursStart = "00:00:00";
            public const string WorkHoursEnd = "23:59:59";
            public const string WorkHoursMessage = "Working hours must be between {0} and {1}.";

            public const string RestDaysLengthMessage = "Rest days information cannot exceed {1} characters.";

            public const string FacebookUrlMaxLengthMessage = "Facebook URL cannot exceed {1} characters.";

            public const string WebsiteUrlMaxLengthMessage = "Website URL cannot exceed {1} characters.";

            public const string InstagramUrlMaxLengthMessage = "Instagram URL cannot exceed {1} characters.";

            public const string CreateAgentFailureMessage = "An error occurred while creating the agent. Please try again.";

            public const string AgentAlreadyExistsMessage = "You are already registered as an agent.";

            public const string AgentPhoneNumberExistsMessage = "The phone number is already in use by another agent.";

            public const string AgentCreatedSuccessfullyMessage = "You have successfully become an agent.";
        }
    }
}
