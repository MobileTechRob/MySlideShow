namespace MySlideShow.ViewModel;

public class SlideShowVM : ContentPage
{
	List<DataModels.PictureConfig> _pictureConfigs;

    public SlideShowVM(List<DataModels.PictureConfig> pictureConfigs)
	{
		_pictureConfigs = pictureConfigs;
    }
}