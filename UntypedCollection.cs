namespace Untyped
{
    public class UntypedCollection
    {
        private readonly Dictionary<Type, object> _collections = new();
        private readonly Dictionary<Type, object> _idSelectors = new();

        public void Add<T>(T item)
        {
            Type type = typeof(T);

            if (!_collections.ContainsKey(type))
            {
                _collections.Add(type, new List<T>());
            }

            (_collections[type] as List<T>)!.Add(item);
        }

        public IEnumerable<T> Get<T>()
        {
            Type type = typeof(T);
            return _collections.ContainsKey(type) ? (List<T>)_collections[type]
                : throw new InvalidOperationException($"No collection for {type.Name}");
        }

        public T Get<T, K>(K id)
        {
            Type type = typeof(T);

            if (!_collections.ContainsKey(type))
            {
                throw new InvalidOperationException($"No collection for {type.Name}");
            }
            if (!_idSelectors.TryGetValue(type, out var selector))
            {
                throw new InvalidOperationException($"No selector for {type.Name}");
            }

            Func<T, K> typedSelector;
            try
            {
                typedSelector = (Func<T, K>)selector;
            }
            catch(InvalidCastException)
            {
                throw new InvalidOperationException($"No selector for {type.Name} with id type {typeof(K).Name}");
            }

            return (_collections[type] as List<T>)!.First(storedItem => typedSelector(storedItem)!.Equals(id));
        }
        public bool Remove<T>(T item)
        {
            return (_collections[typeof(T)] as List<T>)!.Remove(item);
        }

        public bool Remove<T, K>(K id)
        {
            Type type = typeof(T);

            if (!_collections.ContainsKey(type))
            {
                throw new InvalidOperationException($"No collection for {type.Name}");
            }
            if (!_idSelectors.TryGetValue(type, out var selector))
            {
                throw new InvalidOperationException($"No selector for {type.Name}");
            }

            var item = (_collections[type] as List<T>)!.First(storedItem => (selector as Func<T, K>)!(storedItem)!.Equals(id));

            return (_collections[type] as List<T>)!.Remove(item);
        }

        public UntypedCollection SetId<T, K>(Func<T, K> selector)
        {
            _idSelectors[typeof(T)] = selector;

            return this;
        }
    }
}