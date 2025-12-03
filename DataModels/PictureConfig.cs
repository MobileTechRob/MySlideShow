using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MySlideShow.DataModels
{
    public class PictureConfig
    {
        public string FilePath { get; set; }
        public int DisplayDuration { get; set; } // in seconds
        public uint FadeTime { get; set; }

        [JsonIgnore]
        public string DisplayDurationText { get { return " Display Time (secs): " + DisplayDuration.ToString(); } } // in seconds

        [JsonIgnore]
        public string TransitionTimeText { get { return " Fade Time (secs): " + FadeTime.ToString(); } }

        [JsonIgnore]
        public int DisplayDurationMs { get { return DisplayDuration * 1000; } } // in seconds

        [JsonIgnore]
        public uint FadeTimeMs { get { return FadeTime * 1000; } }        
        public Guid Id {  get; set; }   

        public PictureConfig()
        {
            FilePath = string.Empty;
            DisplayDuration = 1; // default duration
            FadeTime = 1;          
            Id = Guid.NewGuid();
        }

        public PictureConfig(string filePath, int displayDuration =1, uint transitionTime = 1)
        {
            FilePath = filePath;
            DisplayDuration = displayDuration; // default duration
            FadeTime = transitionTime;          
            Id = Guid.NewGuid();
        }
    }
}
