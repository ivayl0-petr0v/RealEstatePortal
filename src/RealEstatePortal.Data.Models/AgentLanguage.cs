namespace RealEstatePortal.Data.Models;

public class AgentLanguage
{
    public Guid AgentId { get; set; }
    public virtual Agent Agent { get; set; } = null!;

    public int LanguageId { get; set; }
    public virtual Language Language { get; set; } = null!;
}