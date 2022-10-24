using Data;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Utils;

namespace Persistence
{
    public sealed class LevelDataRepository : BasicSingleton<LevelDataRepository>
    {
        //сделал блокировки, на всякий случай; не думаю, что это будет сильно тормозить, т.к. обновление кеша происходит относительно редко;
        private static readonly object @lock = new object();
        
        private readonly string filePath = Application.persistentDataPath + "/level_data.astsave";
        private readonly BinaryFileDataProvider provider = new BinaryFileDataProvider();

        private Dictionary<string, LevelData> cache;
        private bool updateData;

        private Dictionary<string, LevelData> GetCache()
        {
            if (cache == null)
                RefreshCache();
            return cache;
        }

        private void RefreshCache()
        {
            lock (@lock)
            {
                if (cache == null)
                {
                    cache = provider.Load<Dictionary<string, LevelData>>(filePath);

                    if (cache == null)
                        cache = new Dictionary<string, LevelData>();
                }
            }
        }

        private void UpdateFile()
        {
            if (!updateData)
                lock (@lock)               
                    if (!updateData)
                    {
                        updateData = true;
                        // в начале следующего фрейма файл обновится и флаг updateData разблокируется 
                        MainThreadDispatcher.StartUpdateMicroCoroutine(UpdateFileRoutine());
                    }                           
        }

        private IEnumerator UpdateFileRoutine()
        {
            updateData = false;
            provider.Save(filePath, cache);

            yield return null;
        }

        public void Create(string levelId, LevelData newData)
        {
            var cache = GetCache();

            if (cache.ContainsKey(levelId))
            {
                Debug.LogError("Key already exists!");
                return;
            }

            cache.Add(levelId, newData);
            UpdateFile();
        }

        public void Update(string levelId, LevelData newData)
        {
            var cache = GetCache();

            if (!cache.ContainsKey(levelId))
            {
                Debug.LogError("Key don't exists!");
                return;
            }

            cache[levelId] = newData;
            UpdateFile();
        }

        public LevelData Get(string levelId)
        {
            GetCache().TryGetValue(levelId, out LevelData result);           
            return result;
        }
    }
}
