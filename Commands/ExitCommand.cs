using System.Windows;

namespace _301273104_rosario_lab1.Commands
{
    public class ExitCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
