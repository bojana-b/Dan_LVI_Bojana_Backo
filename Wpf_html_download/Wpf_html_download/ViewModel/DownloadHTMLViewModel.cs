using Wpf_html_download.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.IO.Compression;
using Wpf_html_download.Commands;
using System.IO.Packaging;

namespace Wpf_html_download.ViewModel
{
    class DownloadHTMLViewModel : ViewModelBase
    {
        DownloadHTMLView downloadHTML;

        public DownloadHTMLViewModel(DownloadHTMLView downloadHTMLOpen)
        {
            downloadHTML = downloadHTMLOpen;
        }

        #region Properties
        private string html;
        public string HTML
        {
            get 
            { 
                return html;
            }
            set
            {
                html = value;
                OnPropertyChanged("HTML");
            }
        }
        #endregion

        #region Commands
        // Close Button
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }
        private void CloseExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to close?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    downloadHTML.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCloseExecute()
        {
            return true;
        }

        // Download HTML button
        private ICommand download;
        public ICommand Download
        {
            get
            {
                if (download == null)
                {
                    download = new RelayCommand(param => DownloadExecute(), param => CanDownloadExecute());
                }
                return download;
            }
        }

        private void DownloadExecute()
        {
            Random random = new Random();
            int rnd = random.Next(1, 10000);
            string page = "page" + rnd.ToString();
            string HtmlFolder = @"..\..\Downloads";
            StringBuilder file = new StringBuilder();
            file.Append(HtmlFolder);
            file.Append("\\");
            file.Append(page);
            file.Append(".html");
            string HTMLPath = file.ToString();
            try
            {
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        if (!Directory.Exists(HtmlFolder))
                        {
                            Directory.CreateDirectory(HtmlFolder);
                        }
                        client.DownloadFile(HTML, HTMLPath);
                        MessageBox.Show("File downloaded", "Success!");
                    }
                    catch (WebException e)
                    {
                        MessageBox.Show("Html link is not valid!", "Warning!");
                        return;
                        throw e;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanDownloadExecute()
        {
            if (string.IsNullOrEmpty(HTML))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand zip;
        public ICommand Zip
        {
            get
            {
                if (zip == null)
                {
                    zip = new RelayCommand(param => ZipExecute(), param => CanZipExecute());
                }
                return zip;
            }
        }

        private void ZipExecute()
        {
            try
            {
                string HtmlFolder = @"..\..\Downloads";
                string ZipFolder = @"..\..\Zipped.zip";
                File.Delete(ZipFolder);
                ZipFile.CreateFromDirectory(HtmlFolder, @"..\..\Zipped.zip");
                MessageBox.Show("File zipped", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanZipExecute()
        {
            return true;
        }
        #endregion

    }
}
