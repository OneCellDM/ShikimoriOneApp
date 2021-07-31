﻿
using ReactiveUI;
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
    public class RanobeListControlViewModel : ViewModelBase
    {

        private Filters.RanobeFiltersViewModel? _FiltersViewModel;
        public Filters.RanobeFiltersViewModel? Filters
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
                                var Data = await ShikikomoriApiHandler.ApiClient.Ranobe.GetById((int)_SelectedModel.Id);

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
        public RanobeListControlViewModel(RanobeFiltersViewModel filters)
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
            Filters.FilterChanged += delegate () { _Page =1; Load(); };
        }

        public IReactiveCommand LoadNextPageCommand { get; set; }
        public IReactiveCommand LoadPreviousPageCommand { get; set; }
        public void Load()
        {


            IsLoadingPanelVisible = true;

            DataCollection.Clear();


            Task.Run(async () =>
             {
                 try
                 {
                     if (ShikikomoriApiHandler.ApiClient is not null&&Filters is not null)
                     {
                         
                         var res = await ShikikomoriApiHandler.ApiClient.Ranobe.GetBySearch(Filters.GetRequestSettings(_Page));
                         DataCollection.AddRange(res);
                     }
                 }
                 catch (Exception ex) { Debug.WriteLine(ex); }

                 IsLoadingPanelVisible = false;

             });

        }


    }
}
