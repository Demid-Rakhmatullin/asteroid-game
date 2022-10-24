using Data;
using Persistence;
using UniRx;

namespace Models
{
    public class LevelModel : BaseModel
    {
        public IReactiveProperty<int> CurrentScore { get; private set; }

        public int WinScore { get; private set; }

        public IReadOnlyReactiveProperty<bool> IsWin { get; private set; }

        public LevelModel(int winScore)
        {
            CurrentScore = new ReactiveProperty<int>();
            WinScore = winScore;

            IsWin = CurrentScore
                .Select(s => s >= WinScore)
                .ToReactiveProperty();
        }

        public void ProcessWin()
        {
            var levelData = DataHub.CurrentLevelData;
            levelData.State = LevelDataState.Completed;
            LevelDataRepository.Instance.Update(DataHub.CurrentLevelId, levelData);
        }
    }
}
