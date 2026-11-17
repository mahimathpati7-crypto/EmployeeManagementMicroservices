using System.ComponentModel.DataAnnotations;

namespace Member.Models;

public class MemberRequest : IValidatableObject
{
    [Required, StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [RegularExpression(@"^\+?[0-9 ()-]{7,15}$", ErrorMessage = "PhoneNumber must contain 7 to 15 valid phone characters.")]
    public string? PhoneNumber { get; set; }

    [StringLength(250)]
    public string? Address { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(100)]
    public string? State { get; set; }

    [StringLength(100)]
    public string? Country { get; set; }

    [Required]
    [RegularExpression("^(Basic|Standard|Premium)$", ErrorMessage = "MembershipType must be Basic, Standard, or Premium.")]
    public string MembershipType { get; set; } = string.Empty;

    [Required]
    public DateTime? MembershipStartDate { get; set; }

    public DateTime? MembershipEndDate { get; set; }

    public bool IsActive { get; set; } = true;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MembershipEndDate.HasValue && MembershipStartDate.HasValue && MembershipEndDate < MembershipStartDate)
        {
            yield return new ValidationResult(
                "MembershipEndDate must be on or after MembershipStartDate.",
                new[] { nameof(MembershipEndDate) });
        }
    }
}
