using System.ComponentModel.DataAnnotations;

namespace MS.Emails.Respositories.Dto
{
    public class EmailResponseDto
    {
        [Required]
        public string Codigo { get; set; }
    }
}
