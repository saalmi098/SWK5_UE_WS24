namespace PersonManagement
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> target, Action<T> action)
        {
            foreach (var item in target)
            {
                action(item);
            }
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> target, Predicate<T> predicate)
        {
            foreach (var item in target)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
}
