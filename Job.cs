class Job
{
    private long id;
    private string title;
    private int baseSalary;

    public long Id { get => id; }
    public string Title
    {
        get => title;
        set => title = value;
    }
    public int BaseSalary
    {
        get => baseSalary;
        set => baseSalary = value;
    }

    public Job(long id, string title, int baseSalary)
    {
        this.id = id;
        this.title = title;
        this.baseSalary = baseSalary;
    }

    public Job(string title, int baseSalary)
    {
        this.title = title;
        this.baseSalary = baseSalary;
    }

    public override string ToString()
    {
        return
            @$"
            id: {id}
            Title: {title}
            Base salary: ${baseSalary}.00
            "
            .Replace("            ", "");
    }
}
