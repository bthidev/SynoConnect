using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ReactiveUI;
using Splat;
using SynoConnect.Back.ViewModels;
using SynoConnect.Translatte;

namespace SynoConnect.Desktop.Views
{
    public class DownloadStationView : ReactiveUserControl<DownloadStationViewModel>
    {
        readonly Translattor Translattor = Locator.Current.GetService<Translattor>();
        DispatcherTimer timer;
        public DownloadStationView()
        {
            this.WhenActivated(async disposables =>
            {
                await ViewModel.RefreshDownload();
                timer.Start();
            });
            AvaloniaXamlLoader.Load(this);
            this.FindControl<TextBlock>("AllDL").Text = Translattor.GetTranslatte("DownloadStationView/AllDl");
            this.FindControl<TextBlock>("DL").Text = Translattor.GetTranslatte("DownloadStationView/DL");
            this.FindControl<TextBlock>("Finish").Text = Translattor.GetTranslatte("DownloadStationView/Finish");
            this.FindControl<TextBlock>("Running").Text = Translattor.GetTranslatte("DownloadStationView/Running");
            this.FindControl<TextBlock>("NotRunning").Text = Translattor.GetTranslatte("DownloadStationView/NotRunning");
            this.FindControl<TextBlock>("Stopped").Text = Translattor.GetTranslatte("DownloadStationView/Stopped");
            System.Collections.ObjectModel.ObservableCollection<DataGridColumn> column = this.FindControl<DataGrid>("ListDL").Columns;

            column[0].Header = Translattor.GetTranslatte("DownloadStationView/FileName");
            column[1].Header = Translattor.GetTranslatte("DownloadStationView/FileSize");
            column[2].Header = Translattor.GetTranslatte("DownloadStationView/Downloaded");
            column[3].Header = Translattor.GetTranslatte("DownloadStationView/DownloadedSpeed");
            column[4].Header = Translattor.GetTranslatte("DownloadStationView/Status");
            this.FindControl<Button>("Settings").Click += DownloadStationView_Click;
            this.FindControl<Button>("AddDownload").Click += DownloadStationView_Click1;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += new EventHandler(this.InvalidateSampleData);
        }

        private async void InvalidateSampleData(object state, EventArgs e)
        {
            timer.Stop();
            await ViewModel.RefreshDownload();
            timer.Start();
        }

        private async void DownloadStationView_Click1(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddDownloadPWindwows settings = new AddDownloadPWindwows();
            Window window = new Window();
            await settings.ShowDialog(window);
        }

        private async void DownloadStationView_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SettingsWindows settings = new SettingsWindows();
            Window window = new Window();
            await settings.ShowDialog(window);
        }
    }
}