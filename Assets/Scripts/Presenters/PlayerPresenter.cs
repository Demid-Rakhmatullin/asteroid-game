using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Utils;
using Models;

namespace Presenters
{
    public class PlayerPresenter : BasePresenter
    {
        const string HORIZONTAL_AXIS = "Horizontal";
        const string VERTICAL_AXIS = "Vertical";
        const string FIRE_BTN = "Fire1";

        [SerializeField] float speed;
        [SerializeField] float tilt;
        [SerializeField] float moveBorderLeft, moveBorderRight, moveBorderBottom, moveBorderTop;

        [SerializeField] GameObject lazerShot;
        [SerializeField] Transform gun;
        [SerializeField] float shotDelay;

        [SerializeField] GameObject explosion;

        private Rigidbody rb;
        private float nextShotTime; // в PlayerModel?

        void Start()
        {
            rb = GetComponent<Rigidbody>();

            GameStaticModel.State
               .Where(s => s == GameState.Started)
               .Subscribe(_ => Activate())
               .AddTo(this);
        }

        private void Activate()
        {
            Observable
                .EveryUpdate()
                .Select(_ => (Input.GetAxis(HORIZONTAL_AXIS), Input.GetAxis(VERTICAL_AXIS)))
                .Subscribe(x => Move(x.Item1, x.Item2))
                .AddTo(this);

            Observable
                .EveryUpdate()
                .Where(_ => Input.GetButton(FIRE_BTN) && Time.time > nextShotTime)
                .Subscribe(_ => Shot())
                .AddTo(this);

            this.OnTriggerEnterAsObservable()
                .Where(other => other.CompareTagEnum(Tags.Obstacle))
                .Subscribe(_ => Explode());
        }

        private void Move(float horizontal, float vertical)
        {
            rb.velocity = new Vector3(horizontal, 0, vertical) * speed;
            rb.rotation = Quaternion.Euler(tilt * rb.velocity.z, 0, -tilt * rb.velocity.x);

            var posX = Mathf.Clamp(rb.position.x, moveBorderLeft, moveBorderRight);
            var posZ = Mathf.Clamp(rb.position.z, moveBorderBottom, moveBorderTop);
            rb.position = new Vector3(posX, rb.position.y, posZ);
        }

        private void Shot()
        {
            Instantiate(lazerShot, gun.position, Quaternion.identity, transform);
            nextShotTime = Time.time + shotDelay;
        }

        private void Explode()
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
