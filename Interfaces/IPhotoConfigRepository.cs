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
        public void DeletePhoto(PictureConfig pictureConfig);
        public void SavePhoto(PictureConfig pictureConfig);
        public void SavePhotos(List<PictureConfig> pictureConfigs);
        public List<PictureConfig> LoadPhotos();
        public bool VerifyFile();
        public void DeletePhotos();
    }
}
