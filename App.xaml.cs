using _301273104_rosario_lab1.Factories;
using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace _301273104_rosario_lab1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            // Register ViewModels
            services.AddTransient<ViewModels.MainWindowViewModel>();
            services.AddTransient<ViewModels.BucketLevelOperationsViewModel>();
            services.AddTransient<ViewModels.ObjectLevelOperationsViewModel>();

            // Register Commands
            services.AddTransient<Commands.OpenBucketLevelOperationsCommand>();
            services.AddTransient<Commands.OpenObjectLevelOperationsCommand>();
            services.AddTransient<Commands.ExitCommand>();
            services.AddTransient<Commands.BackToMainWindowCommand>();
            services.AddTransient<Commands.CreateBucketCommand>();
            services.AddTransient<Commands.LoadBucketsCommand>();

            // Register Views
            services.AddTransient<Views.MainWindow>();
            services.AddTransient<Views.BucketLevelOperationsWindow>();
            services.AddTransient<Views.ObjectLevelOperationsWindow>();

            // Register Models
            services.AddSingleton<CreateBucketModel>();
            services.AddSingleton<BucketListModel>();

            // Register Factories
            services.AddSingleton<IWindowFactory, WindowFactory>();

            // Register storage service
            services.AddSingleton<IStorageService, S3StorageService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Resolve MainWindow from DI
            var mainWindow = _serviceProvider.GetRequiredService<Views.MainWindow>();
            mainWindow.DataContext = _serviceProvider.GetRequiredService<ViewModels.MainWindowViewModel>();
            mainWindow.Show();
        }

    }

}
