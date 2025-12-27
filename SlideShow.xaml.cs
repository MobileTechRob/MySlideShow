using MySlideShow.DataModels;
using MySlideShow.Interfaces;
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

    int loopCount = 0;
    bool waitForTransition = false;

    IPhotoConfigRepository _photoConfigRepository;

    public SlideShow()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        _photoConfigRepository = MauiProgram.CreateMauiApp().Services.GetService<Interfaces.IPhotoConfigRepository>()!;
        _pictureConfigs = _photoConfigRepository.LoadPhotos();

        if ((_pictureConfigs == null) || (_pictureConfigs != null && _pictureConfigs.Count == 0))
            return;

        StartSlideShow(this);
    }   

    public void StartSlideShow(ContentPage page)
    {     
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
            parentAnimation.Commit(this, "Transition1", 32, _pictureConfigs[loopCount + 1].FadeTimeMs, null, Animation_Completed, null);              
        }
        else 
        {
            TransitionFunctionCallBack_FadeOut(1);
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
            parentAnimation2.Commit(this, "Transition2", 32, _pictureConfigs[loopCount + 1].FadeTimeMs, null, Animation_Completed, null);          
        }
        else  // no more images, fade out.
        {
            TransitionFunctionCallBack_FadeOut(2);
        }
    }

    private void Animation_Completed(double d, bool b)
    {
        loopCount++;

        Thread.Sleep(_pictureConfigs[loopCount].DisplayDurationMs);

        StartAnimation(0, true);
    }

    private void TransitionFunctionCallBack_FadeOut(int imageNumber)
    {
        parentAnimation2 = new Animation();

        if (imageNumber == 1)
        {
            SlideShowImageOne.Source = _pictureConfigs[loopCount].FilePath;
            pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 1, 0);
            parentAnimation2.Add(0, 1, pictureOne);
        }
        else
        { 
            SlideShowImageTwo.Source = _pictureConfigs[loopCount].FilePath;
            pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 1, 0);
            parentAnimation2.Add(0, 1, pictureTwo);
        }

        parentAnimation2.Commit(this, "Transition3", 32, _pictureConfigs[loopCount].FadeTimeMs, null, RepeatOrNot, null);
    }

    private void RepeatOrNot(double d, bool b)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}