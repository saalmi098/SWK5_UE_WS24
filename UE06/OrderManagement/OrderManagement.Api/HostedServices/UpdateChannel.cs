using System.Threading.Channels;

namespace OrderManagement.Api.HostedServices;

public class UpdateChannel
{
    private const int MAX_QUEUE_LENGTH = 5;

    private readonly Channel<Guid> channel;
    private readonly ILogger<UpdateChannel> logger;

    public UpdateChannel(ILogger<UpdateChannel> logger)
    {
        var options = new BoundedChannelOptions(MAX_QUEUE_LENGTH)
        {
            SingleWriter = false,
            SingleReader = true
        };
        channel = Channel.CreateBounded<Guid>(options);
        this.logger = logger;
    }

    public async Task<bool> AddUpdateTaskAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        await channel.Writer.WriteAsync(customerId, cancellationToken);
        return !cancellationToken.IsCancellationRequested;
    }

    public IAsyncEnumerable<Guid> ReadAllAsync(CancellationToken cancellationToken = default)
    {
        return channel.Reader.ReadAllAsync(cancellationToken);
    }

    public bool TryCompleteWriter(Exception ex) => channel.Writer.TryComplete(ex);
}
