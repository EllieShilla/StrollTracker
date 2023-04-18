namespace StrollTracker.Viber
{
    /// <summary>
    /// Type of Viber messages.
    /// </summary>
    public enum EventType
    {
        conversation_started,
        subscribed,
        unsubscribed,
        delivered,
        failed,
        message,
        seen
    }
}
