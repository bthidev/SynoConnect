using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using SynoConnect.Back.Api;

namespace SynoConnect.Back.ViewModels
{
    public class FileExplorerViewModel : ViewModelBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly BaseSyno _syno;
        private List<string> _listFolder;
        private bool _loaddingProgresse;
        public bool LoaddingProgresse
        {
            get => _loaddingProgresse;
            set
            {
                this.RaiseAndSetIfChanged(ref _loaddingProgresse, value);
            }
        }
        public List<string> ListFolder
        {
            get => _listFolder;
            set
            {
                this.RaiseAndSetIfChanged(ref _listFolder, value);
            }
        }
        public FileExplorerViewModel()
        {
            LoaddingProgresse = true;

            serviceProvider = Locator.Current.GetService<IServiceProvider>();
            _syno = serviceProvider.GetService<BaseSyno>();

            _root = new Node();

            Items = _root.Children;
            SelectedItems = new ObservableCollection<Node>();
        }
        public async Task GetFolder()
        {
            _listFolder = await _syno.GetFolderList();
            foreach (var data in _listFolder)
            {
                _root.AddItem(data);
            }
            LoaddingProgresse = false;

        }
        public async Task AppendFolder(Node path)
        {
            if ( path.Children.Count == 0)
            {
                _listFolder = (await _syno.GetFolderList(BuildPathFromNode(path))).ToList();
                foreach (var data in _listFolder)
                {
                    path.AddItem(data);
                }
            }
        }
        /*
         * test treeview
         */
        public string BuildPathFromNode(Node node)
        {
            string pathFolder = "";
            List<string> parents = new List<string>();

            var currentParent = node.Parent;
            parents.Add(node.Header);
            while (currentParent != null)
            {
                parents.Add(currentParent.Header);
                currentParent = currentParent.Parent;
            }
            parents.Reverse();
            foreach (var data in parents)
            {
                if (data != parents.First())
                    pathFolder += data + "/";
                else
                    pathFolder += data;
            }
            if (pathFolder.EndsWith("/"))
                pathFolder = pathFolder.Remove(pathFolder.Length - 1);
            return pathFolder;
        }
        private readonly Node _root;
        public ObservableCollection<Node> Items { get; }
        public ObservableCollection<Node> SelectedItems { get; }
    }
    public class Node
    {
        private ObservableCollection<Node> _children;

        public Node()
        {
            Header = "/";
        }

        public Node(Node parent, string name)
        {
            Parent = parent;
            Header = name;
        }

        public Node Parent { get; }
        public string Header { get; }
        public bool AreChildrenInitialized => _children != null;
        public ObservableCollection<Node> Children => _children ??= CreateChildren();
        public void AddItem(string name) => Children.Add(new Node(this, name));
        public void RemoveItem(Node child) => Children.Remove(child);
        public override string ToString() => Header;
        private ObservableCollection<Node> CreateChildren()
        {
            return new ObservableCollection<Node>();
        }
    }
}
