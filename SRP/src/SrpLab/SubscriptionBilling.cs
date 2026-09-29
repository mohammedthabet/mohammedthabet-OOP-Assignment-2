namespace SrpLab;

/// <summary>
/// Subscription billing: proration math, invoice number minting, and dunning email bodies.
/// </summary>
public sealed class SubscriptionBilling
{

    public string CustomerId { get; }
    public decimal MonthlyPrice { get; }
    public DateOnly PeriodStart { get; }
    public DateOnly PeriodEnd { get; }
    public int FailedPayments { get; private set; }

    public SubscriptionBilling(string customerId, decimal monthlyPrice, DateOnly periodStart, DateOnly periodEnd)
    {
        CustomerId = customerId;
        MonthlyPrice = monthlyPrice;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
    }

    public decimal Prorate(DateOnly activeFrom)
    {
        // Finance calendar rules change independently of email copy.
        if (activeFrom <= PeriodStart) return MonthlyPrice;
        if (activeFrom >= PeriodEnd) return 0m;
        var totalDays = PeriodEnd.DayNumber - PeriodStart.DayNumber;
        if (totalDays <= 0) return MonthlyPrice;
        var used = PeriodEnd.DayNumber - activeFrom.DayNumber;
        return Math.Round(MonthlyPrice * used / totalDays, 2);
    }
 public void RegisterFailedPayment() => FailedPayments++;
 
}
