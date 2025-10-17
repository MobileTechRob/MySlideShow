using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using MySlideShow.DataModels;
using MySlideShow.Interfaces;

namespace MySlideShow.ViewModel
{
    public class EditPictureVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private DataModels.PictureConfig _pictureConfig;
        public DataModels.PictureConfig PictureConfig
        {
            get => _pictureConfig;
            set
            {
                if (_pictureConfig != value)
                {
                    _pictureConfig = value;
                    OnPropertyChanged();
                }
            }
        }
        public Command ChangePictureCommand { get; set; }
        public Command SaveChangesCommand { get; set; }

        private ContentPage _page;

        IPhotoConfigRepository _photoConfigRepository;

        public EditPictureVM(ContentPage page, PictureConfig pictureConfig, IPhotoConfigRepository photoConfigRepository)
        {
            _page = page;
            PictureConfig = pictureConfig;
            ChangePictureCommand = new Command(ChangePicture);
            SaveChangesCommand = new Command(SaveChanges);
            _photoConfigRepository = photoConfigRepository;
        }

        public void ChangePicture()
        {
            // Implementation for changing the picture
            
        }

        public void SaveChanges()
        {
            _photoConfigRepository.SavePhoto(PictureConfig);

            // Implementation for saving changes    
            _page.Navigation.PopAsync();
        }
    }
}

