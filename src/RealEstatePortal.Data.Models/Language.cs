namespace RealEstatePortal.Data.Models;

using System.Collections.Generic;

public class Language
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<AgentLanguage> AgentLanguages { get; set; }
        = new HashSet<AgentLanguage>();
}
