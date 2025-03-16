using Microsoft.AspNetCore.Mvc;
using OrderProcessingApp.Controllers;
using OrderProcessingApp.Models;
using Xunit;

namespace OrderProcessingApp.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void ProcessOrder_ShouldApply10PercentDiscountForLoyalCustomersWith100OrMore()
        {
            // Arrange
            var controller = new OrderController();

            // Act
            var result = controller.ProcessOrder(100, "Loyal") as ViewResult;
            var order = result?.Model as Order;

            // Assert
            Assert.NotNull(order);
            Assert.Equal(10, order.Discount);
            Assert.Equal(90, order.FinalAmount);
        }

        [Fact]
        public void ProcessOrder_ShouldNotApplyDiscountForNewCustomers()
        {
            // Arrange
            var controller = new OrderController();

            // Act
            var result = controller.ProcessOrder(100, "New") as ViewResult;
            var order = result?.Model as Order;

            // Assert
            Assert.NotNull(order);
            Assert.Equal(0, order.Discount);
            Assert.Equal(100, order.FinalAmount);
        }

        [Fact]
        public void ProcessOrder_ShouldNotApplyDiscountForLoyalCustomersWithLessThan100()
        {
            // Arrange
            var controller = new OrderController();

            // Act
            var result = controller.ProcessOrder(99, "Loyal") as ViewResult;
            var order = result?.Model as Order;

            // Assert
            Assert.NotNull(order);
            Assert.Equal(0, order.Discount);
            Assert.Equal(99, order.FinalAmount);
        }

        [Fact]
        public void ProcessOrder_ShouldReturnIndexView_WhenInputIsInvalid()
        {
            // Arrange
            var controller = new OrderController();

            // Act
            var result = controller.ProcessOrder(-10, "") as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ViewName);
        }
    }
}
