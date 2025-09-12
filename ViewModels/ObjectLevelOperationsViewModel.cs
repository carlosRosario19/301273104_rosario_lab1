using _301273104_rosario_lab1.Commands;

namespace _301273104_rosario_lab1.ViewModels
{
    public class ObjectLevelOperationsViewModel : ViewModelBase
    {
        public CommandBase BackToMainWindowCommand { get; }

        public ObjectLevelOperationsViewModel(
            BackToMainWindowCommand backToMainWindowCommand)
        {
            BackToMainWindowCommand = backToMainWindowCommand;
        }
    }
}
