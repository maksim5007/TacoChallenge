using System.ComponentModel.DataAnnotations;

namespace TacoChallenge.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}