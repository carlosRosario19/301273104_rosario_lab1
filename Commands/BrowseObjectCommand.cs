using _301273104_rosario_lab1.Models;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace _301273104_rosario_lab1.Commands
{
    public class BrowseObjectCommand : CommandBase
    {
        private readonly UploadObjectModel _uploadObjectModel;

        public BrowseObjectCommand(UploadObjectModel uploadObjectModel)
        {
            _uploadObjectModel = uploadObjectModel;
        }
        public override void Execute(object? parameter)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Title = "Select a file to upload",
                    Filter = "All files (*.*)|*.*",
                    Multiselect = false
                };

                bool? result = openFileDialog.ShowDialog();

                if (result == true)
                {
                    _uploadObjectModel.FilePath = openFileDialog.FileName;
                    _uploadObjectModel.ObjectName = Path.GetFileName(openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred while browsing for a file: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}
