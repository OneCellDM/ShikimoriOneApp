using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System.Diagnostics;

namespace ShikimoriOneApp.Views
{
    public partial class AnimeFiltersContol : UserControl
    {
        public AnimeFiltersContol()
        {
            InitializeComponent();
        }
       
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
