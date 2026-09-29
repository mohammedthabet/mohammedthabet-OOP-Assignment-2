namespace SrpLab;

public sealed class WalkingRoutePlanner
{
    public IReadOnlyList<PickNeed> Plan(
        IEnumerable<PickNeed> needs)
    {
        return needs
            .OrderBy(x => x.Aisle)
            .ThenBy(x => x.Bin)
            .ToList();
    }
}