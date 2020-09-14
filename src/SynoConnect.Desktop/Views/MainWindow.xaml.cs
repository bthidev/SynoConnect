using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;
using SynoConnect.Desktop.ViewModels;
using SynoConnect.Translatte;

namespace SynoConnect.Desktop.Views
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        readonly Translattor Translattor = Locator.Current.GetService<Translattor>();
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(async disposables => { await ViewModel.Loadder(); });
            //var test = this.FindControl<TextBlock>("tb");
            //test.Text = Translattor.GetTranslatte("MainWindows/Loading");

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}