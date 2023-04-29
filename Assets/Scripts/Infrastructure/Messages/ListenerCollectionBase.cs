using System.Collections;
using System.Collections.Generic;

namespace Scripts.Infrastructure.Messages
{
    internal class ListenerCollectionBase<TAction> : IListenerCollection, IEnumerable<TAction>
    {
        protected readonly HashSet<TAction> Listeners = new();

        public void AddListener(TAction message) => Listeners.Add(message);

        public void RemoveListener(TAction message) => Listeners.Remove(message);

        public IEnumerator<TAction> GetEnumerator() => Listeners.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Listeners).GetEnumerator();
    }
}