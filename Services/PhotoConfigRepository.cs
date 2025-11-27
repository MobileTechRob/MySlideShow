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

        public void DeletePhoto(PictureConfig picConfig)
        {
            List<PictureConfig> listOfPictures = JsonSerializer.Deserialize<List<PictureConfig>>(File.ReadAllText(fullPath))!;

            listOfPictures.RemoveAll(pic => pic.Id == picConfig.Id);

            if (listOfPictures.Count == 0)
            {
                File.Delete(fullPath);
                return;
            }

            string jsonPhotoConfigList = JsonSerializer.Serialize<List<PictureConfig>>(listOfPictures);
            File.WriteAllText(fullPath, jsonPhotoConfigList);
        }

        public void SavePhotos(List<PictureConfig> pictureConfigs)
        {
            if (File.Exists(fullPath) == false)
            {
                List<PictureConfig> listOfPictures = new();

                foreach (PictureConfig pictureConfig in pictureConfigs)
                    listOfPictures.Add(pictureConfig);

                string jsonPhotoConfigList = JsonSerializer.Serialize<List<PictureConfig>>(listOfPictures);
                File.WriteAllText(fullPath, jsonPhotoConfigList);
            }
        }

        bool IPhotoConfigRepository.VerifyFile()
        {
            if (File.Exists(fullPath) == false)
            {
                return false;
            }
;
            List<PictureConfig> listOfPictures = new();
            listOfPictures = JsonSerializer.Deserialize<List<PictureConfig>>(File.ReadAllText(fullPath))!;

            if (listOfPictures != null && listOfPictures.Count >= 1)
            {
                if (listOfPictures.Where(pic => File.Exists(pic.FilePath) == false).Count<PictureConfig>() >= 1)
                {
                    File.Delete(fullPath);
                    return false; 
                }            
            }

            return true;
        }

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
            List<PictureConfig> listOfPictures = new();

            if (File.Exists(fullPath))
            {                
                listOfPictures = JsonSerializer.Deserialize<List<PictureConfig>>(File.ReadAllText(fullPath))!;

                SortedList<int, PictureConfig> sortedList = new();

                foreach (PictureConfig pc in listOfPictures)                
                    sortedList.Add(sortedList.Count, pc);

                IEnumerable<KeyValuePair<int, PictureConfig>> keyValues= sortedList.Where(p => p.Value.FilePath == pictureConfig.FilePath);

                if (keyValues.Any())
                {
                    sortedList[keyValues.First().Key] = pictureConfig;                    
                }
                else
                {
                    sortedList.Add(sortedList.Count, pictureConfig);
                }

                listOfPictures = sortedList.Values.ToList<PictureConfig>();

            }
            else
                listOfPictures.Add(pictureConfig);

            string jsonPhotoConfigList = JsonSerializer.Serialize<List<PictureConfig>>(listOfPictures);
            File.WriteAllText(fullPath, jsonPhotoConfigList);   
        }
    }
}
