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

        public bool PhotosPresent { get { return _listOfPictures.Count > 0; } set { } } 

        //public Command AddPictureCommand { get; set; }
        public Command AddMusicCommand { get; set; }
        //public Command GenerateShowCommand { get; set; }
        public Command SelectedPictureCommand { get; set; }
        public Command DeletePictureCommand { get; set; }

        SlideShow slideShow;

        ContentPage _page;

        IPhotoConfigRepository _photoConfigRepository;
        List<PictureConfig> loadedPictures;


        public MainPageVM(ContentPage page, IPhotoConfigRepository photoConfigRepository)
        {          
            AddMusicCommand = new Command(AddMusic);    
            SelectedPictureCommand = new Command(EditPictureConfiguration);
            //GenerateShowCommand = new Command(GenerateShow);

            ListOfPictures = new ObservableCollection<DataModels.PictureConfig>();

            _photoConfigRepository = photoConfigRepository!;         
            _page = page;   

            RefreshPhotos();                      
        }

        public async void AddMusic()
        {
            // Logic to add music
            string filePath = await SelectMusicAsync();


            _page.DisplayAlert("Music Selected", $"Selected music file: {filePath}", "OK");
        }

        public void RefreshPhotos()
        {
            loadedPictures = _photoConfigRepository.LoadPhotos();          
            ListOfPictures.Clear();

            if (loadedPictures == null)
            {
                loadedPictures = new List<PictureConfig>();
                loadedPictures.Add(new PictureConfig(PhotoListState.Empty));
             
                //_photoConfigRepository.SavePhotos(LoadTempPhotos());
                //loadedPictures = _photoConfigRepository.LoadPhotos();
            }
            else
            {
                if (!_photoConfigRepository.VerifyFile())
                  return;
            }

            foreach (var pic in loadedPictures)
            {
                pic.EnableDownArrow = true;
                pic.EnableUpArrow = true;   
                ListOfPictures.Add(pic);
            }

            if (ListOfPictures.Count >= 1)
            {
                ListOfPictures[0].EnableUpArrow = false;
                ListOfPictures[ListOfPictures.Count - 1].EnableDownArrow = false;
            }
        }

        private List<PictureConfig> LoadTempPhotos()
        {
            List<PictureConfig> loadedPictures = new List<PictureConfig>();

            //loadedPictures.Add(new PictureConfig("img_one.png", 4,1));
            //loadedPictures.Add(new PictureConfig("img_two.png", 3, 2));
            //loadedPictures.Add(new PictureConfig("img_three.png", 2, 7));
            //loadedPictures.Add(new PictureConfig("img_four.png", 5,2));
            //loadedPictures.Add(new PictureConfig("img_five.png", 6, 1));
            //loadedPictures.Add(new PictureConfig("img_six.png", 7, 3));
            //loadedPictures.Add(new PictureConfig("img_seven.png", 8, 1));
            //loadedPictures.Add(new PictureConfig("img_eight.png", 9, 4));
            //loadedPictures.Add(new PictureConfig("img_nine.png", 4, 5));
            //loadedPictures.Add(new PictureConfig("img_ten.png", 4, 5));
            //loadedPictures.Add(new PictureConfig("img_eleven.png", 4, 5));

            return loadedPictures;
        }

        public async void EditPictureConfiguration(object sender)
        {
            // Logic to add a picture
            PictureConfig selectedPictureConfig = (PictureConfig)sender;

            await _page.Navigation.PushAsync(new EditPicture(_page, selectedPictureConfig));
        }

        //public async void AddNewPicture()
        //{
        //    PictureConfig pictureConfig = null;
        //    // Logic to add a picture
        //    string photo = await SelectPhotoAsync();

        //    if (System.Diagnostics.Debugger.IsAttached)
        //    {
        //        // Code to run only when debugging
        //        if (string.IsNullOrEmpty(photo))
        //        {
        //            if (loadedPictures == null)
        //                photo = "img_1.png";
        //            else
        //            {
        //                int count = loadedPictures.Count + 1;
        //                photo = "img_" + count.ToString() + ".png";
        //            }
                    
        //            pictureConfig = new PictureConfig(photo);
        //        }
        //    }
        //    else if (!string.IsNullOrEmpty(photo))
        //    {
        //        // User cancelled or no photo selected
        //        pictureConfig = new PictureConfig(photo, 1, 1);
        //    }
            
        //    await _page.Navigation.PushAsync(new EditPicture(_page, pictureConfig));
        //}

        public void DeletePicture(object sender)
        {
            ImageButton button = (ImageButton)sender;
            PictureConfig pictureConfig = (PictureConfig)button.BindingContext;

            _photoConfigRepository.DeletePhoto(pictureConfig);

            RefreshPhotos();
            
            OnPropertyChanged(nameof(ListOfPictures));
        }

        public void MovePictureUp(object sender)
        {
            // Logic to move picture up in the list
            ImageButton button = (ImageButton)sender;
            PictureConfig pictureConfig = (PictureConfig)button.BindingContext;
            loadedPictures = _photoConfigRepository.LoadPhotos();

            int picIndex = loadedPictures.FindIndex((pic) => pic.Id == pictureConfig.Id);   

            if (picIndex > 0)
            {
                var temp = loadedPictures[picIndex - 1];
                loadedPictures[picIndex - 1] = loadedPictures[picIndex];
                loadedPictures[picIndex] = temp;
                _photoConfigRepository.SavePhotos(loadedPictures);
                RefreshPhotos();
            }
        }

        public void MovePictureDown(object sender)
        {
            // Logic to move picture down in the list
            ImageButton button = (ImageButton)sender;
            PictureConfig pictureConfig = (PictureConfig)button.BindingContext;
            loadedPictures = _photoConfigRepository.LoadPhotos();

            int picIndex = loadedPictures.FindIndex((pic) => pic.Id == pictureConfig.Id);

            if (picIndex < loadedPictures.Count - 1)
            {
                var temp = loadedPictures[picIndex + 1];
                loadedPictures[picIndex + 1] = loadedPictures[picIndex];
                loadedPictures[picIndex] = temp;
                _photoConfigRepository.SavePhotos(loadedPictures);
                RefreshPhotos();
            }
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

        private async Task<string> SelectMusicAsync()
        {
            string localFilePath = string.Empty;

            var customAudioFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "audio/*" } }
            });

            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select an audio file",
                    FileTypes = customAudioFileType
                });

                if (result != null)
                    return result.FullPath;
            }
            catch (Exception ex)
            {
                // Handle exceptions (permissions, not supported, etc.)
            }

            return localFilePath;
        }
    }
}
