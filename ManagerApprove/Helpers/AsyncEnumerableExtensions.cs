using System.Linq;

namespace ManagerApprove.Helpers
{
    public static class AsyncEnumerableExtensions
    {
        //public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerable<T> source)
        //{
        //    foreach (var item in source)
        //    {
        //        await Task.Yield(); // This ensures the method is asynchronous
        //        yield return item;
        //    }
        //}
        public static async Task<int> CountAsync<T>(this IAsyncEnumerable<T> source)
        {
            int count = 0;
            await foreach (var item in source)
            {
                count++;
            }
            return count;
        }
        public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
        {
            var list = new List<T>();
            await foreach (var item in source)
            {
                list.Add(item);
            }
            return list;
        }

        public static async Task<List<T>> SkipAndTakeToListAsync<T>(this IAsyncEnumerable<T> source, int skip, int take)
        {
            return await source.Skip(skip).Take(take).ToListAsync();
        }

    }
}
