using Swack.Logic.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Swack.UI.ViewModels;

public class ChannelViewModel(Channel channel) : INotifyPropertyChanged
{
    private int unreadMessages;

    public Channel Channel { get; set; } = channel;
    public ObservableCollection<Message> Messages { get; } = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    public int UnreadMessages
    {
        get => unreadMessages;
        set
        {
            unreadMessages = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UnreadMessages)));
        }
    }
}
