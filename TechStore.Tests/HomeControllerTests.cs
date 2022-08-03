using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechStore.Controllers;
using TechStore.Models;
using TechStore.Models.ViewModels;
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
            ProductsListViewModel result =
                 controller.Index().ViewData.Model as ProductsListViewModel;

            // Assert 
            Product[] prodArray = result.Products.ToArray();
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
            ProductsListViewModel result =
                  controller.Index(2).ViewData.Model as ProductsListViewModel;

            // Assert 
            Product[] products = result.Products.ToArray();
            Assert.True(products.Length == 2);
            Assert.Equal("Pr4", products[0].Name);
            Assert.Equal("Pr5", products[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange Организация
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"},
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object)
            {
                PageSize = 3
            };

            // Act Поведение
            ProductsListViewModel result = controller.Index(2).ViewData.Model
                as ProductsListViewModel;

            // Assert 
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(5, pagingInfo.TotalItems);
            Assert.Equal(2, pagingInfo.TotalPages);

        }
    }
}
