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
        }

        protected override void OnAppearing()
        {                    
            base.OnAppearing();

            // Additional logic can be added here if needed when the page appears
            mainPageVM.RefreshPhotos();           
        }

        private void DeleteImageButton_Clicked(object sender, EventArgs e)
        {
            mainPageVM.DeletePicture(sender);            
        }

        private void btnMoveUp_Clicked(object sender, EventArgs e)
        {
            mainPageVM.MovePictureUp(sender);
        }

        private void btnMoveDown_Clicked(object sender, EventArgs e)
        {
            mainPageVM.MovePictureDown(sender); 
        }
    }
}
