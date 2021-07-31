using ReactiveUI;
using ShikimoriOneApp.Filters;
using ShikimoriOneApp.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Avalonia.Input;

namespace ShikimoriOneApp.ViewModels.Filters
{
    public partial class FilterBase : ViewModelBase
    {
        public delegate void filterChanged();
        public event filterChanged? FilterChanged;
        public List<IFilterObject> ActiveFilters = new List<IFilterObject>();

        private int _SelectedOrderIndex;
        private bool _ClearFiltersButtonIsVisible = false;
        private bool _RatingIsVisible = false;

        private FiltersDataModel? _FiltersDataModel = null;

        public bool RatingIsVisible
        {
            get => _RatingIsVisible;
            set
            {
                _RatingIsVisible = value;
            }
        }
        public bool ClearFiltersButtonIsVisible
        {
            get => _ClearFiltersButtonIsVisible;
            set => this.RaiseAndSetIfChanged(ref _ClearFiltersButtonIsVisible, value);
        }

        public FiltersDataModel? FiltersData
        {
            get{
                if(_FiltersDataModel is null)
                  _FiltersDataModel= JsonConvert.DeserializeObject<FiltersDataModel>(File.ReadAllText("FiltersData.json"));
                
                return _FiltersDataModel;
            }
        }

        public int SelectedOrderIndex
        {
            get => _SelectedOrderIndex;

            set
            {
                if (value > -1)
                {
                    if(Order is not null){

                    Order[_SelectedOrderIndex].SetEventIsEnable(false);
                    Order[_SelectedOrderIndex].Active=false;

                    ActiveFilters.Remove(Order[_SelectedOrderIndex]);

                    this.RaiseAndSetIfChanged(ref _SelectedOrderIndex, value);

                    Order[value].SetEventIsEnable();
                    Order[value].Active = true;

                    if (value > 0)
                        ClearFiltersButtonIsVisible = true;
                    }
                }
            }
        }

        public List<IFilterObject>? Status { get; set; }
        public List<IFilterObject>? Kind { get; set; }
        public List<IFilterObject>? Order { get; set; }
        public List<IFilterObject>? Genres { get; set; }
        public List<IFilterObject>? Rating { get; set; }
        public List<IFilterObject>? Seasons { get; set; }


        public IReactiveCommand? ClearFiltersCommand { get; set; }
        public  void LoadData(string typeName) {

            Kind = FiltersData?.Kind?.GetFilterObjectsFromType(typeName);
            Status = FiltersData?.Status?.GetFilterObjectsFromType(typeName);
            Order = FiltersData?.Order?.GetFilterObjectsFromType(typeName);
            Genres = FiltersData?.Genres?.GetFilterObjectsFromType(typeName);
            Rating=FiltersData?.Rating?.ToList<IFilterObject>();
            Seasons = FiltersData?.Seasons?.ToList<IFilterObject>();
           
        }
        
        public void Subscribe()
        {
            var action=new System.Action<IFilterObject>((x)=>x.FilterActiveEvent+=FilterEventHandler);
            Status?.ForEach(action);
            Genres?.ForEach(action);
            Order?.ForEach(action);
            Kind?.ForEach(action);  
            Rating?.ForEach(action);
            Seasons?.ForEach(action);
        }
        

        public void FilterEventHandler(IFilterObject obj)
        {
            if (obj.Active)
                ActiveFilters.Add(obj);

            else ActiveFilters.Remove(obj);

            if (ActiveFilters.Count > 1)
                ClearFiltersButtonIsVisible = true;

            else ClearFiltersButtonIsVisible = false;

            FilterChanged?.Invoke();
        }

        public void ClearFilters()
        {

            EnableOrDisableEvents(false);

            ActiveFilters.ResetActive();
            ActiveFilters.Clear();

            ClearFiltersButtonIsVisible = false;

            EnableOrDisableEvents(true);

            SelectedOrderIndex = 0;
        }

        public void EnableOrDisableEvents(bool val)
        {
            Kind?.SetEventIsEnable(val);
            Status?.SetEventIsEnable(val);
            Order?.SetEventIsEnable(val);
            Genres?.SetEventIsEnable(val);
            Rating?.SetEventIsEnable(val);
            Seasons?.SetEventIsEnable(val);

        }

 


        public FilterBase () => ClearFiltersCommand = ReactiveCommand.Create(() => ClearFilters());


    }
}