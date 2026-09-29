namespace SrpLab;

public sealed record PickNeed(
    string Sku,
    string Aisle,
    int Bin,
    int Requested,
    int Available);