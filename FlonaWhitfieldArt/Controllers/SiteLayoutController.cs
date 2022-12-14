using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using FlonaWhitfieldArt.Models;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Runtime.Caching;
using Umbraco.Core.Models.PublishedContent;

namespace FlonaWhitfieldArt.Controllers
{
    public class SiteLayoutController : SurfaceController
    {
        private string PartialoViewPath(string name)
        {
            return $"~/Views/Partials/SiteLayout/{name}.cshtml";
        }
        // GET: StiteLayout

        public ActionResult RenderFooter()
        {
            return PartialView(PartialoViewPath( "_Footer"));
        }
        public ActionResult RenderIntro()
        {
            return PartialView(PartialoViewPath( "_Intro"));
        }
        public ActionResult RenderTitleControls()
        {
            return PartialView(PartialoViewPath("_TitleControls"));
        }
        /// <summary>
        /// Renders the top navigation in the header partial
        /// </summary>
        /// <returns>Partial view with a model</returns>
        public ActionResult RenderHeader()
        {
            List<NavigationListItem> nav = GetObjectFromCache<List<NavigationListItem>>("mainNav", 0, GetNavigationModelFromDatabase);
            return PartialView(PartialoViewPath ( "_Header"), nav);
        }


        /// <summary>
        /// Finds the home page and gets the navigation structure based on it and it's children
        /// </summary>
        /// <returns>A List of NavigationListItems, representing the structure of the site.</returns>
        private List<NavigationListItem> GetNavigationModelFromDatabase()
        {
            IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");
            List<NavigationListItem> nav = new List<NavigationListItem>();
            nav.Add(new NavigationListItem(new NavigationLink(homePage.Url, homePage.Name)));
            nav.AddRange(GetChildNavigationList(homePage));
            return nav;
        }

        /// <summary>
        /// Loops through the child pages of a given page and their children to get the structure of the site.
        /// </summary>
        /// <param name="page">The parent page which you want the child structure for</param>
        /// <returns>A List of NavigationListItems, representing the structure of the pages below a page.</returns>
        private List<NavigationListItem> GetChildNavigationList(IPublishedContent page)
        {
            List<NavigationListItem> listItems = null;
            var childPages = page.Children.Where("Visible").Where(x => x.Level <= 2).Where(x => !x.HasValue("excludeFromTopNavigation") || (x.HasValue("excludeFromTopNavigation") && !x.GetPropertyValue<bool>("excludeFromTopNavigation")));
            if (childPages != null && childPages.Any() && childPages.Count() > 0)
            {
                listItems = new List<NavigationListItem>();
                foreach (var childPage in childPages)
                {
                    NavigationListItem listItem = new NavigationListItem(new NavigationLink(childPage.Url, childPage.Name));
                    listItem.Items = GetChildNavigationList(childPage);
                    listItems.Add(listItem);
                }
            }
            return listItems;
        }
        /// <summary>
        /// A generic function for getting and setting objects to the memory cache.
        /// </summary>
        /// <typeparam name="T">The type of the object to be returned.</typeparam>
        /// <param name="cacheItemName">The name to be used when storing this object in the cache.</param>
        /// <param name="cacheTimeInMinutes">How long to cache this object for.</param>
        /// <param name="objectSettingFunction">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <returns>An object of the type you asked for</returns>
        private static T GetObjectFromCache<T>(string cacheItemName, int cacheTimeInMinutes, Func<T> objectSettingFunction)
        {
            ObjectCache cache = MemoryCache.Default;
            var cachedObject = (T)cache[cacheItemName];
            if (cachedObject == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheTimeInMinutes);
                cachedObject = objectSettingFunction();
                cache.Set(cacheItemName, cachedObject, policy);
            }
            return cachedObject;
        }
    }
}