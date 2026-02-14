using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySlideShow.DataModels
{
    public class CameraPhotosAsyncResult
    {
        public string info;
        public List<CameraPhoto> cameraPhotos; 
        
        public CameraPhotosAsyncResult()
        {
            this.info = "";
            this.cameraPhotos = new List<CameraPhoto>();
        }   

    }
}
