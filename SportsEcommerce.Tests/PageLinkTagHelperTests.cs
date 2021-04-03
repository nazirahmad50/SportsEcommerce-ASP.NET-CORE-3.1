using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using NUnit.Framework;
using SportsEcommerce.Controllers;
using SportsEcommerce.Infrastructure;
using SportsEcommerce.Models;
using SportsEcommerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEcommerce.Tests
{
    public class PageLinkTagHelperTests
    {
        [Test]
        public void Can_Generate_Page_Links()
        {
            // arrange
            Mock<IUrlHelper> urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1")
                .Returns("Test/Page2")
                .Returns("Test/Page3");

            Mock<IUrlHelperFactory> urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(x => x.GetUrlHelper(It.IsAny<ActionContext>())).Returns(urlHelper.Object);

            PageLinkTagHelpers helper = new PageLinkTagHelpers(urlHelperFactory.Object)
            {
                PageModel = new PagingInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10
                },
                PageAction = "Test"
            };

            TagHelperContext context = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), "");

            Mock<TagHelperContent> content = new Mock<TagHelperContent>();
            TagHelperOutput output = new TagHelperOutput("div", new TagHelperAttributeList(), (cache, encode) => Task.FromResult(content.Object));

            // Act
            helper.Process(context, output);

            // Assert
            Assert.AreEqual(@"<a href=""Test/Page1"">1</a>"
                            + @"<a href=""Test/Page2"">2</a>"
                            + @"<a href=""Test/Page3"">3</a>",
                            output.Content.GetContent());


        }
    }
}
