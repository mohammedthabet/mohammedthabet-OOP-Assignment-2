namespace SrpLab;

public sealed class WardPager
{
    private readonly List<string> _pager = new();

    public void Escalate(int bedNumber)
    {
        _pager.Add(
            $"CODE-YELLOW bed={bedNumber} at {DateTimeOffset.Now:HH:mm}");
    }

    public IReadOnlyList<string> Drain()
    {
        var copy = _pager.ToList();
        _pager.Clear();

        return copy;
    }
}