using Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Presenters
{
    public class HomeScreenPresenter : BasePresenter
    {
        [SerializeField] GameObject homeUI;
        [SerializeField] Button startButton;

        //private HomeModel model = new HomeModel();

        void Start()
        {
            startButton
                .OnClickAsObservable()
                .Subscribe(_ => StartGame());
        }
        
        private void StartGame()
        {
            homeUI.SetActive(false);
            GameStaticModel.State.Value = GameState.Started;
        }
    }
}
