using States;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Presenters
{
    public class HomePresenter : MonoBehaviour
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
            GameState.GameStarted.Value = true;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScript>().isStarted = true; //убрать
        }
    }
}
