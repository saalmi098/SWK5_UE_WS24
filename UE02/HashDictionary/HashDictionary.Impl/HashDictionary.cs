using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace HashDictionary.Impl
{
    public class HashDictionary<K, V> : IDictionary<K, V>
    {
        #region inner class node

        private class Node
        {
            // init only setter; required bedeutet, dass der Wert beim Erstellen des Objekts gesetzt werden muss
            // damit ist kein expliziter Konstruktor mehr notwendig
            public required K Key { get; init; }
            public required V Value { get; set; }
            public Node Next { get; set; }
        }

        #endregion

        #region constants and private members

        private const int INITIAL_HASH_TABLE_SIZE = 8;
        private Node[] ht = new Node[INITIAL_HASH_TABLE_SIZE];

        #endregion

        #region helper methods

        private int IndexFor(K key) => Math.Abs(comparer.GetHashCode(key)) % ht.Length;

        private Node FindNode(K key)
        {
            Node n = ht[IndexFor(key)];

            // == oder != sind Operatoren in C#. Theoretisch (als bösartiger Entwickler) kann man diese Operatoren überladen
            // und damit das Verhalten ändern. "is not null" ist kein Operator, und daher sicherer.
            for (; n is not null; n = n.Next)
            {
                if (comparer.Equals(n.Key, key))
                    return n;
            }

            return null;
        }

        // EqualityComparer<K> is a class that provides methods for comparing objects of type K for equality.
        private static readonly EqualityComparer<K> comparer = EqualityComparer<K>.Default;

        private bool TryAdd(K key, V value, out Node node)
        {
            node = FindNode(key);

            if (node is not null)
                return false; // key already exists

            int idx = IndexFor(key);
            node = ht[idx] = new Node { Key = key, Value = value, Next = ht[idx] };
            Count++;

            return true;
        }

        #endregion

        // hashDictionary["Hagenberg"] = 2000;
        public V this[K key]
        {
            //get => (FindNode(key) ?? throw new KeyNotFoundException()).Value;
            get
            {
                Node node = FindNode(key);
                if (node is null) throw new KeyNotFoundException();
                return node.Value;
            }
            set
            {
                if (!TryAdd(key, value, out Node node))
                {
                    node.Value = value;
                }
            }
        }

        public ICollection<K> Keys
        {
            get
            {
                List<K> keys = new List<K>();

                foreach (var pair in this)
                {
                    keys.Add(pair.Key);
                }

                //for (int i = 0; i < ht.Length; i++)
                //{
                //    for (Node n = ht[i]; n is not null; n = n.Next)
                //    {
                //        keys.Add(n.Key);
                //    }
                //}

                return keys;
            }
        }

        public ICollection<V> Values
        {
            get
            {
                List<V> values = new List<V>();

                foreach (var pair in this)
                {
                    values.Add(pair.Value);
                }

                //for (int i = 0; i < ht.Length; i++)
                //{
                //    for (Node n = ht[i]; n is not null; n = n.Next)
                //    {
                //        values.Add(n.Value);
                //    }
                //}

                return values;
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(K key, V value)
        {
            if (!TryAdd(key, value, out _))
                throw new ArgumentException("Key already exists");
        }

        public void Add(KeyValuePair<K, V> item) => Add(item.Key, item.Value);

        public void Clear()
        {
            ht = new Node[INITIAL_HASH_TABLE_SIZE];
            Count = 0;
        }

        public bool Contains(KeyValuePair<K, V> item) => ContainsKey(item.Key);

        public bool ContainsKey(K key) => FindNode(key) is not null;

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            for (int i = 0; i < ht.Length; i++)
            {
                for (Node n = ht[i]; n is not null; n = n.Next)
                {
                    // yield return is a statement that returns an iterator from a method.
                    yield return new KeyValuePair<K, V>(n.Key, n.Value);
                }
            }
        }

        public bool Remove(K key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(K key, [MaybeNullWhen(false)] out V value)
        {
            Node n = FindNode(key);
            value = n is not null ? n.Value : default;
            return n is not null;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
