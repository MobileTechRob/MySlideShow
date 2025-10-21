using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySlideShow.DataModels;

namespace MySlideShow.Interfaces
{
    public interface IPhotoConfigRepository
    {
        void DeletePhoto(PictureConfig pictureConfig);
        void SavePhoto(PictureConfig pictureConfig);
        List<PictureConfig> LoadPhotos();
        void DeletePhotos();
    }
}
