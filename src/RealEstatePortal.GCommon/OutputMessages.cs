namespace RealEstatePortal.GCommon;

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

        public const string AgentEditedSuccessfullyMessage = "Agent's profile updated successfully!";

        public const string EditAgentLogError = "Error while editing agent {0}";

        public const string EditAgentModelError = "An error occurred while editing the agent. Please try again.";

        public const string DeleteAgentLogError = "Error while deleting agent {0}";
        public const string DeleteAgentModelError = "An error occurred while deleting the agent. Please try again.";

        public const string AgentDeletedSuccessfullyMessage = "Your agent's profile deleted successfully";

        public const string DeleteAgentMissingPermission = "You don't have permission to delete this agent";

        public const string EditAgentMissingPermission = "You don't have permission to edit this agent";
    }

    public static class RealEstate
    {
        public const string PriceRequiredMessage = "Price is required.";
        public const string PriceRangeMessage = "Price must be between {1} and {2}.";

        public const string AreaRequiredMessage = "Area is required.";
        public const string AreaRangeMessage = "Area must be between {1} and {2}.";

        public const string TransactionTypeRequiredMessage = "Transaction type is required.";

        public const string AddressRequiredMessage = "Address is required.";
        public const string AddressLengthMessage = "Address must be between {2} and {1} characters long.";

        public const string ConstructionTypeRequiredMessage = "Construction type is required.";
        public const string ConstructionTypeLengthMessage = "Construction type must be between {2} and {1} characters long.";

        public const string ConstructionYearRangeMessage = "Construction year must be between {1} and {2}.";

        public const string CompletionStatusRequiredMessage = "Completion status is required.";
        public const string CompletionStatusLengthMessage = "Completion status must be between {2} and {1} characters long.";

        public const string FurnishingRequiredMessage = "Furnishing is required.";
        public const string FurnishingLengthMessage = "Furnishing must be between {2} and {1} characters long.";

        public const string DescriptionRequiredMessage = "Description is required.";
        public const string DescriptionLengthMessage = "Description must be between {2} and {1} characters long.";

        public const string CategoryRequiredMessage = "Please select a category.";
        public const string CityRequiredMessage = "Please select a city.";

        public const string FloorRangeMessage = "Floor must be a valid number.";
        public const string RoomCountRangeMessage = "Rooms count must be a valid number.";
        public const string BedroomCountRangeMessage = "Bedrooms count must be a valid number.";
        public const string BathroomCountRangeMessage = "Bathrooms count must be a valid number.";

        public const string UserWithoutAgentProfile = "You must be a registered agent to post a property listing.";
        public const string RealEstateCreatedSuccessfullyMessage = "Property listing created successfully.";
        public const string CreateRealEstateFailureMessage = "An error occurred while creating the property listing. Please try again.";
        public const string RealEstateNotFoundMessage = "Property not found.";
        public const string RealEstateUpdatedSuccessfullyMessage = "Property updated successfully.";
        public const string EditRealEstateLogError = "Error while editing property {0}";
        public const string EditRealEstateModelError = "An error occurred while editing the property. Please try again.";
        public const string DeleteRealEstateLogError = "Unexpected error occurred while deleting the property {0}";
        public const string DeleteRealEstateModelError = "An unexpected error occurred. Please try again later.";
    }

    public static class Admin
    {
        public const string RealEstateDeletedSuccessfullyByAdmin = "Property was successfully deleted by Administrator.";
    }
}
