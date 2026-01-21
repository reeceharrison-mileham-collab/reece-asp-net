using Microsoft.Data.Sqlite;
using Reece.TaskTracker.Models;

namespace Reece.TaskTracker.Data;

public class TaskRepository
{
    private readonly string _connectionString;

    public TaskRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Default")!;
    }

    public List<TaskItem> GetAll()
    {
        var tasks = new List<TaskItem>();

        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = """
            SELECT id, description, due_date, status
            FROM tasks
            ORDER BY due_date;
        """;

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            tasks.Add(new TaskItem
            {
                Id = reader.GetInt32(0),
                Description = reader.GetString(1),
                DueDate = DateTime.Parse(reader.GetString(2)),
                Status = reader.GetString(3)
            });
        }

        return tasks;
    }

    public void Create(string description, DateTime dueDate)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = """
            INSERT INTO tasks (description, due_date, status)
            VALUES ($desc, $due, 'pending');
        """;

        cmd.Parameters.AddWithValue("$desc", description);
        cmd.Parameters.AddWithValue("$due", dueDate.ToString("yyyy-MM-dd"));

        cmd.ExecuteNonQuery();
    }
}

