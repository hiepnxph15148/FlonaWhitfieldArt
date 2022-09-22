using Archetype.Models;
using FlonaWhitfieldArt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace FlonaWhitfieldArt.Controllers
{
    public class HomeController : SurfaceController
    {
        // GET: Home
       
        private string PartialoViewPath(string name)
        {
            return $"~/Views/Partials/Home/{name}.cshtml";
        }

        private const int MAXIMUN_TESTIMOIALS = 4;
        public ActionResult RenderFeatured()
        {
            //List<FeaturedItem> model = new List<FeaturedItem>();
            //IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");
            //ArchetypeModel featuredItems = homePage.GetPropertyValue<ArchetypeModel>("featuredItems");
            //foreach(ArchetypeFieldsetModel fieldset in featuredItems)
            //{
            //   int imageId = fieldset.GetValue<int>("image");
            //   var mediaItem = Umbraco.Media(imageId);
            //   string imageUrl = mediaItem.Url;
            //  int pageId = fieldset.GetValue<int>("page");
            //    IPublishedContent linkedToPage = Umbraco.TypedContent(pageId);
            //    string linkUrl = linkedToPage.Url;
            //  model.Add( new FeaturedItem(fieldset.GetValue<string>("name"), fieldset.GetValue<string>("category"), imageUrl, linkUrl));
            //}
            return PartialView(PartialoViewPath("_Services"));
        }
        public ActionResult RenderServices()
        {
            return PartialView(PartialoViewPath( "_Clients"));
        }
        public ActionResult RenderBlog()
        {

            IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");
            string title = homePage.GetPropertyValue<string>("latestBlogPostsTitle");
            string introduction = homePage.GetPropertyValue<string>("latestBlogPostsIntroduction").ToString();
            LatestBlogPostModel model = new LatestBlogPostModel(title, introduction);

            return PartialView(PartialoViewPath("_Blog"), model);
        }
        public ActionResult RenderTestimonials()
        {
            IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");
            string title = homePage.GetPropertyValue<string>("testimonialsTitle");
            string introduction = homePage.GetPropertyValue<string>("testimonialsIntroduction").ToString();
            List<TestimonialModel> testimonials = new List<TestimonialModel>();
            ArchetypeModel testimonialList = homePage.GetPropertyValue<ArchetypeModel>("testimonialList");
            {
                if (testimonialList != null)
                {
                    foreach (ArchetypeFieldsetModel testimonial in testimonialList.Take(MAXIMUN_TESTIMOIALS))
                    {
                        string name = testimonial.GetValue<string>("name");
                        string quote = testimonial.GetValue<string>("quote");
                        testimonials.Add(new TestimonialModel(quote, name));
                    }
                }
            }
            TestimonialsModel model = new TestimonialsModel(title, introduction, testimonials);
            return PartialView(PartialoViewPath("_Testimonials"), model);
        }
    }
}