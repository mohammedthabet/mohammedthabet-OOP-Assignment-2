namespace SrpLab;

public sealed class BusinessHoursPolicy
{
    public TimeOnly Open { get; }
    public TimeOnly Close { get; }
    public int SlotMinutes { get; }

    public BusinessHoursPolicy(
        TimeOnly open,
        TimeOnly close,
        int slotMinutes)
    {
        Open = open;
        Close = close;
        SlotMinutes = slotMinutes;
    }

    public bool IsWithinBusinessHours(DateTimeOffset when)
    {
        if (when.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday)
            return false;

        var time = TimeOnly.FromDateTime(when.DateTime);

        return time >= Open
               && time.AddMinutes(SlotMinutes) <= Close;
    }
}