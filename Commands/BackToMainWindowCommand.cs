using _301273104_rosario_lab1.Factories;
using _301273104_rosario_lab1.Views;
using System.Windows;

namespace _301273104_rosario_lab1.Commands
{
    public class BackToMainWindowCommand : CommandBase
    {
        private readonly IWindowFactory _windowFactory;

        public BackToMainWindowCommand(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }
        public override void Execute(object? parameter)
        {
            // Resolve the main window from DI
            var mainWindow = _windowFactory.Create<MainWindow>();
            mainWindow.Show();

            // Close the current window (passed as parameter)
            if (parameter is Window currentWindow)
            {
                currentWindow.Close();
            }
            else
            {
                // fallback: close any open window that's not MainWindow
                Application.Current.Windows
                    .OfType<Window>()
                    .FirstOrDefault(w => !(w is MainWindow))?
                    .Close();
            }
        }
    }
}
