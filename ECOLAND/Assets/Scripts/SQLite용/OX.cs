using SQLite4Unity3d;

public class OX
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [NotNull]
    public string Problem { get; set; }
    [NotNull]
    public string Answer { get; set; }

    public override string ToString()
    {
        return string.Format("[OX: Id={0}, Problem={1},  Answer={2}]", Id, Problem, Answer);
    }
}
