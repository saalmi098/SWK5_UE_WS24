using System;

namespace Swack.Logic.Models;

public class Message(Channel channel, User user, DateTime timestamp, string text)
{
    public Channel Channel { get; set; } = channel;

    public User User { get; set; } = user;

    public string Text { get; set; } = text;

    public DateTime Timestamp { get; set; } = timestamp;
}
