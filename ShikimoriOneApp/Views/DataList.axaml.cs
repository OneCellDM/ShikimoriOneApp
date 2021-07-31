using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ShikimoriOneApp.Views
{
    public partial class DataList : UserControl
    {
        public DataList ()
        {
            InitializeComponent();
        }

        private void InitializeComponent ()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
