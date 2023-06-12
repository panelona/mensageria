using System.ComponentModel.DataAnnotations;

namespace MS.Emails.Respositories.Dto
{
    public class EmailRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
