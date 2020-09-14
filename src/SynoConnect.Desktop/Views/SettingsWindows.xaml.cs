using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;
using SynoConnect.Back.ViewModels;
using SynoConnect.Translatte;

namespace SynoConnect.Desktop.Views
{
    public class SettingsWindows : ReactiveWindow<SettingsViewModels>
    {
        readonly Translattor Translattor = Locator.Current.GetService<Translattor>();
        public SettingsWindows()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.WhenActivated(async disposables => { await ViewModel.GetSettings(); });
            AvaloniaXamlLoader.Load(this);
            DataContext = new SettingsViewModels();
            this.FindControl<TextBlock>("TMaxDL").Text = Translattor.GetTranslatte("SettingsWindows/TMaxDL");
            this.FindControl<TextBlock>("TMaxUL").Text = Translattor.GetTranslatte("SettingsWindows/TMaxUL");
            this.FindControl<TextBlock>("DefaultFolder").Text = Translattor.GetTranslatte("SettingsWindows/DefaultFolder");
            this.FindControl<TextBlock>("DefaultFolderEmule").Text = Translattor.GetTranslatte("SettingsWindows/DefaultFolderEmule");
            this.FindControl<TextBlock>("EmuleEnabled").Text = Translattor.GetTranslatte("SettingsWindows/EmuleEnabled");
            this.FindControl<TextBlock>("EmuleMaxDL").Text = Translattor.GetTranslatte("SettingsWindows/EmuleMaxDL");
            this.FindControl<TextBlock>("EmuleMaxUL").Text = Translattor.GetTranslatte("SettingsWindows/EmuleMaxUL");
            this.FindControl<TextBlock>("FTPMaxDL").Text = Translattor.GetTranslatte("SettingsWindows/FTPMaxDL");
            this.FindControl<TextBlock>("HTTPMaxDL").Text = Translattor.GetTranslatte("SettingsWindows/HTTPMaxDL");
            this.FindControl<TextBlock>("NBZMaxDL").Text = Translattor.GetTranslatte("SettingsWindows/NBZMaxDL");
            this.FindControl<TextBlock>("UnzipServiceEnabled").Text = Translattor.GetTranslatte("SettingsWindows/UnzipServiceEnabled");
            this.FindControl<TextBlock>("XunleiEnabled").Text = Translattor.GetTranslatte("SettingsWindows/XunleiEnabled");
            this.FindControl<TextBlock>("Save").Text = Translattor.GetTranslatte("SettingsWindows/Save");
            this.FindControl<Button>("SaveButton").Click += SaveDownload;
        }
        private void SaveDownload(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ViewModel.SaveCommand.Execute();
            this.Close();
        }
    }
}
