using NLP.Enums;
using System.ComponentModel.DataAnnotations;

namespace NLP.Models
{
    public class AudioFeature
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "مطلوب")]
        [Display(Name = "خاص ب")]
        public AudioForType ForType { get; set; }
        public double RMS { get; set; }
        public double ZCR { get; set; }
        public double Energy { get; set; }
        public string Path { get; set; }
    }
}
