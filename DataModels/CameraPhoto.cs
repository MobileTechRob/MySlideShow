using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySlideShow.DataModels
{
    public class CameraPhoto
    {
        public string UriAsString { get; set; } = "";
        public ImageSource? ImageSource { get; set; }
        public string? DateTaken { get; set; }
        public string? Path { get; set; }           
        public string notes { get; set; } = "";
    }
}
