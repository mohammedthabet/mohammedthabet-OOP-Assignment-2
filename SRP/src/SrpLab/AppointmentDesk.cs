namespace SrpLab;

public sealed class AppointmentDesk
{
    private readonly HashSet<DateTimeOffset> _booked = new();
    private readonly BusinessHoursPolicy _businessHours;

    public TimeOnly Open => _businessHours.Open;
    public TimeOnly Close => _businessHours.Close;
    public int SlotMinutes => _businessHours.SlotMinutes;

    public AppointmentDesk(TimeOnly open, TimeOnly close, int slotMinutes)
    {
        _businessHours = new BusinessHoursPolicy(open, close, slotMinutes);
    }

    public bool IsWithinBusinessHours(DateTimeOffset when)
    {
        return _businessHours.IsWithinBusinessHours(when);
    }

    public DateTimeOffset? FindNextSlot(DateTimeOffset from, int searchHours)
    {
        var cursor = Align(from);
        var end = from.AddHours(searchHours);

        while (cursor < end)
        {
            if (_businessHours.IsWithinBusinessHours(cursor)
                && !_booked.Contains(cursor))
            {
                return cursor;
            }

            cursor = cursor.AddMinutes(SlotMinutes);
        }

        return null;
    }

    public bool TryBook(DateTimeOffset slot)
    {
        if (!_businessHours.IsWithinBusinessHours(slot)
            || _booked.Contains(slot))
        {
            return false;
        }

        _booked.Add(slot);
        return true;
    }

    private DateTimeOffset Align(DateTimeOffset from)
    {
        var minutes = from.Minute - (from.Minute % SlotMinutes);

        return new DateTimeOffset(
            from.Year,
            from.Month,
            from.Day,
            from.Hour,
            minutes,
            0,
            from.Offset);
    }
}