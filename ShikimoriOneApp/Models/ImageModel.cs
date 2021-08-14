using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShikimoriOneApp.Models
{

    public class ImageModel : INotifyPropertyChanged
    {
        private string? _url;
        private Bitmap? _bitmap;
        public string? Url { get => _url; set { _url = value; LoadBitmap(_url); } }
        public Bitmap? Image { get => _bitmap; set { _bitmap = value; OnPropertyChanged(); } }

        private async Task<Stream> LoadImageStreamAsync(string url)
        {
            HttpClient _httpClient = new HttpClient();
            try{
            
                var data = await _httpClient.GetByteArrayAsync(url);
                return new MemoryStream(data);
            }
            catch(Exception ex){ throw ex;}
            
            finally{ _httpClient.Dispose();}
        }

        public void LoadBitmap(string? url)
        {
            if (url is null)
                return;

            _ = Task.Run(async () =>
            {
                try
                {
                    await using (var imageStream = await LoadImageStreamAsync(url))
                    {
                        Image = Bitmap.DecodeToWidth(imageStream, 180);
                    }
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


}
