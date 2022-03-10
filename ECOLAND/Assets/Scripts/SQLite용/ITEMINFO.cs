using SQLite4Unity3d;

public class ITEMINFO
{
    [PrimaryKey, NotNull, Unique]
    public string ITEM { get; set; }
    public string LOC_X { get; set; }
    public string LOC_Y { get; set; }

    public override string ToString()
    {
        return string.Format("[ITEMINFO: ITEM={0}, LOC_X={1}, LOC_Y={2}]", ITEM, LOC_X, LOC_Y);
    }
}
