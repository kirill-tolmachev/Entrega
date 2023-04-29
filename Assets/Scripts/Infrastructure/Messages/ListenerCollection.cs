using System;

namespace Scripts.Infrastructure.Messages
{
    internal class ListenerCollection<TMessage> : ListenerCollectionBase<Action<TMessage>>
    {
    }
}