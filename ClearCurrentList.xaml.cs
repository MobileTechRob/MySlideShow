

using MySlideShow.Interfaces;

namespace MySlideShow;

public partial class ClearCurrentList : ContentPage
{
    IPhotoConfigRepository photoConfigRepository;


    public ClearCurrentList(IPhotoConfigRepository photoConfigRepository)
	{
		InitializeComponent();

        this.photoConfigRepository = photoConfigRepository;
    }

    private void btnConfirmListDeletion_Clicked(object sender, EventArgs e)
    {
        photoConfigRepository.DeletePhotos();

        Shell.Current.GoToAsync("//MainPage");
    }
}