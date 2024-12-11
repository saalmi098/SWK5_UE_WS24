using Swack.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swack.Logic
{
    public delegate void MessageReceivedHandler(Message message);

    public delegate void ChannelCreatedHandler(Channel channel);

    public interface IMessagingLogic
    {
        Task SendMessageAsync(Channel channel, string text);

        Task CreateChannelAsync(Channel channel);

        Task<IEnumerable<Channel>> GetChannelsAsync();

        event MessageReceivedHandler MessageReceived;

        event ChannelCreatedHandler ChannelCreated;
    }
}
