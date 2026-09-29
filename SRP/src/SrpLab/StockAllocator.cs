namespace SrpLab;

public sealed class StockAllocator
{
    public int Allocated(PickNeed need)
    {
        return Math.Min(
            need.Requested,
            need.Available);
    }

    public bool HasShortage(PickNeed need)
    {
        return need.Available < need.Requested;
    }
}