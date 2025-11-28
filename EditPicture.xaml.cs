namespace MySlideShow;

using MySlideShow.DataModels;
using MySlideShow.Interfaces;
using System.Threading.Tasks;

public partial class EditPicture : ContentPage
{
    
    IPhotoConfigRepository photoConfigRepository;
    List<PictureConfig> loadedPictures;

    public EditPicture()
    {
        InitializeComponent();
        
        photoConfigRepository = MauiProgram.CreateMauiApp().Services.GetService<Interfaces.IPhotoConfigRepository>()!;
        //BindingContext = new ViewModel.EditPictureVM(this, null, photoConfigRepository);
    }

    public EditPicture(ContentPage page, PictureConfig pictureConfig)
    {
        InitializeComponent();
        photoConfigRepository = MauiProgram.CreateMauiApp().Services.GetService<Interfaces.IPhotoConfigRepository>()!;

        loadedPictures = photoConfigRepository.LoadPhotos(); 

        BindingContext = new ViewModel.EditPictureVM(page, pictureConfig, photoConfigRepository);
    }

   
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        //if (BindingContext is ViewModel.EditPictureVM editPictureVM)
        //{
        PictureConfig pictureConfig = await AddNewPicture();

        BindingContext = new ViewModel.EditPictureVM(this, pictureConfig, photoConfigRepository);

        //          editPictureVM.PictureConfig = AddNewPicture().Result ?? editPictureVM.PictureConfig;

        //int displayTime = editPictureVM.PictureConfig.DisplayDuration;
        //DisplayTimeSlider.Value = displayTime;
        //DisplayTimeLabel.Text = "Display secs: " + (displayTime == 10 ? displayTime.ToString("D2") : displayTime.ToString("D1").Trim('0'));
        //uint transitionTime = editPictureVM.PictureConfig.TransitionTime;
        //FaderSlider.Value = transitionTime;
        //TransitionLabel.Text = "Transition secs: " + transitionTime.ToString("D1");
        //}
    }

    public async Task<PictureConfig> AddNewPicture()
    {
        PictureConfig pictureConfig = null;
        // Logic to add a picture
        string photo = await SelectPhotoAsync();

        if (System.Diagnostics.Debugger.IsAttached)
        {
            // Code to run only when debugging
            if (string.IsNullOrEmpty(photo))
            {
                if (loadedPictures == null)
                    photo = "img_1.png";
                else
                {
                    int count = loadedPictures.Count + 1;
                    photo = "img_" + count.ToString() + ".png";
                }

                pictureConfig = new PictureConfig(photo);
            }
        }
        else if (!string.IsNullOrEmpty(photo))
        {
            // User cancelled or no photo selected
            pictureConfig = new PictureConfig(photo, 5, 5);
        } 
        
        return pictureConfig;
    }

    private async Task<string> SelectPhotoAsync()
    {
        string localFilePath = string.Empty;

        try
        {
            MediaPickerOptions mediaPickerOptions = new MediaPickerOptions();
            mediaPickerOptions.Title = "Select Photo";
            FileResult fileResult = await MediaPicker.PickPhotoAsync(mediaPickerOptions);

            if (fileResult != null)
            {
                localFilePath = fileResult.FullPath;

            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            // Handle exceptions (permissions, not supported, etc.)
        }

        return localFilePath;
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