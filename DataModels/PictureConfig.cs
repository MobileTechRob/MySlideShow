using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySlideShow.DataModels
{
    public class PictureConfig
    {
        public string FilePath { get; set; }
        public int DisplayDuration { get; set; } // in seconds
        public int TransitionTime { get; set; }

        public PictureConfig(string filePath, int displayDuration, int transitionTime   )
        {
            FilePath = filePath;
            DisplayDuration = displayDuration; // default duration
            TransitionTime = transitionTime;
        }
    }
}
