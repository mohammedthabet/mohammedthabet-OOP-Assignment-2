namespace SrpLab;

public sealed class TicketMessageFormatter
{
    public string PublicReply(
        SupportTicket ticket,
        string priority,
        DateTimeOffset slaDeadline,
        string agentName)
    {
        var message =
            priority == "P1"
                ? "We are treating this as a critical incident."
                : "Thanks for reaching out.";

        return $"Hi,\n{message}\n" +
               $"Ticket {ticket.Id} is with {agentName}. " +
               $"Next update before {slaDeadline:u}.\n";
    }

    public string InternalEscalation(
        SupportTicket ticket,
        string priority,
        DateTimeOffset slaDeadline)
    {
        return $"ESCALATE {ticket.Id} priority={priority} " +
               $"breachAt={slaDeadline:u} keywords-scanned=yes";
    }
}