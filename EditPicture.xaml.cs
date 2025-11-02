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

    private void DurationEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (DurationEditor != null) 
        {
            if (int.TryParse(DurationEditor.Text, out int duration))
            {
                if (BindingContext is ViewModel.EditPictureVM editPictureVM)
                {
                    editPictureVM.PictureConfig.DisplayDuration = duration;
                }
            }
        }
    }

    private void Fader_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        uint timeSeconds = (uint)e.NewValue;

        TransitionLabel.Text = "Transition secs: " + timeSeconds.ToString("D1");

        if (timeSeconds == 10) 
            TransitionLabel.Text = "Transition secs: " + timeSeconds.ToString("D2");

        if (BindingContext is ViewModel.EditPictureVM editPictureVM)
        {
            editPictureVM.PictureConfig.TransitionTime = timeSeconds;
        }
    }
}