using Swack.Logic;
using Swack.Logic.Models;
using System.Collections.ObjectModel;

namespace Swack.UI.ViewModels;

public class MainViewModel //: INotifyPropertyChanged
{
    private ChannelViewModel? currentChannel;
    private readonly IMessagingLogic messagingLogic;

    public ObservableCollection<ChannelViewModel> Channels { get; private set; } = [];

    public ChannelViewModel? CurrentChannel
    {
        get => currentChannel;
        set
        {
            currentChannel = value;

            if (currentChannel is not null)
            {
                currentChannel.UnreadMessages = 0;
                //OnPropertyChanged(nameof(CurrentChannel));
            }
            //OnPropertyChanged(nameof(CurrentChannel));
        }
    }

    public MainViewModel(IMessagingLogic messagingLogic)
    {
        this.messagingLogic = messagingLogic ?? throw new ArgumentNullException(nameof(messagingLogic));
        this.messagingLogic.MessageReceived += OnMessageReceived;
    }

    private void OnMessageReceived(Message message)
    {
        var channel = Channels.FirstOrDefault(ch => ch.Channel.Name == message.Channel.Name);
        channel?.Messages.Add(message);

        if (channel is not null && channel != CurrentChannel)
        {
            channel.UnreadMessages++;
        }
    }

    public async Task InitializeAsync()
    {
        foreach (var channel in await messagingLogic.GetChannelsAsync())
        {
            Channels.Add(new ChannelViewModel(channel));
        }
        //OnPropertyChanged(nameof(Channels));
    }

    //public event PropertyChangedEventHandler? PropertyChanged;

    //private void OnPropertyChanged(string propertyName)
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //}
}
