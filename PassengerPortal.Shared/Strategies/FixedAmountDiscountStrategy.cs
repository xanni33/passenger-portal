using PassengerPortal.Shared.Interfaces;

namespace PassengerPortal.Shared.Strategies;

public class FixedAmountDiscountStrategy : IDiscountStrategy
{
    private readonly decimal _fixedAmount;

    public FixedAmountDiscountStrategy(decimal fixedAmount)
    {
        _fixedAmount = fixedAmount;
    }

    public decimal ApplyDiscount(decimal basePrice)
    {
        return basePrice - _fixedAmount;
    }
}