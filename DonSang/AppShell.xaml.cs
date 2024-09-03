using Microsoft.Extensions.DependencyInjection;

namespace DonSang
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Enregistrer les routes des pages de détail
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            // Remplacer "newevent" par le nom de la route et la page correcte
            Routing.RegisterRoute(nameof(CreateAccountPage), typeof(Views.CreateAccountPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(Views.LoginPage));
            // Vous pouvez ajouter d'autres routes ici pour d'autres pages si nécessaire
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            // Naviguer vers la page de connexion (login)
            await Current.GoToAsync("//LoginPage");
        }

        private async void NavigateToHomePage()
        {
            var homePage = App.GetService<HomePage>(); // Utiliser la méthode GetService pour accéder à HomePage
            await Navigation.PushAsync(homePage);
        }
    }
}
