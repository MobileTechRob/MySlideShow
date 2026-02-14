using MySlideShow.DataModels;

namespace MySlideShow.Interfaces
{

    public interface IMultiPhotoPicker
    {
        Task<List<CameraPhoto>> PickMultiplePhotosAsync();
    }
}