using MauiCRUD.Services;
using MauiCRUD.ViewModels;
using MauiCRUD.Views;
using Microsoft.Extensions.Logging;

namespace MauiCRUD
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Services
            builder.Services.AddSingleton<IStudentServices, StudentServices>();

            //Views and View Models
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            builder.Services.AddTransient<StudentListPage>();
            builder.Services.AddTransient<StudentListPageViewModel>();

            builder.Services.AddTransient<AddUpdateStudent>();
            builder.Services.AddTransient<AddUpdateStudentViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
