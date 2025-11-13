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
    int imageToFadeOut;

    ContentPage _page;
    int loopCount = 0;


    public SlideShow(List<PictureConfig> pictureConfigs)
	{
		InitializeComponent();

        _pictureConfigs = pictureConfigs;
    }

    public async void DisplayFirstImage()
    {
        SlideShowImageOne.Source = _pictureConfigs[0].FilePath;
        SlideShowImageOne.Opacity = 1;
        await Task.Run(() => { Thread.Sleep(_pictureConfigs[0].DisplayDuration * 1000); });
    }

    public async void StartSlideShow(ContentPage page)
    {
        _page = page;
        loopCount = 0;

        StartAnimation(0, true);
    }

    private async void StartAnimation(double d, bool b)
    {
        // loop through images
        // wait for x seconds to display image

        // loop at loop count and determine which function to call.
        // image 0 to image 1 in calls TransitionFunctionCallBack_PictureOneToPictureTwo
        // image 1 to image 2 in calls TransitionFunctionCallBack_PictureTwoToPictureOne

        // even image count to odd image count
        // odd image count to even image count

        //TransitionFunctionCallBack_PictureOneToPictureTwo(0, true);
        //TransitionFunctionCallBack_PictureTwoToPictureOne(0, true);
        
        if (loopCount % 2 == 0)
        {
            TransitionFunctionFadePicOneToPicTwo(0, true);
            loopCount++;
        }
        else
        {
            TransitionFunctionFadePicTwoToPicOne(0, true);
            loopCount++;
        }

        if (loopCount < _pictureConfigs.Count)
        {
            // end of slideshow
            await Task.Run(() => { Thread.Sleep(_pictureConfigs[loopCount].DisplayDurationMs); });
        }   
        
    }

    private async void TransitionFunctionFadePicOneToPicTwo(double d, bool b)
    {
        int imageDisplayInterval = _pictureConfigs[loopCount].DisplayDurationMs;
        SlideShowImageOne.Source = _pictureConfigs[loopCount].FilePath;
        SlideShowImageOne.Opacity = 1;

        Animation pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 1, 0);
        parentAnimation = new Animation();
        parentAnimation.Add(0, 1, pictureOne);

        if ((loopCount + 1) < _pictureConfigs.Count)
        {
            SlideShowImageTwo.Source = _pictureConfigs[loopCount + 1].FilePath;
            Animation pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 0, 1);            
            parentAnimation.Add(0, 1, pictureTwo);
            parentAnimation.Commit(this, "Transition1", 32, _pictureConfigs[loopCount + 1].TransitionTimeMs, null, StartAnimation, null);
        }
        else 
        {
            SlideShowImageTwo.Source = _pictureConfigs[loopCount].FilePath;
            Animation pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 0, 1);
            parentAnimation.Add(0, 1, pictureTwo);
            parentAnimation.Commit(this, "Transition1", 32, _pictureConfigs[loopCount].TransitionTimeMs, null, TransitionFunctionCallBack_FadeOut, null);
        }        
    }


    private async void TransitionFunctionFadePicTwoToPicOne(double d, bool b)
    {
        // This function is intentionally left blank.
        //await Task.Run(() => { Thread.Sleep(_pictureConfigs[loopCount].DisplayDurationMs); });

        parentAnimation2 = new Animation();
        SlideShowImageTwo.Source = _pictureConfigs[loopCount].FilePath;
        pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 1, 0);
        parentAnimation2.Add(0, 1, pictureTwo);

        if (loopCount + 1 < _pictureConfigs.Count)
        {
            SlideShowImageOne.Source = _pictureConfigs[loopCount + 1].FilePath;
            pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 0, 1);
            parentAnimation2.Add(0, 1, pictureOne);
            parentAnimation2.Commit(this, "Transition2", 32, _pictureConfigs[loopCount + 1].TransitionTimeMs, null, StartAnimation, null);
        }
        else  // no more images, fade out.
        {
            SlideShowImageOne.Source = _pictureConfigs[loopCount].FilePath;
            pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 0, 1);
            parentAnimation2.Add(0, 1, pictureOne);
            parentAnimation2.Commit(this, "Transition2", 32, _pictureConfigs[loopCount].TransitionTimeMs, null, TransitionFunctionCallBack_FadeOut, null);
        }
    }

    private async void TransitionFunctionCallBack_FadeOut(double d, bool b)
    {
        loopCount = _pictureConfigs.Count - 1;

        await Task.Run(() => { Thread.Sleep(_pictureConfigs[loopCount].DisplayDurationMs); });

        parentAnimation2 = new Animation();
        SlideShowImageOne.Source = _pictureConfigs[loopCount].FilePath;
        pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 1, 0);

        parentAnimation2.Add(0, 1, pictureOne);
        parentAnimation2.Commit(this, "Transition3", 32, _pictureConfigs[loopCount].TransitionTimeMs, null, RepeatOrNot, null);
    }

    private async void RepeatOrNot(double d, bool b)
    { 
        _page.Navigation.PopAsync();
    }

    private bool EndSlideShow() 
    {
        return true;
    }

}