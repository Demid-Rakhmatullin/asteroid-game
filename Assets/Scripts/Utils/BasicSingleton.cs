
namespace Utils
{
    public abstract class BasicSingleton<T>
        where T : new()
    {
        private static readonly T instance = new T();

        public static T Instance => instance;
    }
}
