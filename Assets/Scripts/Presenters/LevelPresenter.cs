using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Data;
using System;
using Models;
using Messages;

namespace Presenters
{
    public class LevelPresenter : BasePresenter
    {
        [SerializeField] GameObject PlayerPrefab;
        [SerializeField] Text hpCounter;
        [SerializeField] Text scoreCounter;
        [SerializeField] Text winScoreText;
        [SerializeField] Text winText;
        [SerializeField] Text loseText;

        private LevelModel level;
        private CompositeDisposable levelSubscriptions;

        void Start()
        {
            levelSubscriptions = new CompositeDisposable();

            DataHub.GameState
               .Subscribe(ProcessState)
               .AddTo(this);
        }

        private void ProcessState(GameState state)
        {
            switch (state)
            {
                case GameState.Started:
                    StartGame();
                    break;
                case GameState.Losed:
                    LoseGame();
                    break;
                case GameState.Win:
                    WinGame();
                    break;
            }
        }

        private void StartGame()
        {
            level = new LevelModel(200);
            level.CurrentScore.SubscribeToText(scoreCounter);
            winScoreText.text = level.WinScore.ToString();

            level.IsWin
                .Where(isWin => isWin)
                .Subscribe(_ => DataHub.GameState.Value = GameState.Win);

            MessageBroker.Default.Receive<PlayerHpChangedMessage>()
                .SubscribeToText(hpCounter, message => message.Value.ToString())
                .AddTo(levelSubscriptions);

            MessageBroker.Default.Receive<ScoreChangedMessage>()
                .Subscribe(message => level.CurrentScore.Value += message.Delta)
                .AddTo(levelSubscriptions);

            Instantiate(PlayerPrefab);
        }

        private void WinGame()
            => EndGame(winText);

        private void LoseGame()
            => EndGame(loseText);

        private void EndGame(Text endText)
        {
            endText.gameObject.SetActive(true);

            Observable
                .Timer(TimeSpan.FromSeconds(3))
                .Subscribe(_ =>
                {
                    endText.gameObject.SetActive(false);
                    DataHub.GameState.Value = GameState.Stopped;
                })
                .AddTo(this);

            CleanUp();
        }

        private void CleanUp()
        {
            level = null;
            levelSubscriptions.Clear();
        }
    }
}
