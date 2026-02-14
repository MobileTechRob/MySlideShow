using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySlideShow.DataModels;

namespace MySlideShow.Interfaces
{
    public interface ICameraPhotoService
    {
        Task<CameraPhotosAsyncResult> GetCameraPhotosAsync();
    }
}
