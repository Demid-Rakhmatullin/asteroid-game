using Data;
using System.Linq;
using UnityEngine;
using UniRx;

namespace Presenters
{
    public class SelectLevelScreenPresenter : BasePresenter
    {
        [SerializeField] GameObject selectLevelUI;

        void Start()
        {
            DataHub.GameState
               .Where(s => s == GameState.SelectLevel)
               .Subscribe(_ => selectLevelUI.SetActive(true))
               .AddTo(this);

            DataHub.GameState
              .Where(s => s == GameState.Started)
              .Subscribe(_ => selectLevelUI.SetActive(false))
              .AddTo(this);
        }
    }
}
