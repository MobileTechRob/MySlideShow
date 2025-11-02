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
        public uint TransitionTime { get; set; }

        public int DisplayDurationMs { get { return DisplayDuration * 1000; } } // in seconds
        public uint TransitionTimeMs { get { return TransitionTime * 1000; } }


        public PictureConfig(string filePath, int displayDuration, uint transitionTime   )
        {
            FilePath = filePath;
            DisplayDuration = displayDuration; // default duration
            TransitionTime = transitionTime;
        }
    }
}
