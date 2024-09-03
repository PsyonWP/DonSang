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

        // Méthode statique pour accéder aux services
        public static T GetService<T>() where T : class
        {
            return Current.Handler.MauiContext.Services.GetService<T>();
        }
    }
}
