using ReactiveUI;
using ShikimoriOneApp.Models;
using ShikimoriSharp.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace ShikimoriOneApp.ViewModels
{

    public class ArticleViewModel : ViewModelBase
    {
        public delegate void CloseActicle();
        public event CloseActicle? CloseArticleEvent;
       
        private List<string>? _OtherTitles;
        private List<ImageModel>? _ScreenShots;
        private List<ImageModel>? _Videos;
        private ImageModel? _Poster;   
        public ImageModel? Poster
        {
            get
            {
                if (_Poster is null)
                {
                    string? url = null;
                    if (Article is AnimeID)
                    {
                        var anime = Article as AnimeID;
                        url = anime?.Image?.Original;
                    }

                    if (Article is MangaID)
                    {
                        var manga = Article as MangaID;
                        url = manga?.Image?.Original;
                    }

                    if (url != null)
                    {
                        _Poster = new ImageModel()
                        {
                            Url = "https://shikimori.one/"+url,
                        };
                    }
                }

                return _Poster;
            }
        }
        public bool DescriptionIsVisible
        {
            get
            {
                if (Article is AnimeID)
                {
                    var anime = Article as AnimeID;
                    return anime?.Description is not null;
                }
                if (Article is MangaID)
                {
                    var manga = Article as MangaID;
                    return manga?.Description is not null;
                }
                return false;
            }
        }
        public bool OtherTitlesIsvisible { get => (OtherTitles?.Count > 0); }
        public bool ScreeenShotsIsvisible { get => (ScreenShots?.Count > 0); }
        public bool VideosIsvisible { get => Videos?.Count > 0; }
       
        public List<string>? OtherTitles
        {
            get
            {
                if (_OtherTitles is null)
                {
                    _OtherTitles = new List<string>();
                    if (Article is AnimeID)
                    {
                        var anime = Article as AnimeID;
                        if (anime?.Russian is not null && anime.Russian?.Length > 0)
                            _OtherTitles?.Add(anime.Russian);

                        if (anime?.English is not null)
                            _OtherTitles?.AddRange(anime.English.Where(x=> x is not null));

                        if (anime?.Japanese is not null)
                            _OtherTitles?.AddRange(anime.Japanese.Where(x => x is not null));

                        if (anime?.Synonyms is not null)
                            _OtherTitles?.AddRange(anime.Synonyms.Where(x => x is not null));
                    }

                    if (Article is MangaID)
                    {
                        var manga = Article as MangaID;
                        if (manga?.Russian is not null && manga.Russian?.Length > 0)
                            _OtherTitles?.Add(manga.Russian);

                        if (manga?.English is not null)
                            _OtherTitles?.AddRange(manga.English.Where(x => x is not null));

                        if (manga?.Japanese is not null)
                            _OtherTitles?.AddRange(manga.Japanese.Where(x => x is not null));

                        if (manga?.Synonyms is not null)
                            _OtherTitles?.AddRange(manga.Synonyms.Where(x => x is not null));
                    }
                }
                return _OtherTitles;
            }
        }
        public List<ImageModel> Videos
        {
            get
            {

                if (_Videos is null)
                {
                    _Videos = new List<ImageModel>();
                    if (Article is AnimeID)
                    {
                        var anime = Article as AnimeID;
                        if(anime is not null)
                            foreach (var video in anime.Videos)
                                _Videos.Add(new ImageModel() { Url = video.ImageUrl.AbsoluteUri });
                    }

                }
                return _Videos;
            }
        }

        public List<ImageModel> ScreenShots
        {
            get {

                if (_ScreenShots is null)
                {
                    _ScreenShots = new List<ImageModel>();
                    if (Article is AnimeID)
                    {
                        var anime = Article as AnimeID;

                        if(anime is not null)
                        foreach(var screenshot in anime.Screens)
                            _ScreenShots.Add(new ImageModel() { Url = "https://shikimori.one/"+screenshot.Preview });              
                    }

                }
                return _ScreenShots;
            }
        }

        public string? ArticleTitle
        {
            get
            {
                if (Article is AnimeID)
                {
                    var anime = Article as AnimeID;
                    return string.Format("{0} / {1}", anime?.Name, anime?.Russian);
                }

                if (Article is MangaID)
                {
                    var manga = Article as MangaID;
                    return string.Format("{0} / {1}", manga?.Name, manga?.Russian);
                }
                return "";
            }
        }

        public string? Title
        {
            get
            {
                if (Article is AnimeID)
                {
                    var anime = Article as AnimeID;

                    return string.Format("Название:{0}", anime?.Name);
                }

                if (Article is MangaID)
                {
                    var manga = Article as MangaID;
                  
                    return string.Format("Название:{0}", manga?.Name);
                }
                return "";
            }
        }

        public object? Article { get; set; }

        public IReactiveCommand? CloseCommand { get; set; } 

        public void init()
        {
            CloseCommand = ReactiveCommand.Create(() =>CloseArticleEvent?.Invoke()); 
           
        }
        public ArticleViewModel(AnimeID animeID)
        {
            Article = animeID;
            init();
        }

        public ArticleViewModel(MangaID mangaID) {
            Article = mangaID;
            init();
        }
    }
}