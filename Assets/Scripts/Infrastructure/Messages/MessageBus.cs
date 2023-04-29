using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Scripts.Infrastructure.Messages
{

    internal class MessageBus : IMessageBus
    {
        private readonly Dictionary<Type, IListenerCollection> m_listeners = new();

        private readonly Dictionary<Type, IListenerCollection> m_asyncListeners = new();
        private readonly Dictionary<Type, IRequest> m_requestListeners = new();

        private TResult GetListeners<TMessage, TResult>(Dictionary<Type, IListenerCollection> source) where TMessage : IMessage where TResult : IListenerCollection, new()
        {
            if (!source.TryGetValue(typeof(TMessage), out var result))
                source[typeof(TMessage)] = result = new TResult();
            return (TResult)result;
        }

        private ListenerCollection<T> GetListeners<T>() where T : IMessage => GetListeners<T, ListenerCollection<T>>(m_listeners);

        private AsyncListenerCollection<T> GetAsyncListeners<T>() where T : IMessage => GetListeners<T, AsyncListenerCollection<T>>(m_asyncListeners);

        public async UniTask Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            foreach (var listener in GetListeners<TMessage>())
                listener?.Invoke(message);

            foreach (var listener in GetAsyncListeners<TMessage>())
            {
                if (listener != null)
                    await listener.Invoke(message);
            }
        }

        public UniTask Request<TRequest>(TRequest request) where TRequest : IRequest
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            throw new NotImplementedException();
        }

        public UniTask<TResult> Request<TRequest, TResult>(TRequest request) where TRequest : IRequest<TResult>
        {
            throw new NotImplementedException();
        }

        public void Subscribe<TMessage>(Action<TMessage> callback) where TMessage : IMessage
        {
            var listeners = GetListeners<TMessage>();
            listeners.AddListener(callback);
        }

        public void SubscribeAsync<TMessage>(Func<TMessage, UniTask> callback) where TMessage : IMessage
        {
            var listeners = GetAsyncListeners<TMessage>();
            listeners.AddListener(callback);
        }

        public void Unsubscribe<TMessage>(Action<TMessage> callback) where TMessage : IMessage
        {
            var listeners = GetListeners<TMessage>();
            listeners.RemoveListener(callback);
        }
    }
}
