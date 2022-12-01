using UraniumUI.Material.Resources;

namespace Uranium
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}