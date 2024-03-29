﻿using ReactiveUI;
using ShikimoriOneApp.Filters;
using ShikimoriOneApp.ViewModels.Filters;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ShikimoriOneApp.ViewModels
{
	public class MangaListControlViewModel : ViewModelBase
	{
		private Filters.MangaFiltersViewModel? _FiltersViewModel;

		public Filters.MangaFiltersViewModel? Filters
		{
			get => _FiltersViewModel;
			set => _FiltersViewModel = value;
		}

		public ObservableCollection<Models.DataModel> DataCollection { get; set; } = new System.Collections.ObjectModel.ObservableCollection<Models.DataModel>();

		private int _Page = 1;

		private bool _IsLoadingPanelVisible;

		private Models.DataModel? _SelectedModel;

		public Models.DataModel? SelectedModel
		{
			get => _SelectedModel;
			set
			{
				_SelectedModel = value;
				if (_SelectedModel is not null)
					Task.Run(async () =>
					{
						try
						{
							if (ShikikomoriApiHandler.ApiClient is not null && _SelectedModel.Id is not null)
							{
								ExtensionMethods.CallOpenArticleEvent(new ArticleViewModel(await ShikikomoriApiHandler.ApiClient.Mangas.GetById((long)_SelectedModel.Id)));
							}
						}
						catch (Exception ex)
						{
							Debug.WriteLine(ex);
						}
					});
			}
		}

		public bool IsLoadingPanelVisible { get => _IsLoadingPanelVisible; set => this.RaiseAndSetIfChanged(ref _IsLoadingPanelVisible, value); }

		public MangaListControlViewModel(MangaFiltersViewModel filters)
		{
			Filters = filters;
			Load();
			LoadPreviousPageCommand = ReactiveCommand.Create(() =>
			{
				if (_Page <= 1)
					return;

				_Page += -1;

				Load();
			});
			LoadNextPageCommand = ReactiveCommand.Create(() =>
			{
				_Page += 1;
				Load();
			});
			Filters.FilterChanged += delegate () { _Page = 1; Load(); };
		}

		public IReactiveCommand LoadNextPageCommand { get; set; }
		public IReactiveCommand LoadPreviousPageCommand { get; set; }

		public void Load()
		{
			if (!IsLoadingPanelVisible)
			{
				IsLoadingPanelVisible = true;

				DataCollection.Clear();

				try
				{
					if (ShikikomoriApiHandler.ApiClient is not null && Filters is not null)
					{
						var res = ShikikomoriApiHandler.ApiClient.Mangas.GetBySearch(Filters.GetRequestSettings(_Page)).GetAwaiter();
						res.OnCompleted(() =>
						{
							DataCollection.AddRange(res.GetResult());
						});
					}
				}
				catch (Exception ex) { Debug.WriteLine(ex); }

				IsLoadingPanelVisible = false;
			}
		}
	}
}