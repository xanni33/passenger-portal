using NUnit.Framework;
using PassengerPortal.Shared.Strategies;

namespace TestProject1.SharedTests
{
    public class StrategiesTests
    {
        [Test]
        public void ApplyDiscountFixedAmount_ShouldReturnCorrectDiscountedPrice()
        {
            // Arrange
            var strategy = new FixedAmountDiscountStrategy(10);
            var basePrice = 100m;

            // Act
            var result = strategy.ApplyDiscount(basePrice);

            // Assert
            Assert.AreEqual(90m, result);
        }

        [Test]
        public void ApplyDiscountFixedAmount_ShouldNotReturnNegativePrice()
        {
            // Arrange
            var strategy = new FixedAmountDiscountStrategy(150);
            var basePrice = 100m;

            // Act
            var result = strategy.ApplyDiscount(basePrice);

            // Assert
            Assert.AreEqual(-50m, result);
        }
        
        
        [Test]
        public void ApplyNoDiscount_ShouldReturnBasePrice()
        {
            // Arrange
            var strategy = new NoDiscountStrategy();
            var basePrice = 100m;

            // Act
            var result = strategy.ApplyDiscount(basePrice);

            // Assert
            Assert.AreEqual(basePrice, result);
        }
        
        [Test]
        public void ApplyDiscountPercentage_ShouldReturnCorrectDiscountedPrice()
        {
            // Arrange
            var strategy = new PercentageDiscountStrategy(10);
            var basePrice = 100m;

            // Act
            var result = strategy.ApplyDiscount(basePrice);

            // Assert
            Assert.AreEqual(90m, result);
        }

        [Test]
        public void ApplyDiscountPercentage_ShouldReturnZeroFor100PercentDiscount()
        {
            // Arrange
            var strategy = new PercentageDiscountStrategy(100);
            var basePrice = 100m;

            // Act
            var result = strategy.ApplyDiscount(basePrice);

            // Assert
            Assert.AreEqual(0m, result);
        }

        [Test]
        public void ApplyDiscountPercentage_ShouldReturnBasePriceForZeroPercentDiscount()
        {
            // Arrange
            var strategy = new PercentageDiscountStrategy(0);
            var basePrice = 100m;

            // Act
            var result = strategy.ApplyDiscount(basePrice);

            // Assert
            Assert.AreEqual(basePrice, result);
        }
    }
}