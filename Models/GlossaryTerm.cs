using System.ComponentModel.DataAnnotations;

namespace Divination.Models;

public class GlossaryTerm
{
    [Key]
    public string Term { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}