namespace SrpLab;

public sealed class SlaPolicy
{
    public DateTimeOffset Deadline(
        SupportTicket ticket,
        string priority)
    {
        var hours = priority switch
        {
            "P1" => 4,
            "P2" => 24,
            _ => 72
        };

        return ticket.OpenedAt.AddHours(hours);
    }

    public bool IsBreached(
        SupportTicket ticket,
        string priority,
        DateTimeOffset now)
    {
        return now > Deadline(ticket, priority);
    }
}