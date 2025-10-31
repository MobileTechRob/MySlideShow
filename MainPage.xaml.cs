using Microsoft.Maui.Controls.Compatibility;
using MySlideShow.Interfaces;

namespace MySlideShow
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        ViewModel.MainPageVM mainPageVM;

        public MainPage()
        {
            InitializeComponent();
            IPhotoConfigRepository photoConfigRepository = MauiProgram.CreateMauiApp().Services.GetService<Interfaces.IPhotoConfigRepository>()!;
            mainPageVM = new ViewModel.MainPageVM(this, photoConfigRepository);            
            BindingContext = mainPageVM;

            GeneratePictureButton.IsEnabled = mainPageVM.PhotosPresent;
        }

        protected override void OnAppearing()
        {                    
            base.OnAppearing();

            // Additional logic can be added here if needed when the page appears
            mainPageVM.RefreshPhotos();
            GeneratePictureButton.IsEnabled = mainPageVM.PhotosPresent;
        }
    }
}
