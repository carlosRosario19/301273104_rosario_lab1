using _301273104_rosario_lab1.Factories;
using _301273104_rosario_lab1.Models;
using _301273104_rosario_lab1.Services;
using _301273104_rosario_lab1.Stores;
using Amazon;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
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

            // 1. Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<App>() // <-- this enables dotnet user-secrets
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            // Register IAmazonS3 with credentials from configuration
            services.AddSingleton<IAmazonS3>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();

                var region = config["AWS:Region"];
                var accessKey = config["AWS:AccessKey"];
                var secretKey = config["AWS:SecretKey"];

                return new AmazonS3Client(
                    accessKey,
                    secretKey,
                    RegionEndpoint.GetBySystemName(region)
                );
            });

            // Register storage service
            services.AddSingleton<IStorageService, S3StorageService>();

            // Register Stores
            services.AddSingleton<InMemoryBucketStore>();

            // Register Models
            services.AddSingleton<CreateBucketModel>();
            services.AddSingleton<SelectedBucketModel>();
            services.AddSingleton<ObjectList>();
            services.AddSingleton<SelectedObjectModel>();

            // Register Factories
            services.AddSingleton<IWindowFactory, WindowFactory>();

            // Register Commands
            services.AddTransient<Commands.OpenBucketLevelOperationsCommand>();
            services.AddTransient<Commands.OpenObjectLevelOperationsCommand>();
            services.AddTransient<Commands.ExitCommand>();
            services.AddTransient<Commands.BackToMainWindowCommand>();
            services.AddTransient<Commands.CreateBucketCommand>();
            services.AddTransient<Commands.LoadBucketsCommand>();
            services.AddTransient<Commands.DeleteBucketCommand>();
            services.AddTransient<Commands.RefreshBucketsCommand>();
            services.AddTransient<Commands.ListObjectsCommand>();
            services.AddTransient<Commands.DeleteObjectCommand>();
            services.AddTransient<Commands.DownloadObjectCommand>();

            // Register ViewModels
            services.AddTransient<ViewModels.MainWindowViewModel>();
            services.AddTransient<ViewModels.BucketLevelOperationsViewModel>();
            services.AddTransient<ViewModels.ObjectLevelOperationsViewModel>();

            // Register Views
            services.AddTransient<Views.MainWindow>();
            services.AddTransient<Views.BucketLevelOperationsWindow>();
            services.AddTransient<Views.ObjectLevelOperationsWindow>();

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
