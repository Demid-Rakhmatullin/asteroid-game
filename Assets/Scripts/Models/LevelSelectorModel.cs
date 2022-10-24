using Data;
using Persistence;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Models
{
    public class LevelSelectorModel : BaseModel
    {
        public IReactiveProperty<LevelDataState> CurrentState;
        public readonly string levelName;

        private readonly string levelId;
        private readonly LevelData levelData;

        public LevelSelectorModel(string levelId, string levelName, LevelData levelData)
        {
            CurrentState = new ReactiveProperty<LevelDataState>();

            this.levelId = levelId;
            this.levelName = levelName;
            this.levelData = levelData;
        }

        public void CheckState(LevelDataState? previousLevelState)
        {
            if (levelData.State == LevelDataState.NotGenerated && (previousLevelState == null || previousLevelState == LevelDataState.Completed))
            {
                levelData.State = LevelDataState.Open;
                GenerateLevelSettings(levelData);
                LevelDataRepository.Instance.Create(levelId, levelData);
            }

            CurrentState.Value = levelData.State;
        }

        public void UpdateHub()
        {
            DataHub.CurrentLevelId = levelId;
            DataHub.CurrentLevelName = levelName;
            DataHub.CurrentLevelData = levelData;
            DataHub.GameState.Value = GameState.Started;
        }

        private void GenerateLevelSettings(LevelData level)
        {
            level.WinScore = Random.Range(1500, 3000) / 100 * 100;
            level.ObstacleSpawnMaxDelay = Random.Range(0.7f, 2f);

            //тут я успел сделать по-нормальному; пока рандомно, фактически, только условие "есть или нет большой астероид"; но, для универсальности в LevelData массив obstacl-ов, а не просто флаг на большой астероид;
            var randomObstacleTypes = new List<ObstacleType>() { ObstacleType.AsteroidMedium };
            var hasBigAsteroid = Random.Range(0, 2);
            if (hasBigAsteroid > 0)
                randomObstacleTypes.Add(ObstacleType.AsteroidBig);
            level.ObstacleTypes = randomObstacleTypes.ToArray();
        }
    }
}
