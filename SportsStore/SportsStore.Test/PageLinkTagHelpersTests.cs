﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SportsStore.Infrastucture;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Test
{
    public class PageLinkTagHelpersTests
    {
        [Fact]
        public void Can_Generate_Page_Links()
        {
            //Arrange
            var urlhepler = new Mock<IUrlHelper>();
            urlhepler.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1")
                .Returns("Test/Page2")
                .Returns("Test/Page3");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(x => x.GetUrlHelper(It.IsAny<ActionContext>())).Returns(urlhepler.Object);

            var viewContext = new Mock<ViewContext>();

            PageLinkTagHelper helper = new PageLinkTagHelper(urlHelperFactory.Object)
            {
                PageModel = new PagingInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10,
                },
                ViewContext = viewContext.Object,
                PageAction = "Test"
            };

            TagHelperContext ctx = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(), "");

            var content = new Mock<TagHelperContent>();
            TagHelperOutput output = new TagHelperOutput("div",
                new TagHelperAttributeList(),
                (cashe, encoder) => Task.FromResult(content.Object));

            //Act
            helper.Process(ctx, output);

            //Assert
            Assert.Equal(@"<a href=""Test/Page1"">1</a>" +
                @"<a href=""Test/Page2"">2</a>" +
                @"<a href=""Test/Page3"">3</a>",
                output.Content.GetContent());
        }
    }
}
