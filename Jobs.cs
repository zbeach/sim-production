static class Jobs {
  /// <summary>
  /// Gets the job with the specified id.
  /// </summary>
  /// <param name="id">The id of the job to get.</param>
  /// <returns>The job with the specified id.</returns>
  public static Job Get(long id) {
    var command = Global.connection.CreateCommand();
    command.CommandText =
    @$"
        SELECT *
        FROM jobs
        WHERE id = {id};
    ";

    var reader = command.ExecuteReader();
    if (!reader.Read()) {
      throw new Exception($"No job with id {id}.");
    }

    return new Job(
      id: reader.GetInt64(0),
      title: reader.GetString(1),
      baseSalary: reader.GetInt32(2)
    );
  }

  /// <summary>
  /// Gets an array of jobs by their ids. If the array of ids is empty, gets all jobs.
  /// </summary>
  /// <param name="ids">The ids of the jobs to get.</param>
  /// <returns>A list of jobs.</returns>
  public static Job[] Get(long[] ids) {
    var command = Global.connection.CreateCommand();
    command.CommandText =
    @$"
        SELECT *
        FROM jobs
        {(
          ids.Length > 0
            ? $"WHERE id IN ({string.Join(",", ids)})"
            : ""
        )}
        ORDER BY id ASC;
    ";

    var reader = command.ExecuteReader();

    List<Job> jobs = new List<Job>();
    while (reader.Read()) {
      jobs.Add(new Job(
        id: reader.GetInt64(0),
        title: reader.GetString(1),
        baseSalary: reader.GetInt32(2)
      ));
    }

    return jobs.ToArray();
  }

  /// <summary>
  /// Gets all jobs.
  /// </summary>
  /// <returns>A list of jobs.</returns>
  public static Job[] Get() {
    return Get(new long[] { });
  }

  /// <summary>
  /// Adds a list of jobs.
  /// </summary>
  /// <param name="jobs">The jobs to add.</param>
  public static void Add(params Job[] jobs) {
    var command = Global.connection.CreateCommand();
    command.CommandText =
    @$"
        INSERT INTO jobs (title, base_salary)
        VALUES ({string.Join(",", jobs.Select(job => $"({job.Title}, {job.BaseSalary})"))});
    ";

    command.ExecuteNonQuery();
  }

  /// <summary>
  /// Adds a job.
  /// </summary>
  /// <param name="job">The job to add.</param>
  public static void Add(Job job) {
    Add(new Job[] { job });
  }

  /// <summary>
  /// Removes a job.
  /// </summary>
  /// <param name="id">The id of the job to remove.</param>
  public static void Remove(long id) {
    var command = Global.connection.CreateCommand();
    command.CommandText =
    @$"
        DELETE FROM jobs
        WHERE id = {id};
    ";

    command.ExecuteNonQuery();
  }

  /// <summary>
  /// Removes a list of jobs.
  /// </summary>
  /// <param name="ids">The ids of the jobs to remove.</param>
  public static void Remove(long[] ids) {
    var command = Global.connection.CreateCommand();
    command.CommandText =
    @$"
        DELETE FROM jobs
        WHERE id IN ({string.Join(",", ids)});
    ";

    command.ExecuteNonQuery();
  }

  /// <summary>
  /// Removes all jobs.
  /// </summary>
  public static void Remove() {
    var command = Global.connection.CreateCommand();
    command.CommandText =
        "DELETE FROM jobs WHERE TRUE;";

    command.ExecuteNonQuery();
  }
}
