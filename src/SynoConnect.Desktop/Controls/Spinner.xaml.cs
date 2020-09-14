using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SynoConnect.Desktop.Controls
{
    internal class Spinner : UserControl
    {
        public Spinner()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}