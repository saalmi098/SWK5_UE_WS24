using System;

namespace Swack.Logic.Models;

public class ImageMessage : Message
{
    public ImageMessage(Channel channel, User user, DateTime timestamp, string imageUrl, string text = "")
        : base(channel, user, timestamp, text)
    {
        ImageUrl = imageUrl;
    }

    public string ImageUrl { get; set; }
}
