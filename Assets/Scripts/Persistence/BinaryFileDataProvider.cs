using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Persistence
{
    public class BinaryFileDataProvider
    {
        private readonly BinaryFormatter formatter = new BinaryFormatter();

        public void Save(string filePath, object data)
        {
            using var stream = File.Open(filePath, FileMode.Create);
            formatter.Serialize(stream, data);
        }

        public T Load<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return default;

            using var stream = File.Open(filePath, FileMode.Open);
            return (T)formatter.Deserialize(stream);
        }
    }
}
