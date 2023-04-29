using System;
using Cysharp.Threading.Tasks;

namespace Scripts.Infrastructure.Messages
{
    internal class AsyncListenerCollection<TMessage> : ListenerCollectionBase<Func<TMessage, UniTask>>
    {
    }
}