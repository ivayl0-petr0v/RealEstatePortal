namespace RealEstatePortal.Data.Models
{
    public class AgentLanguage
    {
        public Guid AgentId { get; set; }
        public Agent Agent { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}