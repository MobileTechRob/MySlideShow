using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Net;
using Android.Provider;
using Microsoft.Maui.ApplicationModel;
using MySlideShow.Interfaces;
using MySlideShow.DataModels;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using Android.Runtime;

[assembly: Dependency(typeof(MySlideShow.MultiPhotoPicker))]
namespace MySlideShow
{
    //public class MyAndroidUri : Android.Net.Uri
    //{
    //    public MyAndroidUri(string uriString) 
    //    {
    //        // Call the base class constructor  
    //        Android.Net.Uri baseUri = Android.Net.Uri.Parse(uriString);
    //    }

    //    public override string? Authority => throw new NotImplementedException();

    //    public override string? EncodedAuthority => throw new NotImplementedException();

    //    public override string? EncodedFragment => throw new NotImplementedException();

    //    public override string? EncodedPath => throw new NotImplementedException();

    //    public override string? EncodedQuery => throw new NotImplementedException();

    //    public override string? EncodedSchemeSpecificPart => throw new NotImplementedException();

    //    public override string? EncodedUserInfo => throw new NotImplementedException();

    //    public override string? Fragment => throw new NotImplementedException();

    //    public override string? Host => throw new NotImplementedException();

    //    public override bool IsHierarchical => throw new NotImplementedException();

    //    public override bool IsRelative => throw new NotImplementedException();

    //    public override string? LastPathSegment => throw new NotImplementedException();

    //    public override string? Path => throw new NotImplementedException();

    //    public override IList<string>? PathSegments => throw new NotImplementedException();

    //    public override int Port => throw new NotImplementedException();

    //    public override string? Query => throw new NotImplementedException();

    //    public override string? Scheme => throw new NotImplementedException();

    //    public override string? SchemeSpecificPart => throw new NotImplementedException();

    //    public override string? UserInfo => throw new NotImplementedException();

    //    public override Builder? BuildUpon()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override int DescribeContents()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override string? ToString()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}   


    public class MultiPhotoPicker : IMultiPhotoPicker
    {
        public TaskCompletionSource<List<CameraPhoto>>? _tcs;

        public Task<List<CameraPhoto>> PickMultiplePhotosAsync()
        {
            _tcs = new TaskCompletionSource<List<CameraPhoto>>();

            var intent = new Intent(Intent.ActionGetContent);
            intent.SetType("image/*");
            intent.PutExtra(Intent.ExtraAllowMultiple, true);
            intent.AddCategory(Intent.CategoryOpenable);

            var activity = Platform.CurrentActivity ?? throw new NullReferenceException("No current activity");

            MainActivity.ActivityResultReceived += OnActivityResult;

            activity.StartActivityForResult(Intent.CreateChooser(intent, "Select Pictures"), 9999);


            return _tcs.Task;
        }

        private void OnActivityResult(int requestCode, Result resultCode, Intent? data)
        {
            if (requestCode == 9999)
            {
                MainActivity.ActivityResultReceived -= OnActivityResult;
                var result = new List<CameraPhoto>();

                if (resultCode == Result.Ok && data != null)
                {
                    if (data.ClipData != null)
                    {
                        for (int i = 0; i < data.ClipData.ItemCount; i++)
                        {
                            var uri = data.ClipData.GetItemAt(i).Uri;

                            CameraPhoto photo = new CameraPhoto();

                            string UriAsString = uri.ToString();

                            // Use the stream to load the image (e.g., into an ImageSource)
                            //photo.ImageSource = ImageSource.FromStream(() => Android.App.Application.Context.ContentResolver.OpenInputStream(uri));
                            photo.ImageSource = ImageSource.FromStream(() => Android.App.Application.Context.ContentResolver.OpenInputStream(Android.Net.Uri.Parse(UriAsString)));

                            photo.notes = "data.ClipBoard";

                            result.Add(photo);
                        }
                    }
                    else if (data.Data != null)
                    {
                        CameraPhoto photo = new CameraPhoto();

                        string UriAsString = data.Data.ToString();

                        // Use the stream to load the image (e.g., into an ImageSource)
                        photo.ImageSource = ImageSource.FromStream(() => Android.App.Application.Context.ContentResolver.OpenInputStream(Android.Net.Uri.Parse(UriAsString)));

                        photo.notes = "single selection";

                        result.Add(photo);
                    }
                }
                _tcs?.SetResult(result);
            }
        }

        private string GetPathFromUri(Android.Net.Uri uri)
        {
            // Implement logic to get file path from uri
            return uri.Path!;
        }
    }
}