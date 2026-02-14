using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MySlideShow.ViewModel;
using MySlideShow;
using Microsoft.VisualBasic;

namespace MySlideShow
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<Interfaces.IPhotoConfigRepository, Services.PhotoConfigRepository>();

#if ANDROID
            builder.Services.AddTransient<Interfaces.ICameraPhotoService,  CameraPhotoService>();
            builder.Services.AddTransient<Interfaces.IMultiPhotoPicker, MultiPhotoPicker>();
#endif           

            builder.Services.AddTransient<SlideShow>();

            return builder.Build();
        }
    }
}
