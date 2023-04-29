using System;
using Cysharp.Threading.Tasks;

namespace Scripts.Infrastructure.Messages
{
    public interface IMessageBus
    {
        UniTask Publish<TMessage>(TMessage message) where TMessage : IMessage;

        UniTask Request<TRequest>(TRequest request) where TRequest : IRequest;
        UniTask<TResult> Request<TRequest, TResult>(TRequest request) where TRequest : IRequest<TResult>;

        void Subscribe<TMessage>(Action<TMessage> callback) where TMessage : IMessage;
        void SubscribeAsync<TMessage>(Func<TMessage, UniTask> callback) where TMessage : IMessage;

        void Unsubscribe<TMessage>(Action<TMessage> callback) where TMessage : IMessage;
    }
}