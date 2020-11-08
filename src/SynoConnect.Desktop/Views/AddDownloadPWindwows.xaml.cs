using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;
using SynoConnect.Back.ViewModels;
using SynoConnect.Translatte;
using System.Linq;

namespace SynoConnect.Desktop.Views
{
    public class AddDownloadPWindwows : ReactiveWindow<AddDownloadViewModel>
    {
        readonly Translattor Translattor = Locator.Current.GetService<Translattor>();
        public AddDownloadPWindwows()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.WhenActivated(async disposables => { await ViewModel.GetSettings(); });
            AvaloniaXamlLoader.Load(this);
            DataContext = new AddDownloadViewModel();
            this.FindControl<TextBlock>("Uri").Text = Translattor.GetTranslatte("AddDownloadWindwows/Uri");
            this.FindControl<TextBlock>("File").Text = Translattor.GetTranslatte("AddDownloadWindwows/File");
            this.FindControl<TextBlock>("Username").Text = Translattor.GetTranslatte("AddDownloadWindwows/Username");
            this.FindControl<TextBlock>("Password").Text = Translattor.GetTranslatte("AddDownloadWindwows/Password");
            this.FindControl<TextBlock>("UnzipPassword").Text = Translattor.GetTranslatte("AddDownloadWindwows/UnzipPassword");
            this.FindControl<TextBlock>("Destination").Text = Translattor.GetTranslatte("AddDownloadWindwows/Destination");
            this.FindControl<TextBlock>("Save").Text = Translattor.GetTranslatte("AddDownloadWindwows/Save");
            this.FindControl<Button>("ChooseFile").Click += AddDownloadPWindwows_Click;
            this.FindControl<Button>("SaveButton").Click += SaveDownload;
            this.FindControl<Button>("ExploreButton").Click += SettingsWindows_Click;
        }
        private async void SettingsWindows_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Window window = new Window();
            var test = new FolderExplorer();
            var result = await test.ShowDialog<string>(window);
            this.FindControl<TextBox>("ExploreText").Text = result;
        }
        private void SaveDownload(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ViewModel.SaveCommand.Execute();
            this.Close();
        }
        private async void AddDownloadPWindwows_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            Window window = new Window();
            string[] result = await file.ShowAsync(window);
            if (result.Length > 0)
            {
                ViewModel.DownloadModels.File = result.First();
            }
        }
    }
}
