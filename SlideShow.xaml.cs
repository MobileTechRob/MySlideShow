using MySlideShow.DataModels;
using System;
using System.Threading.Tasks;
namespace MySlideShow;


public partial class SlideShow : ContentPage
{
	List<PictureConfig> _pictureConfigs;
    Animation parentAnimation;
    Animation parentAnimation2;

    Animation pictureOne;
    Animation pictureTwo;

    uint durationInMilliseconds = 4000;

    public SlideShow(List<PictureConfig> pictureConfigs)
	{
		InitializeComponent();

        _pictureConfigs = pictureConfigs;

        _pictureConfigs[0].FilePath = "img_1.png";
        _pictureConfigs[1].FilePath = "img_2.png";
        _pictureConfigs[2].FilePath = "img_3.png";
    }

    public void DisplayFirstImage()
    {
        SlideShowImageOne.Source = _pictureConfigs[0].FilePath;
        SlideShowImageOne.Opacity = 1;
    }

    public async void StartSlideShow()
    {
        int imageDisplayInterval = _pictureConfigs[0].DisplayDuration;
        SlideShowImageOne.Source = _pictureConfigs[0].FilePath;
        SlideShowImageOne.Opacity = 1;
             
        parentAnimation = new Animation();

        SlideShowImageTwo.Source = _pictureConfigs[1].FilePath;

        Animation pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 1, 0);
        Animation pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 0, 1);        

        parentAnimation.Add(0, 1,pictureOne);
        parentAnimation.Add(0, 1,pictureTwo);

        durationInMilliseconds = 4000;
        parentAnimation.Commit(this, "Transition1", 32, durationInMilliseconds, null, TransitionFunctionCallBack, null);
    }

    private void TransitionFunctionCallBack(double d, bool b)
    {
        parentAnimation2 = new Animation();
        SlideShowImageTwo.Source = _pictureConfigs[1].FilePath;
        SlideShowImageOne.Source = _pictureConfigs[2].FilePath;

        pictureOne = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 1, 0);
        pictureTwo = new Animation(v => { SlideShowImageOne.Opacity = v; }, 0, 1);

        parentAnimation2.Add(0, 1, pictureOne);
        parentAnimation2.Add(0, 1, pictureTwo);

        parentAnimation2.Commit(this, "Transition2", 32, durationInMilliseconds, null, null, null);
    }

    private bool EndSlideShow() 
    {
        return true;
    }

}