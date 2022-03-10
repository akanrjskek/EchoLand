using SQLite4Unity3d;

public class STATINFO
{
    [PrimaryKey, NotNull, Unique]
    public string FLAG_DATE { get; set; }
    public int DAILYWALK { get; set; }
    public int CAFE_O { get; set; }
    public int CAFE_X { get; set; }

    public override string ToString()
    {
        return string.Format("[STATINFO: FLAG_DATE={0}, DAILYWALK={1}, CAFE_O={2}, CAFE_X={3}]", FLAG_DATE, DAILYWALK, CAFE_O, CAFE_X);
    }
}
