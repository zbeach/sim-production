static class Employees
{
    /// <summary>
    /// Gets an employee by id.
    /// </summary>
    /// <param name="id">The id of the employee to get.</param>
    /// <returns>The employee with the given id.</returns>
    public static Employee Get(long id)
    {
        var command = Global.connection.CreateCommand();
        command.CommandText =
        @$"
            SELECT *
            FROM employees
            WHERE id = {id}
            LIMIT 1;
        ";
        
        var reader = command.ExecuteReader();
        reader.Read();
        
        return new Employee(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetString(2),
            new DateTime(reader.GetInt64(3))
        );
    }

    /// <summary>
    /// Gets an array of employees by their ids. If the array of ids is empty, gets all employees.
    /// </summary>
    /// <param name="ids">The ids of the employees to get.</param>
    /// <returns>A list of employees.</returns>
    public static Employee[] Get(long[] ids)
    {
        var command = Global.connection.CreateCommand();
        command.CommandText =
        @$"
            SELECT *
            FROM employees
            {(
                ids.Length > 0
                    ? $"WHERE id IN ({string.Join(",", ids)})"
                    : ""
            )}
            ORDER BY id ASC;
        ";

        var reader = command.ExecuteReader();
        
        List<Employee> employees = new List<Employee>();
        while (reader.Read())
        {
            employees.Add(new Employee(
                reader.GetInt64(0),
                reader.GetString(1),
                reader.GetString(2),
                new DateTime(reader.GetInt64(3))
            ));
        }

        return employees.ToArray();
    }

    /// <summary>
    /// Gets all employees.
    /// </summary>
    /// <returns>All employees.</returns>
    public static Employee[] Get()
    {
        return Get(new long[] { });
    }

    /// <summary>
    /// Adds an employee.
    /// </summary>
    /// <param name="firstName">The first name of the employee.</param>
    /// <param name="lastName">The last name of the employee.</param>
    /// <param name="hireDate">The hire date of the employee.</param>
    public static void Add(Employee employee)
    {
        Add(new Employee[] { employee });
    }

    /// <summary>
    /// Adds an array of employees.
    /// </summary>
    /// <param name="employees">The employees to add.</param>
    public static void Add(Employee[] employees)
    {
        var command = Global.connection.CreateCommand();
        command.CommandText =
        @$"
            INSERT INTO employees (first_name, last_name, hire_date)
            VALUES {string.Join(",", employees.Select(employee => $"('{employee.FirstName}', '{employee.LastName}', {employee.HireDate.Ticks})"))};
        ";

        command.ExecuteNonQuery();
    }

    /// <summary>
    /// Removes the employee with the given id.
    /// </summary>
    /// <param name="id">The id of the employee to remove.</param>
    public static void Remove(long id)
    {
        var command = Global.connection.CreateCommand();
        command.CommandText =
        @$"
            DELETE FROM employees
            WHERE id = {id};
        ";

        command.ExecuteNonQuery();
    }

    /// <summary>
    /// Removes the employees with the given ids.
    /// </summary>
    /// <param name="ids">The ids of the employees to remove.</param>
    public static void Remove(long[] ids)
    {
        var command = Global.connection.CreateCommand();
        command.CommandText =
        @$"
            DELETE FROM employees
            WHERE id IN ({string.Join(",", ids)});
        ";

        command.ExecuteNonQuery();
    }

    /// <summary>
    /// Removes all employees.
    /// </summary>
    public static void Remove()
    {
        var command = Global.connection.CreateCommand();
        command.CommandText =
        @$"
            DELETE FROM employees WHERE TRUE;
        ";

        command.ExecuteNonQuery();
    }
}
