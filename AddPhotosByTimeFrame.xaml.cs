using MySlideShow.DataModels;
using MySlideShow.Interfaces;
using MySlideShow.Utilities;
using System.ComponentModel.Design;

namespace MySlideShow;

public partial class AddPhotosByTimeFrame : ContentPage
{
    
    private int quantityOfSpecificTimeUnit;

    string localFilePath = "StringNotEMpty";

    IPhotoConfigRepository photoConfigRepository;   

    private SortOrder sortOrder = SortOrder.Descending;

    public AddPhotosByTimeFrame()
	{
		InitializeComponent();

        radioButtonMinute.IsChecked = true;

        photoConfigRepository = MauiProgram.CreateMauiApp().Services.GetService<Interfaces.IPhotoConfigRepository>()!;

        datePickerFromDate.Date = DateTime.Now;
        timePickerFromTime.Time = DateTime.Now.TimeOfDay;

        datePickerEarlierDate.IsEnabled = false;
        timePickerEarlierTime.IsEnabled = false;

        radioButtonAscending.IsChecked = false;
        radioButtonDescending.IsChecked = true;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await DisplayAlert("Permission Request", "The app needs permission to access photos to build the slideshow. Please grant permission when prompted.", "OK");

        PermissionStatus permissionStatus = await Permissions.RequestAsync<Permissions.Photos>();

        if (permissionStatus != PermissionStatus.Granted)
        {
            await DisplayAlert("Permission Denied", permissionStatus.ToString(), "OK");
            Shell.Current.GoToAsync("//MainPage");
        }
    }

    private void sliderTimeFrame_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        DisplaySliderValueText();
    }

    private void DisplaySliderValueText()
    {
        TimeFrameText.Text = String.Format("Last {0} {1}", (int)sliderTimeFrame.Value,
            radioButtonMinute.IsChecked ? "Minute(s)" :
            radioButtonHour.IsChecked ? "Hour(s)" :
            radioButtonDay.IsChecked ? "Day(s)" :
            radioButtonWeek.IsChecked ? "Week(s)" : "");

        datePickerEarlierDate.IsEnabled = false;
        timePickerEarlierTime.IsEnabled = false;

        TimeFrameText.IsEnabled = true;
        sliderTimeFrame.IsEnabled = true;
    }


    private void radioButtonMinute_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        DisplaySliderValueText();
    }

    private void radioButtonHour_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        DisplaySliderValueText();
    }

    private void radioButtonDay_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        DisplaySliderValueText();
    }

    private void radioButtonWeek_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        DisplaySliderValueText();
    }

    private void radioButtonOther_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        //DisplaySliderValueText();
        datePickerEarlierDate.IsEnabled = true;
        timePickerEarlierTime.IsEnabled = true;

        TimeFrameText.IsEnabled = false;
        sliderTimeFrame.IsEnabled = false;  
    }

    private async void btnAddPhotosGetDirectory_Clicked(object sender, EventArgs e)
    {
        try
        {
            MediaPickerOptions mediaPickerOptions = new MediaPickerOptions();
            mediaPickerOptions.Title = "Choose directory where photos are";
            FileResult fileResult = await MediaPicker.PickPhotoAsync(mediaPickerOptions);

            if (fileResult != null)
            {
                await DisplayAlert("Date Time Selected Photo", fileResult.FullPath, "OK");

                localFilePath = fileResult.FullPath.Substring(0, fileResult.FullPath.LastIndexOf('/'));
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            // Handle exceptions (permissions, not supported, etc.)
        }
    }

    private async void btnAddPhotosByTimeFrame_Clicked(object sender, EventArgs e)
    {
        Dictionary<string, DateTime> photoDateTimeDict = new Dictionary<string, DateTime>();

        Dictionary<string, DateTime> cameraPhotos = new Dictionary<string, DateTime>();

        string errorMessage = string.Empty;

        //List<CameraPhoto> photos = new List<CameraPhoto>();

        CameraPhotosAsyncResult cameraPhotosAsyncResult = new CameraPhotosAsyncResult();    

        if (!string.IsNullOrEmpty(localFilePath))
        {
            try
            {              
                ICameraPhotoService cameraPhotoService = MauiProgram.CreateMauiApp().Services.GetService<Interfaces.ICameraPhotoService>()!;
                cameraPhotosAsyncResult = await cameraPhotoService.GetCameraPhotosAsync();

                await DisplayAlert("Date Time Photo", cameraPhotosAsyncResult.info, "OK");


                //IEnumerable<string> fileList = Directory.EnumerateFiles(localFilePath);

                foreach (var fileName in cameraPhotosAsyncResult.cameraPhotos)
                {
                //    FileInfo fileInfo = new FileInfo(fileName);
                //    DateTime photoCreationTime = fileInfo.CreationTime;

                    bool result = await DisplayAlert("Date Time Photo",  fileName.Path + ",Date:" + fileName.DateTaken, "OK", "Cancel");

                    if (result == false)
                        break;

                    //photoDateTimeDict.Add(fileName, photoCreationTime);
                }
            }
            catch (Exception ex)
            {
                errorMessage= ex.ToString();
            }

            if (errorMessage != string.Empty)
            {
                await DisplayAlert("Error", errorMessage, "OK");
            }
            else
            {
                int count = photoDateTimeDict.Count;
                await DisplayAlert("Built List", $"Proceed to Generate Slide Show with {count} photos" , "OK");

                DateTime laterDate = datePickerFromDate.Date + timePickerFromTime.Time;
                DateTime earlierDate = datePickerEarlierDate.Date + timePickerEarlierTime.Time;



                //List<string> photos = SortPhotos(photoDateTimeDict, laterDate, earlierDate,
                //                        ConvertTimeUnits.UI_To_GenericTimeUnit(radioButtonMinute.IsChecked,
                //                                                                radioButtonHour.IsChecked,
                //                                                                radioButtonDay.IsChecked,
                //                                                                radioButtonWeek.IsChecked),
                //                        (int)sliderTimeFrame.Value, sortOrder);

                count = 0;

                // Save to PhotoConfig Repository
                foreach (var photo in cameraPhotosAsyncResult.cameraPhotos)
                {
                    photoConfigRepository.SavePhoto(new PictureConfig(photo.Path!));
                    count++;
                }

                await DisplayAlert("Saved List", $"Saved {count} photos", "OK");

            }

            Shell.Current.GoToAsync("//MainPage");
        }
    }

    public List<string> SortPhotos(Dictionary<string, DateTime> photoDateTimeDict, DateTime laterDate, DateTime earlierDate, TimeUnit timeUnit, int qtyTimeUnit, SortOrder sortOrder)
    {
        return SortPhotosByTimeFrame(photoDateTimeDict, laterDate, earlierDate, timeUnit, qtyTimeUnit, sortOrder);
    }

    private List<string> SortPhotosByTimeFrame(Dictionary<string, DateTime> photoDateTimeDict, DateTime laterDate, DateTime earlierDate, TimeUnit timeUnit, int qtyTimeUnit, SortOrder sortOrder)
    {
        return CollectFiles.SortPhotosByTimeFrame(photoDateTimeDict, laterDate, earlierDate, timeUnit, qtyTimeUnit, sortOrder);
    }

    private void radioButtonAscending_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        sortOrder = SortOrder.Ascending;
    }

    private void radioButtonDescending_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        sortOrder = SortOrder.Descending;
    }
}