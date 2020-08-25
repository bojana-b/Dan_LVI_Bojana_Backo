using System.Windows;
using Wpf_html_download.ViewModel;

namespace Wpf_html_download.View
{
    /// <summary>
    /// Interaction logic for DownloadHTMLView.xaml
    /// </summary>
    public partial class DownloadHTMLView : Window
    {
        public DownloadHTMLView()
        {
            InitializeComponent();
            this.DataContext = new DownloadHTMLViewModel(this);
        }
    }
}
