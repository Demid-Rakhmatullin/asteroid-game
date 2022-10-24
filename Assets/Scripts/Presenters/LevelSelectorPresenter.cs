using Data;
using Persistence;
using System.Linq;
using UnityEngine;
using UniRx;
using Models;
using UnityEngine.UI;

namespace Presenters
{
    public class LevelSelectorPresenter : BasePresenter
    {
        [SerializeField] string levelId;
        [SerializeField] string levelName;
        [SerializeField] LevelSelectorPresenter previousLevelSelector;

        [SerializeField] Text levelNameText;
        [SerializeField] Button selectButton;
        [SerializeField] Image markerImage;
        [SerializeField] Sprite openSprite, closedSprite,
            completedSprite;

        [SerializeField] LevelData levelData;
        private LevelSelectorModel model;

        public LevelDataState State => levelData.State;

        private LevelDataState? PreviousLevelState
            => previousLevelSelector != null ? (LevelDataState?)previousLevelSelector.State : null;

        void Awake()
        {
            // в Awake, что бы в Start previousLevelSelector был актуален
            levelData = LevelDataRepository.Instance.Get(levelId) ?? new LevelData();
        }

        void Start()
        {
            model = new LevelSelectorModel(levelId, levelName, levelData);

            model.CurrentState.Subscribe(SetMarkerSprite);
            model.CurrentState.Subscribe(EnableButton);
            levelNameText.text = model.levelName;

            selectButton
                .OnClickAsObservable()
                .Subscribe(_ => model.UpdateHub());

            DataHub.GameState
                .Where(s => s == GameState.SelectLevel)
                .Subscribe(_ => model.CheckState(PreviousLevelState))
                .AddTo(this);
        }

        private void SetMarkerSprite(LevelDataState state)
        {
            Sprite sprite = null;
            switch (state)
            {
                case LevelDataState.NotGenerated:
                    sprite = closedSprite;
                    break;
                case LevelDataState.Completed:
                    sprite = completedSprite;
                    break;
                case LevelDataState.Open:
                    sprite = openSprite;
                    break;
            }

            markerImage.sprite = sprite;
        }

        private void EnableButton(LevelDataState state)
        {
            selectButton.enabled = state == LevelDataState.Open 
                || state == LevelDataState.Completed;
        }
    }
}
