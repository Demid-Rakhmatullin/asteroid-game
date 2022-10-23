using Presenters;
using UnityEngine;

namespace Infrastructure
{
    public class LevelSelectorLink : MonoBehaviour
    {
        [SerializeField] LevelSelectorPresenter levelPresenter;

        public LevelDataState State { get => levelPresenter.State; }
    }
}
