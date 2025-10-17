using MySlideShow.DataModels;
using MySlideShow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using System.Text.Json;
using System.IO;

namespace MySlideShow.Services
{
    public class PhotoConfigRepository : Interfaces.IPhotoConfigRepository
    {
        string fullPath = FileSystem.AppDataDirectory + "/photos.json";

        void IPhotoConfigRepository.DeletePhotos()
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        List<PictureConfig> IPhotoConfigRepository.LoadPhotos()
        {
            if (File.Exists(fullPath) == false)
            {
                return null!;
            }
;
            List<PictureConfig> listOfPictures = new();
            listOfPictures = JsonSerializer.Deserialize<List<PictureConfig>>(File.ReadAllText(fullPath))!;   
            return listOfPictures;
        }

        void IPhotoConfigRepository.SavePhoto(PictureConfig pictureConfig)
        {        
            if (File.Exists(fullPath) == false)
            {
                return;
            }   

            List<PictureConfig> listOfPictures = new();
            listOfPictures = JsonSerializer.Deserialize<List<PictureConfig>>(File.ReadAllText(fullPath))!;   
            listOfPictures.Add(pictureConfig);

            string jsonPhotoConfigList = JsonSerializer.Serialize<List<PictureConfig>>(listOfPictures);
            File.WriteAllText(fullPath, jsonPhotoConfigList);   
        }
    }
}
