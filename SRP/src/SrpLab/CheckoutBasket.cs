namespace SrpLab;

public sealed class CheckoutBasket
{
    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();

    private readonly CouponPolicy _couponPolicy = new();
    private readonly GiftWrapPricingPolicy _giftWrapPricingPolicy = new();

    private string? _couponRaw;
    private bool _giftWrap;

    public void AddLine(
        string sku,
        decimal price,
        int qty)
    {
        if (qty <= 0)
            throw new ArgumentOutOfRangeException(nameof(qty));

        _lines.Add((sku, price, qty));
    }

    public IReadOnlyList<string> Skus()
    {
        return _lines
            .Select(line => line.Sku)
            .ToList();
    }

    public void ApplyCouponText(string? couponText)
    {
        _couponRaw = couponText;
    }

    public void EnableGiftWrap()
    {
        _giftWrap = true;
    }

    public decimal SubTotal()
    {
        return _lines.Sum(line => line.Price * line.Qty);
    }

    public decimal DiscountAmount()
    {
        return _couponPolicy.CalculateDiscount(
            _couponRaw,
            SubTotal());
    }

    public decimal GrandTotal()
    {
        var total =
            SubTotal()
            - DiscountAmount()
            + _giftWrapPricingPolicy.CalculateFee(_giftWrap);

        return Math.Max(0m, total);
    }
}