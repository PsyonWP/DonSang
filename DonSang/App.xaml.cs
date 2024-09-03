using Microsoft.Extensions.DependencyInjection;

namespace DonSang
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        public static T GetService<T>() where T : class
        {
            return Current.Handler.MauiContext.Services.GetService<T>();
        }
    }
}
