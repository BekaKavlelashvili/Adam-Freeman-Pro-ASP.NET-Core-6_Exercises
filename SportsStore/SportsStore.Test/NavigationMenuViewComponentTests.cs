﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Test
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            //Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(x => x.Products).Returns((new Product[]
            {
                new Product {ProductId = 1, Name = "P1",
                 Category = "Apples"},
                 new Product {ProductId = 2, Name = "P2",
                 Category = "Apples"},
                 new Product {ProductId = 3, Name = "P3",
                 Category = "Plums"},
                 new Product {ProductId = 4, Name = "P4",
                 Category = "Oranges"},
            }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            //Atc = get the set of categories

            string[] results = ((IEnumerable<string>?)(target.Invoke() as ViewComponentResult)?.ViewData.Model ?? Enumerable.Empty<string>()).ToArray();

            //Assert
            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            // Arrange
            string categoryToSelect = "Apples";
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

            mock.Setup(m => m.Products).Returns((new Product[] {
                 new Product {ProductId = 1, Name = "P1", Category = "Apples"},
                 new Product {ProductId = 4, Name = "P2", Category = "Oranges"},
             }).AsQueryable<Product>());

            NavigationMenuViewComponent target =
            new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                }
            };
            target.RouteData.Values["category"] = categoryToSelect;

            // Action
            string? result = (string?)(target.Invoke()
            as ViewViewComponentResult)?.ViewData?["SelectedCategory"];

            // Assert
            Assert.Equal(categoryToSelect, result);
        }
    }
}
