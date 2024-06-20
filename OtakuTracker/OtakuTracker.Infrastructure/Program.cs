namespace OtakuTracker.Infrastructure;

using Npgsql;

class Program
{
    static void Main()
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=otakutracker";
        
        using var conn = new NpgsqlConnection(connectionString);
        try
        {
            conn.Open();
            Console.WriteLine("Connection successful");
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"Connection failed: {ex.Message}");
        }
    }
}
