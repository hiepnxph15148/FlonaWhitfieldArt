using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Mvc;
using System.Web.Mvc;
using FlonaWhitfieldArt.Models;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace FlonaWhitfieldArt.Controllers
{
    public class BlogController : SurfaceController
    {

        private string PartialoViewPath(string name)
        {
            return $"~/Views/Partials/Blog/{name}.cshtml";
        }
        // GET: Blog
        public ActionResult RenderPostList(int numberOfItems)
        {
            List<BlogPreview> model = new List<BlogPreview>();
             IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");
            IPublishedContent blogPage = homePage.Children.Where(x => x.DocumentTypeAlias == "blog").FirstOrDefault();
            foreach (IPublishedContent page in blogPage.Children.OrderByDescending(x => x.UpdateDate).Take(numberOfItems))
            {
                int imageId = page.GetPropertyValue<int>("articleImage");
                var mediaItem = Umbraco.Media(imageId);

                model.Add(new BlogPreview(page.Name, page.GetPropertyValue<string>("arcticleIntro"), mediaItem.Url, page.Url));
            }
            return PartialView(PartialoViewPath("_PostList"), model);
        }
    }
}   