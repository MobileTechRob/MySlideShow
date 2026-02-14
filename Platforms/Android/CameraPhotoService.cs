
using Android.App;
using Android.Content;
using Android.Database;
using Android.Net;
using Android.OS;
using Android.Provider;
using GoogleGson;
using Microsoft.Maui.Controls.PlatformConfiguration;
using MySlideShow.DataModels;
using MySlideShow.Interfaces;
using System.ComponentModel.Design;
using static Android.Provider.MediaStore;

namespace MySlideShow
{
    public class CameraPhotoService : ICameraPhotoService
    {
        public CameraPhotoService()
        {
        }

        public async Task<CameraPhotosAsyncResult> GetCameraPhotosAsync()
        {            
            var context = Android.App.Application.Context;

            CameraPhotosAsyncResult cameraPhotosAsyncResult = new CameraPhotosAsyncResult();

            string[] projection =
            {
                MediaStore.Images.Media.InterfaceConsts.Id,
                MediaStore.Images.Media.InterfaceConsts.DateAdded
            };

            cameraPhotosAsyncResult.info = "Querying ...";
            string? sortOrder = null; // MediaStore.Images.Media.InterfaceConsts.DateAdded + " DESC";

            Android.Net.Uri? uri1 = MediaStore.Images.Media.ExternalContentUri;
            Android.Net.Uri? uri2 = MediaStore.Images.Media.GetContentUri(MediaStore.VolumeExternalPrimary);

            ICursor? c1 = null;
            ICursor? c2 = null;

            try
            {
                c1 = context.ContentResolver!.Query(uri1!, projection, null, null, null);
                c2 = context.ContentResolver!.Query(uri2!, projection, null, null, null);
            }
            catch (Exception ex)
            {
                cameraPhotosAsyncResult.info += "Exception: " + ex.Message;
            }

            ICursor? cursor = null;
            Android.Net.Uri? contentUri = null;

            try
            {
                if ((c1 != null) && (c1.Count > 0))
                {
                    cursor = c1;
                    contentUri = MediaStore.Images.Media.ExternalContentUri;
                }
                else if ((c2 != null) && (c2.Count > 0))
                {
                    cursor = c2;
                    contentUri = MediaStore.Images.Media.GetContentUri(MediaStore.VolumeExternalPrimary);
                }

                if ((cursor == null) || (cursor.Count == 0))
                {
                    cameraPhotosAsyncResult.info += $"No cursor available.";

                    if (cursor != null)
                        cameraPhotosAsyncResult.info += $"count=({cursor.Count})";

                    return cameraPhotosAsyncResult;
                }
             
                cameraPhotosAsyncResult.info += "have cursor ";

                int idColumn = cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.Id);
                int dateAddedColumn = cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.DateAdded);
                int count = 0;

                cursor.MoveToFirst();

                cameraPhotosAsyncResult.info += cursor.Count.ToString() + " rows";

                while (cursor.MoveToNext())
                {
                    long id = cursor.GetLong(idColumn);
                    string? dateAdded = cursor.GetString(dateAddedColumn);

                    Android.Net.Uri finalContentUri = ContentUris.WithAppendedId(contentUri, id);

                    string? path = finalContentUri.Path;

                    CameraPhoto photo = new CameraPhoto
                    {
                        ImageSource = ImageSource.FromStream(() =>
                        context.ContentResolver.OpenInputStream(finalContentUri)),
                        DateTaken = dateAdded,
                        Path = path
                    };

                    cameraPhotosAsyncResult.cameraPhotos.Add(photo);

                    count++;

                    cameraPhotosAsyncResult.info += "*";

                    if (count >= 50)
                        break;
                }
            }
            catch (Exception ex)
            {
                cameraPhotosAsyncResult.info += "Exception: " + ex.Message;
            }

            return cameraPhotosAsyncResult;
        }

        public void GetPhotosFromCamera(ref CameraPhotosAsyncResult cameraPhotosAsyncResult)
        {
            string dcimDir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).AbsolutePath;

            // Combine with "Camera" subfolder
            string cameraDir = Path.Combine(dcimDir, "Camera");

            if (Directory.Exists(cameraDir))
            {
                cameraPhotosAsyncResult.info += "<" + cameraDir + ">";

                string[] files = Directory.GetFiles(cameraDir);
                foreach (string file in files)
                {
                    CameraPhoto photo = new CameraPhoto
                    {
                        ImageSource = ImageSource.FromFile(file),
                        DateTaken = File.GetCreationTime(file).ToString(),
                        Path = file
                    };
                    cameraPhotosAsyncResult.cameraPhotos.Add(photo);
                }

                cameraPhotosAsyncResult.info += $"{cameraPhotosAsyncResult.cameraPhotos.Count} photos in CameraDir";
            }
            else
            {
                cameraPhotosAsyncResult.info += $" Camera directory not found.";
            }
        }
    }
}
