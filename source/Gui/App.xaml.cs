using System.Windows;

namespace Gui
{
    public partial class App
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow
                {
                    DataContext = new PaletteGenerationViewModel()
                };

            mainWindow.Show();
        }
    }
}
