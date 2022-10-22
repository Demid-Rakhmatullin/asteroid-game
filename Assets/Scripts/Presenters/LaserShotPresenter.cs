using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Utils;

namespace Presenters
{
    public class LaserShotPresenter : BasePresenter
    {
        [SerializeField] float speed;

        void Start()
        {
            Launch();

            this.OnTriggerEnterAsObservable()
                .Where(other => other.CompareTagEnum(Tags.Obstacle))
                .Subscribe(_ => Destroy(gameObject));
        }

        private void Launch()
            => GetComponent<Rigidbody>().velocity = Vector3.forward * speed;
    }
}
