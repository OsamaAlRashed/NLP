using NLP.Enums;

namespace NLP.ViewModels
{
    public class AudioFeatureVM
    {
        public AudioForType ForType { get; set; }
        public double RMS { get; set; }
        public double ZCR { get; set; }
        public double Energy { get; set; }
    }
}
