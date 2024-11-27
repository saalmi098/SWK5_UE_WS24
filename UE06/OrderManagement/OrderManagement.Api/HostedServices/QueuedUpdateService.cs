
using OrderManagement.Logic;

namespace OrderManagement.Api.HostedServices;

public class QueuedUpdateService : BackgroundService
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<QueuedUpdateService> logger;
    private readonly UpdateChannel updateChannel;

    public QueuedUpdateService(IServiceProvider serviceProvider, ILogger<QueuedUpdateService> logger, UpdateChannel updateChannel)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
        this.updateChannel = updateChannel;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // await foreach wegen IAsyncEnumerable
        await foreach (var customerId in updateChannel.ReadAllAsync(stoppingToken))
        {
            // QueuedUpdateService ist Singleton Instanz, IOrderManagementLogic hat aber einen Scoped-Bereich -> daher muss ein neuer Scope erstellt werden
            using var scope = serviceProvider.CreateScope();
            var logic = scope.ServiceProvider.GetRequiredService<IOrderManagementLogic>();
            decimal revenue = await logic.UpdateTotalRevenueAsync(customerId);

            logger.LogInformation($"Updated total revenue for customer {customerId} at {DateTimeOffset.Now}: revenue = {revenue}");
        }
    }
}
