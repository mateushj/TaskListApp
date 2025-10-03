using System;
using System.Threading;
using Npgsql;

namespace TaskListAPI.Infrastructure
{
    public static class DatabaseWaiter
    {
        public static void WaitForPostgres(string? connectionString, int timeoutSeconds = 30)
        {
            var start = DateTime.Now;
            while ((DateTime.Now - start).TotalSeconds < timeoutSeconds)
            {
                try
                {
                    using var conn = new NpgsqlConnection(connectionString);
                    conn.Open();
                    Console.WriteLine("Postgres is ready!");
                    return;
                }
                catch
                {
                    Console.WriteLine(connectionString);
                    Console.WriteLine("Waiting for Postgres...");
                    Thread.Sleep(1000);
                }
            }
            throw new Exception("Could not connect to Postgres within timeout.");
        }
    }
}
