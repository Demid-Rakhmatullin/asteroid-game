using Data;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Presenters
{
    public class HomeScreenPresenter : BasePresenter
    {
        [SerializeField] GameObject homeUI;
        [SerializeField] Button startButton;

        void Start()
        {
            DataHub.GameState
               .Where(s => s == GameState.Stopped)
               .Subscribe(_ => homeUI.SetActive(true))
               .AddTo(this);

            startButton
                .OnClickAsObservable()
                .Subscribe(_ => StartGame());
        }
        
        private void StartGame()
        {
            homeUI.SetActive(false);
            DataHub.GameState.Value = GameState.Started;
        }
    }
}
