namespace SrpLab;

public sealed class DunningEmailWriter
{
    public string Write(
        SubscriptionBilling subscription,
        InvoiceNumberGenerator invoiceNumbers,
        string customerName,
        DateOnly asOf)
    {
        var amount =
            subscription.Prorate(subscription.PeriodStart);

        var invoice =
            invoiceNumbers.Next(subscription.PeriodStart);

        var severity = subscription.FailedPayments switch
        {
            <= 1 => "friendly reminder",
            2 => "second notice",
            _ => "final notice before suspension"
        };

        return $"Subject: {severity} {invoice}\n" +
               $"Hi {customerName},\n" +
               $"Balance {amount:C} as of {asOf:o} " +
               $"({subscription.FailedPayments} failures).\n";
    }
}