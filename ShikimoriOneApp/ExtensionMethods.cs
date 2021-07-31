using DynamicData.Kernel;
using ShikimoriOneApp.Filters;
using ShikimoriOneApp.ViewModels;
using ShikimoriOneApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShikimoriOneApp
{
    public static class ExtensionMethods
    {
        public delegate void OpenArticle( ArticleViewModel? articleView);
        public static event  OpenArticle? OpenArticleEvent;
        
        public static void CallOpenArticleEvent(ArticleViewModel? articleModel)=>OpenArticleEvent?.Invoke(articleModel);

        
        public static List<IFilterObject> GetFilterObjectsFromType (this IEnumerable<IFilterObject> filterObjects,  string typeName)=>
             filterObjects.Where(x => x.Type?.Contains(typeName) is true).ToList();   

        public static String? GetActiveValueString (this IEnumerable<IFilterObject> filterObjects){
             
             string? res=null;
             filterObjects.Where(x => x.Active is true).ToList().ForEach((x)=>res+=x.Value+",");
             return res;
             
        }
        public static void SetEventIsEnable (this IEnumerable<IFilterObject> filterObjects,bool enable=true)=>
                filterObjects.ToList().ForEach ((x)=>SetEventIsEnable(x) );     

        public static void SetEventIsEnable (this IFilterObject filterObject,bool enable=true)=>
            filterObject.FilterActiveEventIsEnable=enable;       
        
        public static void ResetActive (this IEnumerable<IFilterObject> filterObjects){

              filterObjects.SetEventIsEnable(false);
              filterObjects.ToList().ForEach ( (x)=>{x.Active=false;});
              filterObjects.SetEventIsEnable(true);
                        
        }

        public static void  AddRange(this ObservableCollection<Models.DataModel> collection, IEnumerable<ShikimoriSharp.Classes.Anime> Datacollection)=>
            Datacollection.ToList().ForEach(x=>collection.Add(new (x)));
        
        public static void AddRange(this ObservableCollection<Models.DataModel> collection, IEnumerable<ShikimoriSharp.Classes.Manga> Datacollection)=>
            Datacollection.ToList().ForEach(x => collection.Add(new(x)));
    
    }
}
