using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsEcommerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEcommerce.Infrastructure
{
    /// <summary>
    /// When razor finds 'page-model' attribute on the div element, it will ask this class to transform the element
    /// </summary>
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelpers : TagHelper
    {
        /// <summary>
        /// Tag helpers use IUrlHelperFactory to generate URLs that target different parts of the application
        /// </summary>
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelpers(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PagingInfo PageModel { get; set; }

        public string PageAction { get; set; }

        // tag helper style properties
        public bool PageClassesEnabled { get; set; }
        public string PageClass { get; set; }
        public string PageClassesNormal { get; set; }
        public string PageClassSelected { get; set; }

        /// <summary>
        /// the 'HtmlAttributeName' allows for specifying a prefix for attribute names on the element which in this case will be page-url-
        /// For e.g in the layout this can be changed to page-url-category, and now the key will be category and the value will be whats passed to it using razor expression for this dictionary
        /// </summary>
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            TagBuilder result = new TagBuilder("div");
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                // set the key 'productPage' to the page number
                PageUrlValues["productPage"] = i;
                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassesNormal);
                }

                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }

            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
