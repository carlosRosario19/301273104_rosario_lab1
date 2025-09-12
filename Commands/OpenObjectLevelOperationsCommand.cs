using _301273104_rosario_lab1.Factories;
using _301273104_rosario_lab1.Views;
using System.Windows;

namespace _301273104_rosario_lab1.Commands
{
    public class OpenObjectLevelOperationsCommand : CommandBase
    {
        private readonly IWindowFactory _windowFactory;

        public OpenObjectLevelOperationsCommand(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }
        public override void Execute(object? parameter)
        {
            var window = _windowFactory.Create<ObjectLevelOperationsWindow>();
            window.Show();

            Application.Current.Windows
                .OfType<MainWindow>()
                .FirstOrDefault()?
                .Close();
        }
    }
}
