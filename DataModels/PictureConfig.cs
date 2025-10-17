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

        public PictureConfig(string filePath, int displayDuration)
        {
            FilePath = filePath;
            DisplayDuration = displayDuration; // default duration
        }
    }
}
