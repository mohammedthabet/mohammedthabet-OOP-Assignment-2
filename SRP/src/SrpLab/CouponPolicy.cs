namespace SrpLab;

public sealed class CouponPolicy
{
    public decimal CalculateDiscount(
        string? couponText,
        decimal subtotal)
    {
        if (string.IsNullOrWhiteSpace(couponText))
            return 0m;

        var text = couponText.Trim().ToUpperInvariant();

        if (text.StartsWith("SAVE") &&
            int.TryParse(text[4..], out var percent) &&
            percent is > 0 and <= 50)
        {
            return Math.Round(
                subtotal * percent / 100m,
                2);
        }

        if (text.Contains("FREESHIP"))
            return 0m;

        if (text == "WELCOME10")
            return Math.Min(10m, subtotal);

        return 0m;
    }
}