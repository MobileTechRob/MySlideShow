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
    bool waitForTransition = false;

    public SlideShow(List<PictureConfig> pictureConfigs)
	{
		InitializeComponent();

        _pictureConfigs = pictureConfigs;
    }

    public async void DisplayFirstImage()
    {
        SlideShowImageOne.Source = _pictureConfigs[0].FilePath;
        SlideShowImageOne.Opacity = 1;

        await Task.Run(() => Thread.Sleep(_pictureConfigs[0].DisplayDurationMs));
    }

    public void StartSlideShow(ContentPage page)
    {
        _page = page;
        loopCount = -1;

        StartAnimation(0, true);
    }

    private void StartAnimation(double d, bool b)
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
        if (loopCount == -1)
        {
            TransitionFunctionBeginSlideShow(0, true);            
        }
        else if (loopCount % 2 == 0)
        {
            TransitionFunctionFadePicOneToPicTwo(0, true);            
        }
        else
        {
            TransitionFunctionFadePicTwoToPicOne(0, true);            
        }
    }

    private void TransitionFunctionBeginSlideShow(double d, bool b)
    {
        SlideShowImageOne.Source = "img_black.png";
        SlideShowImageOne.Opacity = 0;

        Animation pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 1, 0);
        parentAnimation = new Animation();
        parentAnimation.Add(0, 1, pictureOne);

        SlideShowImageTwo.Source = _pictureConfigs[0].FilePath;
        Animation pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 0, 1);
        parentAnimation.Add(0, 1, pictureTwo);
        parentAnimation.Commit(this, "Transition1", 32, 250, null, Animation_Completed, null);
    }

    private void TransitionFunctionFadePicOneToPicTwo(double d, bool b)
    {
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
            parentAnimation.Commit(this, "Transition1", 32, _pictureConfigs[loopCount + 1].TransitionTimeMs, null, Animation_Completed, null);              
        }
        else 
        {
            SlideShowImageTwo.Source = _pictureConfigs[loopCount].FilePath;
            Animation pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 0, 1);
            parentAnimation.Add(0, 1, pictureTwo);
            parentAnimation.Commit(this, "Transition1", 32, _pictureConfigs[loopCount].TransitionTimeMs, null, TransitionFunctionCallBack_FadeOut, null);            
        }        
    }


    private void TransitionFunctionFadePicTwoToPicOne(double d, bool b)
    {
        parentAnimation2 = new Animation();
        SlideShowImageTwo.Source = _pictureConfigs[loopCount].FilePath;
        pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 1, 0);
        parentAnimation2.Add(0, 1, pictureTwo);

        if (loopCount + 1 < _pictureConfigs.Count)
        {
            SlideShowImageOne.Source = _pictureConfigs[loopCount + 1].FilePath;
            pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 0, 1);
            parentAnimation2.Add(0, 1, pictureOne);
            parentAnimation2.Commit(this, "Transition2", 32, _pictureConfigs[loopCount + 1].TransitionTimeMs, null, Animation_Completed, null);          
        }
        else  // no more images, fade out.
        {
            SlideShowImageOne.Source = _pictureConfigs[loopCount].FilePath;
            pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 0, 1);
            parentAnimation2.Add(0, 1, pictureOne);
            parentAnimation2.Commit(this, "Transition2", 32, _pictureConfigs[loopCount].TransitionTimeMs, null, TransitionFunctionCallBack_FadeOut, null);            
        }
    }

    private void Animation_Completed(double d, bool b)
    {
        loopCount++;

        Thread.Sleep(_pictureConfigs[loopCount].DisplayDurationMs);

        StartAnimation(0, true);
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

    private void RepeatOrNot(double d, bool b)
    { 
        _page.Navigation.PopAsync();
    }

    private bool EndSlideShow() 
    {
        return true;
    }

}