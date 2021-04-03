using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SportsEcommerce.Controllers;
using SportsEcommerce.Models;
using SportsEcommerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsEcommerce.Tests
{
    public class HomeControllerTests
    {
        Mock<IStoreRepository> mock;

        [SetUp]
        public void Setup()
        {
           mock = new Mock<IStoreRepository>();
        }

        [Test]
        public void Can_Use_Repository()
        {
            // Arrange
            mock.Setup(x => x.Products).Returns((new Product[] {
                new Product{ ProductID = 1, Name = "P1"},
                new Product{ ProductID = 2, Name = "P2"}
            }).AsQueryable());

            HomeController controller = new HomeController(mock.Object);

            // Act
           ProductsListViewModel result = (controller.Index(null) as ViewResult).ViewData.Model as ProductsListViewModel;

            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.AreEqual("P1", prodArray[0].Name);
            Assert.AreEqual("P2", prodArray[1].Name);


        }


        [Test]
        public void Can_Paginate()
        {
            // Arrange
            mock.Setup(x => x.Products).Returns((new Product[] {
                new Product{ ProductID = 1, Name = "P1"},
                new Product{ ProductID = 2, Name = "P2"},
                new Product{ ProductID = 3, Name = "P3"},
                new Product{ ProductID = 4, Name = "P4"},
                new Product{ ProductID = 5, Name = "P5"},
            }).AsQueryable());

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            // Act
            ProductsListViewModel result = (controller.Index(null, 2) as ViewResult).ViewData.Model as ProductsListViewModel;

            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.AreEqual("P4", prodArray[0].Name);
            Assert.AreEqual("P5", prodArray[1].Name);

        }

        [Test]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            mock.Setup(x => x.Products).Returns((new Product[] {
                new Product{ ProductID = 1, Name = "P1"},
                new Product{ ProductID = 2, Name = "P2"},
                new Product{ ProductID = 3, Name = "P3"},
                new Product{ ProductID = 4, Name = "P4"},
                new Product{ ProductID = 5, Name = "P5"},
            }).AsQueryable());

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            // Act
            ProductsListViewModel result = (controller.Index(null, 2) as ViewResult).ViewData.Model as ProductsListViewModel;

            // Assert
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(2, pagingInfo.CurrentPage);
            Assert.AreEqual(3, pagingInfo.ItemsPerPage);
            Assert.AreEqual(5, pagingInfo.TotalItems);
            Assert.AreEqual(2, pagingInfo.TotalPages);
        }
    }
}
