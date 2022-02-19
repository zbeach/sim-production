class Employee
{
    private long id;
    private string firstName;
    private string lastName;
    private DateTime hireDate;

    public long Id { get => id; }
    public string FirstName { get => firstName; }
    public string LastName { get => lastName; }
    public DateTime HireDate { get => hireDate; }

    public Employee(long id, string firstName, string lastName, DateTime hireDate)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.hireDate = hireDate;
    }

    public Employee(string firstName, string lastName, DateTime hireDate)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.hireDate = hireDate;
    }

    public override string ToString()
    {
        return
            @$"
            id: {id}
            First name: {firstName}
            Last name: {lastName}
            Hire date: {hireDate}
            "
            .Replace("            ", "");
    }
}
