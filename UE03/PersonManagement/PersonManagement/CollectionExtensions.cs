namespace PersonManagement
{
    public static class CollectionExtensions
    {
        // target ist ICollection, da es Add-Methode hat (IEnumerable kann nur iteriert werden)
        public static void AddAll<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                target.Add(item);
            }
        }
    }
}
