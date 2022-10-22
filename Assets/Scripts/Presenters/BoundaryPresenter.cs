using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Presenters
{
    public class BoundaryPresenter : BasePresenter
    {
        void Start()
        {
            this.OnTriggerExitAsObservable()
                .Subscribe(other => Destroy(other.gameObject));
        }
    }
}
