using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
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
    public class FolderExplorer : ReactiveWindow<FileExplorerViewModel>
    {
        readonly Translattor Translattor = Locator.Current.GetService<Translattor>();

        public FolderExplorer()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.WhenActivated(async disposables => { 
                await ViewModel.GetFolder();
            });
            
            AvaloniaXamlLoader.Load(this);
            DataContext = new FileExplorerViewModel();
            this.FindControl<TextBlock>("Save").Text = "save";
            this.FindControl<TreeView>("Treeview").SelectionChanged += FolderExplorer_SelectionChanged;
            this.FindControl<Button>("SaveButton").Click += FolderExplorer_Click;
        }

        private void FolderExplorer_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.Close( ViewModel.BuildPathFromNode( ViewModel.SelectedItems.FirstOrDefault() ) );
        }

        private async void FolderExplorer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await ViewModel.AppendFolder(ViewModel.SelectedItems.FirstOrDefault());
        }
    }
    public class MenuItem
    {
        public MenuItem()
        {
            this.Items = new ObservableCollection<MenuItem>();
        }

        public string Title { get; set; }

        public ObservableCollection<MenuItem> Items { get; set; }
    }
}