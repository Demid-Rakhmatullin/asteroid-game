using System;
using System.Linq;
using UnityEngine;
using UniRx;
using Data;

namespace Presenters
{
    public class BgScrollerPresenter : BasePresenter
    {
        [SerializeField] float speed;

        private Vector3 startPos;
        private IDisposable scrollSubscription;

        void Start()
        {
            startPos = transform.position;

            DataHub.GameState
                .Where(s => s == GameState.Started)
                .Subscribe(_ => StartScroll())
                .AddTo(this);

            DataHub.GameState
                .Where(s => s == GameState.Stopped)
                .Skip(1) //initial call
                .Subscribe(_ => StopScroll())
                .AddTo(this);
        }

        private void StartScroll()
        {
            scrollSubscription =
                Observable
                    .EveryUpdate()
                    .Subscribe(_ =>
                        {
                            var shift = Mathf.Repeat(Time.time * speed, transform.localScale.y);
                            transform.position = startPos + Vector3.back * shift;
                        })//;
                    .AddTo(this);
        }

        private void StopScroll()
        {
            scrollSubscription.Dispose();
            transform.position = startPos;
        }
    }
}
