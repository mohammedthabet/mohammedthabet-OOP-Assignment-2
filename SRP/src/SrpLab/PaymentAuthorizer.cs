namespace SrpLab;

public sealed class PaymentAuthorizer
{
    public string Authorize(
        CheckoutBasket basket,
        string cardLast4)
    {
        var payload =
            $"{basket.GrandTotal():0.00}|" +
            $"{cardLast4}|" +
            $"{basket.Skus().Count}";

        var hash = payload.GetHashCode();

        return $"AUTH-{Math.Abs(hash):X8}";
    }
}