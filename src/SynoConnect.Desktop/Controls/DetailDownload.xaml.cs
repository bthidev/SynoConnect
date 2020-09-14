using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Splat;
using SynoConnect.Translatte;
using Synology.DownloadStation.Task.Results;

namespace SynoConnect.Desktop.Controls
{

    public class DetailDownload : ReactiveUserControl<ITaskFileResult>
    {
        readonly Translattor Translattor = Locator.Current.GetService<Translattor>();

        public DetailDownload()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.FindControl<TabItem>("General").Header = Translattor.GetTranslatte("DetailDownload/General");
            this.FindControl<TextBlock>("FileName").Text = Translattor.GetTranslatte("DetailDownload/FileName");
            this.FindControl<TextBlock>("Type").Text = Translattor.GetTranslatte("DetailDownload/Type");
            this.FindControl<TextBlock>("Destination").Text = Translattor.GetTranslatte("DetailDownload/Destination");
            this.FindControl<TextBlock>("FileSize").Text = Translattor.GetTranslatte("DetailDownload/FileSize");
            this.FindControl<TextBlock>("UserName").Text = Translattor.GetTranslatte("DetailDownload/UserName");
            this.FindControl<TextBlock>("CreateDate").Text = Translattor.GetTranslatte("DetailDownload/CreateDate");
            this.FindControl<TextBlock>("WaitingTime").Text = Translattor.GetTranslatte("DetailDownload/WaitingTime");

            this.FindControl<TabItem>("Transfert").Header = Translattor.GetTranslatte("DetailDownload/Transfert");
            this.FindControl<TextBlock>("Status").Text = Translattor.GetTranslatte("DetailDownload/Status");
            this.FindControl<TextBlock>("Uploaded").Text = Translattor.GetTranslatte("DetailDownload/Uploaded");
            this.FindControl<TextBlock>("Progress").Text = Translattor.GetTranslatte("DetailDownload/Progress");
            this.FindControl<TextBlock>("Speed").Text = Translattor.GetTranslatte("DetailDownload/Speed");
            this.FindControl<TextBlock>("Pair").Text = Translattor.GetTranslatte("DetailDownload/Pair");

            this.FindControl<TabItem>("Tracker").Header = Translattor.GetTranslatte("DetailDownload/Tracker");
            System.Collections.ObjectModel.ObservableCollection<DataGridColumn> column = this.FindControl<DataGrid>("TrackerList").Columns;

            column[0].Header = Translattor.GetTranslatte("DetailDownload/Url");
            column[1].Header = Translattor.GetTranslatte("DetailDownload/TrackerStatus");
            column[2].Header = Translattor.GetTranslatte("DetailDownload/UpdateIn");
            column[3].Header = Translattor.GetTranslatte("DetailDownload/Seed");
            column[4].Header = Translattor.GetTranslatte("DetailDownload/PeerNumber");

            this.FindControl<TabItem>("PairHeader").Header = Translattor.GetTranslatte("DetailDownload/PairHeader");
            column = this.FindControl<DataGrid>("PairList").Columns;

            column[0].Header = Translattor.GetTranslatte("DetailDownload/IPPeer");
            column[1].Header = Translattor.GetTranslatte("DetailDownload/Client");
            column[2].Header = Translattor.GetTranslatte("DetailDownload/PairProgress");
            column[3].Header = Translattor.GetTranslatte("DetailDownload/PairUploadSpeed");
            column[4].Header = Translattor.GetTranslatte("DetailDownload/PairDownloadSpeed");

            this.FindControl<TabItem>("FileHeader").Header = Translattor.GetTranslatte("DetailDownload/FileHeader");
            column = this.FindControl<DataGrid>("FileList").Columns;

            column[0].Header = Translattor.GetTranslatte("DetailDownload/UnitFileName");
            column[1].Header = Translattor.GetTranslatte("DetailDownload/UnitFileSize");
            column[2].Header = Translattor.GetTranslatte("DetailDownload/UnitFileDownloaded");
            column[3].Header = Translattor.GetTranslatte("DetailDownload/UnitFileProgress");
            column[4].Header = Translattor.GetTranslatte("DetailDownload/Priority");
        }
    }
}
