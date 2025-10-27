using BasedLibrary.Constants;
using System.ComponentModel.DataAnnotations;

namespace BasedLibrary.Entities.BaseEntitys;
/// <summary>
/// Represents the base entity for all domain models.
/// Provides a unique identifier and a common Name property.
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or set the name associated with the entity.
    /// </summary>
    [Required(ErrorMessage = ValidationMessages.NameRequired)]
    [StringLength(100, ErrorMessage = ValidationMessages.NameLengthExceeded)]
    public string Name { get; set; } = string.Empty;
}
