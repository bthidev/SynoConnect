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
    public class LoginView : ReactiveUserControl<LoginViewModel>
    {
        readonly Translattor Translattor = Locator.Current.GetService<Translattor>();
        public LoginView()
        {
            this.WhenActivated(async disposables => { await ViewModel.TryLogUser(); });
            AvaloniaXamlLoader.Load(this);
            this.FindControl<TextBlock>("InfoText").Text = Translattor.GetTranslatte("LoginView/InfoServer");
            this.FindControl<TextBox>("UserName").Watermark = Translattor.GetTranslatte("LoginView/UserName");
            this.FindControl<TextBox>("Password").Watermark = Translattor.GetTranslatte("LoginView/Password");
            this.FindControl<TextBlock>("LoginButton").Text = Translattor.GetTranslatte("LoginView/LoginButton");
            this.FindControl<TextBlock>("StayLog").Text = Translattor.GetTranslatte("LoginView/StayLog");
        }

    }
}