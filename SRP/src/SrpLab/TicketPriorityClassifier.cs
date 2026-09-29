namespace SrpLab;

public sealed class TicketPriorityClassifier
{
    public string Classify(SupportTicket ticket)
    {
        var blob =
            (ticket.Subject + " " + ticket.Body).ToLowerInvariant();

        if (blob.Contains("down") ||
            blob.Contains("outage") ||
            blob.Contains("cannot login"))
        {
            return "P1";
        }

        if (blob.Contains("urgent") ||
            blob.Contains("asap") ||
            blob.Contains("blocked"))
        {
            return "P2";
        }

        return "P3";
    }
}