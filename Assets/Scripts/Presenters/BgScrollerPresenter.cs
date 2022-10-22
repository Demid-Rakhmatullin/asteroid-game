using System.Linq;
using UnityEngine;
using UniRx;
using Models;

namespace Presenters
{
    public class BgScrollerPresenter : BasePresenter
    {
        [SerializeField] float speed;

        private Vector3 startPos; //в model?

        void Start()
        {
            startPos = transform.position;

            GameStaticModel.State
                .Where(s => s == GameState.Started)
                .Subscribe(_ => StartScroll())
                .AddTo(this);
        }

        private void StartScroll()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                    {
                        var shift = Mathf.Repeat(Time.time * speed, transform.localScale.y);
                        transform.position = startPos + Vector3.back * shift;
                    })
                .AddTo(this);
        }
    }
}
