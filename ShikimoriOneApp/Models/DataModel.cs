using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShikimoriOneApp.Models
{
    public class DataModel:ShikimoriSharp.Bases.AnimeMangaBase,IDisposable
    {
        public long? Episodes { get; set; }
        public long EpisodesAired { get; set; }

        
        public string? Title {
            get { return Name; }
        }

        private ImageModel? _Image;
        
        private static HttpClient _Httpclient=new HttpClient();
        public  ImageModel? ImageModel { get => _Image;  set { _Image = value; } }
        public DataModel(ShikimoriSharp.Classes.Anime anime)
        {
            this.AiredOn = anime.AiredOn;
            this.Episodes = anime.Episodes;
            this.EpisodesAired = anime.EpisodesAired;
            this.Id = anime.Id;
            this.Image = anime.Image;
            this.Kind = anime.Kind;
            this.Name = anime.Name;
            this.ReleasedOn = anime.ReleasedOn;

            if( anime.Russian is not null)
                this.Russian = anime.Russian;

            this.Score = anime.Score;
            this.Status = anime.Status;
            this.Url = anime.Url;

            ImageModel = new ImageModel()
            {
                Url = "https://shikimori.one/"+this.Image?.Original
            };
        }
        public DataModel(ShikimoriSharp.Classes.Manga manga)
        {
            
            this.AiredOn = manga.AiredOn;
            this.Id = manga.Id;
            this.Image = manga.Image;
            this.Kind = manga.Kind;
            this.Name = manga.Name;
            this.ReleasedOn = manga.ReleasedOn;

            if (manga.Russian is not null)
                this.Russian = manga.Russian;

            this.Score = manga.Score;
            this.Status = manga.Status;
            this.Url = manga.Url;

            ImageModel = new ImageModel()
            {
                Url = "https://shikimori.one"+this.Image?.Original
            };
        }
        public void Dispose()
        {
            
        }
    }
}
