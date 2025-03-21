using PassengerPortal.Shared.Interfaces;

namespace PassengerPortal.Shared.Strategies;

public class NoDiscountStrategy : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal basePrice) => basePrice;
}