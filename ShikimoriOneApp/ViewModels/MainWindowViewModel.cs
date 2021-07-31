using ReactiveUI;
using ShikimoriOneApp.ViewModels.Filters;
using System.Diagnostics;

namespace ShikimoriOneApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private bool _ArticleIsVisible;
        private ArticleViewModel? _ArticleContext;
        private AnimeFiltersViewModel? _AnimeFiltersViewModel;
        private MangaFiltersViewModel? _MangaFiltersViewModel;

        private RanobeFiltersViewModel? _RanobeFiltersViewModel;

        private AnimeListControlViewModel? _AnimeListControlViewModel;

        private MangaListControlViewModel? _MangaListControlViewModel;

        private RanobeListControlViewModel? _RanobeListControlViewModel;

        private object? _Dcontext;
        private object? _FilterMenuContext;

        public Filters.RanobeFiltersViewModel? RanobeFiltersViewModel
        {
            get => _RanobeFiltersViewModel;
            set => _RanobeFiltersViewModel = value;
        }

        public Filters.AnimeFiltersViewModel? AnimeFiltersViewModel
        {
            get => _AnimeFiltersViewModel;
            set => _AnimeFiltersViewModel = value;
        }

        public Filters.MangaFiltersViewModel? MangaFiltersViewModel
        {
            get => _MangaFiltersViewModel;
            set => _MangaFiltersViewModel = value;
        }

        public bool ArticleIsVisible
        {
            get => _ArticleIsVisible;
            set => this.RaiseAndSetIfChanged(ref _ArticleIsVisible, value);
        }

        public ArticleViewModel? ArticleContext
        {
            get => _ArticleContext;
            set => this.RaiseAndSetIfChanged(ref _ArticleContext, value);
        }

        public object? FilterMenuContext
        {
            get => _FilterMenuContext;
            set => this.RaiseAndSetIfChanged(ref _FilterMenuContext, value);
        }

        public object? Dcontext
        {
            get => _Dcontext;
            set => this.RaiseAndSetIfChanged(ref _Dcontext, value);
        }

        private int _MainSelectedIndex = 1;

        public int MainMenuSelectedIndex
        {
            get => _MainSelectedIndex;
            set
            {
                MangaFiltersViewModel?.EnableOrDisableEvents(false);
                AnimeFiltersViewModel?.EnableOrDisableEvents(false);
                RanobeFiltersViewModel?.EnableOrDisableEvents(false);
                switch (value)
                {
                    case 0:
                        {
                            FilterMenuContext = MangaFiltersViewModel;
                            Dcontext = _MangaListControlViewModel;

                            MangaFiltersViewModel?.EnableOrDisableEvents(true);
                            break;
                        }
                    case 1:
                        {
                            FilterMenuContext = AnimeFiltersViewModel;
                            Dcontext = _AnimeListControlViewModel;

                            AnimeFiltersViewModel?.EnableOrDisableEvents(true);
                            break;
                        }
                    case 2:
                        {
                            FilterMenuContext = RanobeFiltersViewModel;
                            Dcontext = _RanobeListControlViewModel;

                            RanobeFiltersViewModel?.EnableOrDisableEvents(true);
                            break;
                        }
                }
                _MainSelectedIndex = value;
            }
        }

        public MainWindowViewModel() => InitViews();

        private void InitViews()
        {
            _RanobeFiltersViewModel = new();
            _MangaFiltersViewModel = new();
            _AnimeFiltersViewModel = new();

            _AnimeListControlViewModel = new(_AnimeFiltersViewModel);
            _MangaListControlViewModel = new(_MangaFiltersViewModel);
            _RanobeListControlViewModel = new(_RanobeFiltersViewModel);

            FilterMenuContext = AnimeFiltersViewModel;

            Dcontext = _AnimeListControlViewModel;

            ExtensionMethods.OpenArticleEvent += OpenArticleEventHandler;
        }

        public void OpenArticleEventHandler(ArticleViewModel? Model)
        {
            if (Model is not null)
            {
                ArticleContext = Model;
                Model.CloseArticleEvent += CloseArticleEventHandler;
                ArticleIsVisible = true;
            }
            else
            {
                ArticleIsVisible = false;
                Debug.WriteLine("Article Model Is NULL");
            }
        }

        private void CloseArticleEventHandler()
        {
            if (ArticleContext is not null)
                ArticleContext.CloseArticleEvent -= CloseArticleEventHandler;

            ArticleContext = null;
            ArticleIsVisible = false;
        }
    }
}