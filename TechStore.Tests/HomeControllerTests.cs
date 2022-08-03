using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechStore.Controllers;
using TechStore.Models;
using Xunit;

namespace TechStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Use_Repository()
        {
            // Arrange Организовка
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"}
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);

            // Act Поведение
            IEnumerable<Product> result = (controller.Index() as ViewResult).ViewData.Model
                as IEnumerable<Product>;

            // Assert 
            Product[] prodArray = result.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P2", prodArray[1].Name);

        }

        [Fact]
        public void Can_Paginate()
        {
            // Arrange Организация
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(mock => mock.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "Pr1"},
                new Product {ProductID = 2, Name = "Pr2"},
                new Product {ProductID = 3, Name = "Pr3"},
                new Product {ProductID = 4, Name = "Pr4"},
                new Product {ProductID = 5, Name = "Pr5"},
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            // Act Поведение
            IEnumerable<Product> result = (controller.Index(2) as ViewResult).ViewData.Model
                as IEnumerable<Product>;

            // Assert 
            Product[] products = result.ToArray();
            Assert.True(products.Length == 2);
            Assert.Equal("Pr1", products[0].Name);
            Assert.Equal("Pr2", products[1].Name);
        }
    }
}
