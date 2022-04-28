using NLP.Enums;
using System.ComponentModel.DataAnnotations;

namespace NLP.Models
{
    public class Word
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "الكلمة")]
        [Required(ErrorMessage = "الكلمة مطلوبة")]
        public string Text { get; set; }

        [Display(Name = "النوع")]
        [Required(ErrorMessage = "النوع مطلوب")]
        public WordType Type { get; set; }

    }
}
