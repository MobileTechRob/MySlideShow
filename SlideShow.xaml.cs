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

    public SlideShow(List<PictureConfig> pictureConfigs)
	{
		InitializeComponent();

        _pictureConfigs = pictureConfigs;

    }

    public void DisplayFirstImage()
    {
        SlideShowImageOne.Source = _pictureConfigs[0].FilePath;
        SlideShowImageOne.Opacity = 1;
    }

    public async void StartSlideShow()
    {
        int imageDisplayInterval = _pictureConfigs[0].DisplayDurationMs;
        SlideShowImageOne.Source = _pictureConfigs[0].FilePath;
        SlideShowImageOne.Opacity = 1;
             
        parentAnimation = new Animation();

        SlideShowImageTwo.Source = _pictureConfigs[1].FilePath;

        Animation pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 1, 0);
        Animation pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 0, 1);        

        parentAnimation.Add(0, 1,pictureOne);
        parentAnimation.Add(0, 1,pictureTwo);

        parentAnimation.Commit(this, "Transition1", 32, _pictureConfigs[0].TransitionTimeMs, null, TransitionFunctionCallBack_2_3, null);
    }

    private async void TransitionFunctionCallBack_2_3(double d, bool b)
    {
        await Task.Run(() => { Thread.Sleep(_pictureConfigs[1].DisplayDurationMs); });

        parentAnimation2 = new Animation();
        SlideShowImageTwo.Source = _pictureConfigs[1].FilePath;
        pictureOne = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 1, 0);
        parentAnimation2.Add(0, 1, pictureOne);

        if (_pictureConfigs.Count > 3) 
        {
            SlideShowImageOne.Source = _pictureConfigs[2].FilePath;
            pictureTwo = new Animation(v => { SlideShowImageOne.Opacity = v; }, 0, 1);            
            parentAnimation2.Add(0, 1, pictureTwo);        
        }

        parentAnimation2.Commit(this, "Transition2", 32, _pictureConfigs[1].TransitionTimeMs, null, TransitionFunctionCallBack_3_4, null);
    }

    private async void TransitionFunctionCallBack_3_4(double d, bool b)
    {
        await Task.Run(() => { Thread.Sleep(_pictureConfigs[2].DisplayDurationMs); });

        parentAnimation2 = new Animation();
        SlideShowImageOne.Source = _pictureConfigs[2].FilePath;
        pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 1, 0);

        parentAnimation2.Add(0, 1, pictureOne);

        if (_pictureConfigs.Count > 4)
        {
            SlideShowImageTwo.Source = _pictureConfigs[3].FilePath;
            pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 0, 1);
            parentAnimation2.Add(0, 1, pictureTwo);
        }

        parentAnimation2.Commit(this, "Transition3", 32, _pictureConfigs[2].TransitionTimeMs, null, TransitionFunctionCallBack_4_5, null);
    }

    private async void TransitionFunctionCallBack_4_5(double d, bool b)
    {
        await Task.Run(() => { Thread.Sleep(_pictureConfigs[3].DisplayDurationMs); });

        parentAnimation2 = new Animation();
        SlideShowImageTwo.Source = _pictureConfigs[3].FilePath;
        pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 1, 0);
        parentAnimation2.Add(0, 1, pictureTwo);

        if (_pictureConfigs.Count > 5)
        {
            SlideShowImageOne.Source = _pictureConfigs[4].FilePath;
            pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 0, 1);
            parentAnimation2.Add(0, 1, pictureOne);
        }

        parentAnimation2.Commit(this, "Transition4", 32, _pictureConfigs[3].TransitionTimeMs, null, TransitionFunctionCallBack_5_6, null);
    }

    private async void TransitionFunctionCallBack_5_6(double d, bool b)
    {
        await Task.Run(() => { Thread.Sleep(_pictureConfigs[4].DisplayDurationMs); });

        parentAnimation2 = new Animation();
        SlideShowImageTwo.Source = _pictureConfigs[4].FilePath;
        pictureOne = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 1, 0);
        parentAnimation2.Add(0, 1, pictureOne);

        if (_pictureConfigs.Count > 6)
        {
            SlideShowImageOne.Source = _pictureConfigs[5].FilePath;
            pictureTwo = new Animation(v => { SlideShowImageOne.Opacity = v; }, 0, 1);
            parentAnimation2.Add(0, 1, pictureTwo);
        }


        parentAnimation2.Commit(this, "Transition5", 32, _pictureConfigs[4].TransitionTimeMs, null, TransitionFunctionCallBack_6_7, null);
    }

    private async void TransitionFunctionCallBack_6_7(double d, bool b)
    {
        await Task.Run(() => { Thread.Sleep(_pictureConfigs[5].DisplayDurationMs); });

        parentAnimation2 = new Animation();
        SlideShowImageTwo.Source = _pictureConfigs[5].FilePath;
        pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 1, 0);
        parentAnimation2.Add(0, 1, pictureTwo);

        if (_pictureConfigs.Count > 7)
        {
            SlideShowImageOne.Source = _pictureConfigs[6].FilePath;
            pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 0, 1);
            parentAnimation2.Add(0, 1, pictureOne);
        }

        parentAnimation2.Commit(this, "Transition6", 32, _pictureConfigs[5].TransitionTimeMs, null, TransitionFunctionCallBack_7_8, null);
    }

    private async void TransitionFunctionCallBack_7_8(double d, bool b)
    {
        await Task.Run(() => { Thread.Sleep(_pictureConfigs[6].DisplayDurationMs); });

        parentAnimation2 = new Animation();
        SlideShowImageOne.Source = _pictureConfigs[6].FilePath;
        pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 1, 0);
        parentAnimation2.Add(0, 1, pictureOne);

        if (_pictureConfigs.Count > 8)
        {
            SlideShowImageTwo.Source = _pictureConfigs[7].FilePath;
            pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 0, 1);
            parentAnimation2.Add(0, 1, pictureTwo);
        }

        parentAnimation2.Commit(this, "Transition7", 32, _pictureConfigs[6].TransitionTimeMs, null, TransitionFunctionCallBack_8_9, null);
    }

    private async void TransitionFunctionCallBack_8_9(double d, bool b)
    {
        await Task.Run(() => { Thread.Sleep(_pictureConfigs[7].DisplayDurationMs); });

        parentAnimation2 = new Animation();
        SlideShowImageTwo.Source = _pictureConfigs[7].FilePath;
        pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 1, 0);
        parentAnimation2.Add(0, 1, pictureTwo);

        if (_pictureConfigs.Count > 9) 
        {
            SlideShowImageOne.Source = _pictureConfigs[8].FilePath;
            pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 0, 1);
            parentAnimation2.Add(0, 1, pictureOne);
        }

        parentAnimation2.Commit(this, "Transition8", 32, _pictureConfigs[7].TransitionTimeMs, null, TransitionFunctionCallBack_9_10, null);
    }

    private async void TransitionFunctionCallBack_9_10(double d, bool b)
    {
        await Task.Run(() => { Thread.Sleep(_pictureConfigs[8].DisplayDurationMs); });

        parentAnimation2 = new Animation();
        SlideShowImageOne.Source = _pictureConfigs[8].FilePath;
        pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 1, 0);
        parentAnimation2.Add(0, 1, pictureOne);

        if (_pictureConfigs.Count > 10)
        {
            SlideShowImageTwo.Source = _pictureConfigs[9].FilePath;
            pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 0, 1);
            parentAnimation2.Add(0, 1, pictureTwo);
        }

        parentAnimation2.Commit(this, "Transition9", 32, _pictureConfigs[8].TransitionTimeMs, null, TransitionFunctionCallBack_10_11, null);
    }

    private async void TransitionFunctionCallBack_10_11(double d, bool b)
    {
        await Task.Run(() => { Thread.Sleep(_pictureConfigs[9].DisplayDurationMs); });

        parentAnimation2 = new Animation();
        SlideShowImageTwo.Source = _pictureConfigs[9].FilePath;
        pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 1, 0);
        parentAnimation2.Add(0, 1, pictureTwo);

        if (_pictureConfigs.Count > 11)
        {
            SlideShowImageOne.Source = _pictureConfigs[10].FilePath;
            pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 0, 1);
            parentAnimation2.Add(0, 1, pictureOne);
        }

        parentAnimation2.Commit(this, "Transition10", 32, _pictureConfigs[9].TransitionTimeMs, null, TransitionFunctionCallBack_11_12, null);
    }

    private async void TransitionFunctionCallBack_11_12(double d, bool b)
    {
        await Task.Run(() => { Thread.Sleep(_pictureConfigs[10].DisplayDurationMs); });

        parentAnimation2 = new Animation();
        SlideShowImageOne.Source = _pictureConfigs[10].FilePath;
        pictureOne = new Animation(v => { SlideShowImageOne.Opacity = v; }, 1, 0);
        parentAnimation2.Add(0, 1, pictureOne);

        if (_pictureConfigs.Count > 12)
        {
            SlideShowImageTwo.Source = _pictureConfigs[11].FilePath;
            pictureTwo = new Animation(v => { SlideShowImageTwo.Opacity = v; }, 0, 1);
            parentAnimation2.Add(0, 1, pictureTwo);
        }

        parentAnimation2.Commit(this, "Transition11", 32, _pictureConfigs[10].TransitionTimeMs, null, null, null);
    }

    private bool EndSlideShow() 
    {
        return true;
    }

}