using NLP.Enums;
using System.ComponentModel.DataAnnotations;

namespace NLP.ViewModels
{
    public class AudioFileVM
    {
        public IFormFile File { get; set; }

        [Display(Name = "الكلمة")]
        public AudioForType ForType { get; set; }
    }
}
