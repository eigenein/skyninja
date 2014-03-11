using System;

namespace SkyNinja.Core.Inputs.Skype
{
    /// <summary>
    /// messages.type
    /// </summary>
    internal enum SkypeMessageType
    {
        SetTopic = 2,
        ConferenceOpensUp = 4,
        VideosessionStarted = 30,
        VideosessionTerminated = 39,
        AuthorizationRequested = 50,
        AuthorizationAccepted = 51,
        Blocked = 53,
        SentEmoticon = 60,
        Said = 61,
        SentContact = 63,
        SentSms = 64,
        SentVoiceMessage = 65,
        SentFile = 68,
        Birthday = 110
    }
}
