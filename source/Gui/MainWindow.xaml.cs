namespace Gui
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new PaletteGenerationViewModel();
        }
    }
}
