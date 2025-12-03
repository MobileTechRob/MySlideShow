namespace MySlideShow;

using MySlideShow.DataModels;
using MySlideShow.Interfaces;
using System.Threading.Tasks;

public partial class EditPicture : ContentPage
{
    
    IPhotoConfigRepository photoConfigRepository;
    List<PictureConfig> loadedPictures;
    bool editPicture = false;
    PictureConfig pictureConfig = null;


    public EditPicture()
    {
        InitializeComponent();
        
        photoConfigRepository = MauiProgram.CreateMauiApp().Services.GetService<Interfaces.IPhotoConfigRepository>()!;

        loadedPictures = photoConfigRepository.LoadPhotos();
    }

    public EditPicture(ContentPage page, PictureConfig pictureConfig)
    {
        InitializeComponent();
        photoConfigRepository = MauiProgram.CreateMauiApp().Services.GetService<Interfaces.IPhotoConfigRepository>()!;

        this.pictureConfig = pictureConfig;

        BindingContext = new ViewModel.EditPictureVM(page, pictureConfig, photoConfigRepository);
        editPicture = true;
    }

   
    protected override async void OnAppearing()
    {
        if (!editPicture)
        {
            pictureConfig = await AddNewPicture();
        }
        
        BindingContext = new ViewModel.EditPictureVM(this, pictureConfig, photoConfigRepository);
        
        DisplayTime.Value = pictureConfig.DisplayDuration;
        FadeTime.Value = pictureConfig.FadeTime;
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
            pictureConfig = new PictureConfig(photo, 1, 1);
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

        if (displayTime <= 0)
            displayTime = 1;

        DisplayTimeLabel.Text = "Display secs: " + displayTime.ToString("D1");

        if (BindingContext is ViewModel.EditPictureVM editPictureVM)
        {
            editPictureVM.PictureConfig.DisplayDuration = displayTime;
        }
    }

    private void Fader_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        uint timeSeconds = (uint)e.NewValue;

        if (timeSeconds <= 0)
            timeSeconds = 1;

        TransitionLabel.Text = "Transition secs: " + timeSeconds.ToString("D1");

        if (BindingContext is ViewModel.EditPictureVM editPictureVM)
        {
            editPictureVM.PictureConfig.FadeTime = timeSeconds;
        }
    }

}