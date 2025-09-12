using _301273104_rosario_lab1.Commands;

namespace _301273104_rosario_lab1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public CommandBase OpenBucketLevelOperationsCommand { get; }
        public CommandBase OpenObjectLevelOperationsCommand { get; }
        public CommandBase ExitCommand { get; }

        public MainWindowViewModel(
            OpenBucketLevelOperationsCommand openBucketLevelOperationsCommand,
            OpenObjectLevelOperationsCommand openObjectLevelOperationsCommand,
            ExitCommand exitCommand)
        {
            OpenBucketLevelOperationsCommand = openBucketLevelOperationsCommand;
            OpenObjectLevelOperationsCommand = openObjectLevelOperationsCommand;
            ExitCommand = exitCommand;
        }
    }
}
