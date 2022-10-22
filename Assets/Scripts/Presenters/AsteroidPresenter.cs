using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Utils;

namespace Presenters
{
    public class AsteroidPresenter : BasePresenter
    {
        [SerializeField] float rotationSpeed;
        [SerializeField] float minSpeed;
        [SerializeField] float maxSpeed;
        [SerializeField] GameObject explosion;

        void Start()
        {
            Launch();

            this.OnTriggerEnterAsObservable()
                .Where(other => other.CompareTagEnum(Tags.Player, Tags.PlayerProjectile))
                .Subscribe(Explode);
        }

        private void Launch()
        {
            var rb = GetComponent<Rigidbody>();
            rb.angularVelocity = Random.insideUnitSphere * rotationSpeed;
            rb.velocity = Vector3.back * Random.Range(minSpeed, maxSpeed);
        }

        private void Explode(Collider other)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
