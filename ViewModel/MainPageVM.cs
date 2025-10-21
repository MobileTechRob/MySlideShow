using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using MySlideShow.DataModels;
using MySlideShow.Interfaces;

namespace MySlideShow.ViewModel
{
    public class MainPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string? _exampleProperty;
        public string? ExampleProperty
        {
            get => _exampleProperty;
            set
            {
                if (_exampleProperty != value)
                {
                    _exampleProperty = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<DataModels.PictureConfig> _listOfPictures;
        public ObservableCollection<DataModels.PictureConfig> ListOfPictures
        {
            get => _listOfPictures;
            set
            {
                if (_listOfPictures != value)
                {
                    _listOfPictures = value;
                    OnPropertyChanged();
                }
            }
        }

        private string photo = "";

        public Command AddPictureCommand { get; set; }
        public Command GenerateShowCommand { get; set; }
        public Command SelectedPictureCommand { get; set; }

        ContentPage _page;

        IPhotoConfigRepository _photoConfigRepository;

        public MainPageVM(ContentPage page, IPhotoConfigRepository photoConfigRepository)
        {
            AddPictureCommand = new Command(AddNewPicture);
            SelectedPictureCommand = new Command(ShowPictureConfiguration);
            GenerateShowCommand = new Command(GenerateShow);
            ListOfPictures = new ObservableCollection<DataModels.PictureConfig>();

            _photoConfigRepository = photoConfigRepository!;         
            _page = page;   

            RefreshPhotos();
        }

        public void RefreshPhotos()
        {
            List<PictureConfig> loadedPictures = _photoConfigRepository.LoadPhotos();

            ListOfPictures.Clear();

            if (loadedPictures == null)
            {
                return;
            }

            foreach (var pic in loadedPictures)
            {
                ListOfPictures.Add(pic);
            }   
        }

        public async void ShowPictureConfiguration(object sender)
        {
            // Logic to add a picture
            PictureConfig selectedPictureConfig = (PictureConfig)sender;

            await _page.Navigation.PushAsync(new EditPicture(_page, selectedPictureConfig));
        }

        public async void AddNewPicture()
        {
            // Logic to add a picture
            photo = await SelectPhotoAsync();

            PictureConfig pictureConfig = new PictureConfig(photo, 5);

            await _page.Navigation.PushAsync(new EditPicture(_page, pictureConfig));
        }

        private void PagePopped(object? sender, NavigationEventArgs args)
        {
            PictureConfig pictureConfig = new PictureConfig(photo, 5);

            // Handle any actions needed after returning from EditPicture
            ListOfPictures.Add(pictureConfig);
        }

        public void GenerateShow()
        {
            // Logic to generate the slideshow
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
    }
}
