namespace SrpLab;

public sealed class GiftCardFormatter
{
    public string Format(
        CheckoutBasket basket,
        string fromName)
    {
        var items = string.Join(", ", basket.Skus());

        return $"Dear friend,\n" +
               $"A gift from {fromName} awaits ({items}).\n" +
               $"Total surprise value: {basket.GrandTotal():C}\n";
    }
}