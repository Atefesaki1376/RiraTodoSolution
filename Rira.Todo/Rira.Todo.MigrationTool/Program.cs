internal class Program
{
    static async Task Main(string[] args)
    {
        // Create a host for dependency injection
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Register your DbContext
                services.AddDbContext<GuidAppDbContext>(options =>
                    options.UseSqlServer("Data Source=DESKTOP-CJ50OTT\\MSSQLSERVER02;Database=Rira; Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")); // Replace with your connection string
            })
            .Build();

        // Run the migration tool
        using var scope = host.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GuidAppDbContext>();

        try
        {
            // Apply pending migrations
            Console.WriteLine("Applying migrations...");
            await dbContext.Database.MigrateAsync();

            Console.WriteLine("Database migrations applied successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
        }
    }
}