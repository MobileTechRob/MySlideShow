namespace MySlideShow;

using MySlideShow.DataModels;
using MySlideShow.Interfaces;

public partial class EditPicture : ContentPage
{
    public EditPicture(ContentPage page, PictureConfig pictureConfig)
    {
        InitializeComponent();
        IPhotoConfigRepository photoConfigRepository = MauiProgram.CreateMauiApp().Services.GetService<Interfaces.IPhotoConfigRepository>()!;
        BindingContext = new ViewModel.EditPictureVM(page, pictureConfig, photoConfigRepository);
    }


    private void DisplayTime_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        int displayTime = (int)e.NewValue;

        string displayTimeAsString = displayTime.ToString("D1").Trim('0'); ;

        if (displayTime == 10) 
            displayTimeAsString = displayTime.ToString("D2");

        DisplayTimeLabel.Text = "Display secs: " + displayTimeAsString;

        if (BindingContext is ViewModel.EditPictureVM editPictureVM)
        {
            editPictureVM.PictureConfig.DisplayDuration = (int)e.NewValue;
        }
    }

    private void Fader_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        uint timeSeconds = (uint)e.NewValue;

        TransitionLabel.Text = "Transition secs: " + timeSeconds.ToString("D1");

        if (BindingContext is ViewModel.EditPictureVM editPictureVM)
        {
            editPictureVM.PictureConfig.TransitionTime = timeSeconds;
        }
    }

}