namespace PassengerPortal.Shared.Interfaces
{
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal basePrice);
    }
}