using PassengerPortal.Shared.Interfaces;

namespace PassengerPortal.Shared.Strategies;

public class PercentageDiscountStrategy : IDiscountStrategy
{
    private readonly decimal _percentage;

    public PercentageDiscountStrategy(decimal percentage)
    {
        _percentage = percentage;
    }

    public decimal ApplyDiscount(decimal basePrice)
    {
        return basePrice - (basePrice * _percentage / 100);
    }
}