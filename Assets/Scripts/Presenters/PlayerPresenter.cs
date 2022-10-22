using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Utils;
using Models;
using System;

namespace Presenters
{
    public class PlayerPresenter : BasePresenter
    {
        const string HORIZONTAL_AXIS = "Horizontal";
        const string VERTICAL_AXIS = "Vertical";
        const string FIRE_BTN = "Fire1";
        const float DAMAGE_ANIM_INTERVAL = 0.15f;
        const int DAMAGE_ANIM_MAX_COUNT = 6;

        [SerializeField] int initialHp;
        [SerializeField] float speed;
        [SerializeField] float tilt;
        [SerializeField] float moveBorderLeft, moveBorderRight, moveBorderBottom, moveBorderTop;

        [SerializeField] GameObject lazerShot;
        [SerializeField] Transform gun;
        [SerializeField] float shotDelay;

        [SerializeField] GameObject mesh;
        [SerializeField] GameObject explosion;
        [SerializeField] Text hpText;

        private PlayerModel player;
        private Rigidbody rb;

        private int damageAnimCounter;
        private bool damageAnimInProgress;

        void Start()
        {
            rb = GetComponent<Rigidbody>();

            player = new PlayerModel(initialHp);
            player.CurrentHp.SubscribeToText(hpText);

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
                .Where(_ => Input.GetButton(FIRE_BTN) && Time.time > player.NextShotTime)
                .Subscribe(_ => Shot())
                .AddTo(this);

            this.OnTriggerEnterAsObservable()
                .Where(other => other.CompareTagEnum(Tags.Obstacle))
                .Subscribe(_ => ObstacleCollision());
            
            player.OnDamaged
                .Do(_ => damageAnimCounter = DAMAGE_ANIM_MAX_COUNT)
                .Where(_ => !damageAnimInProgress)
                .Subscribe(_ => DamageAnimation());
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
            player.NextShotTime = Time.time + shotDelay;
        }

        private void ObstacleCollision()
        {
            player.CurrentHp.Value--;
            //DamageAnimation();


            //Instantiate(explosion, transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }

        private void DamageAnimation()
        {
            damageAnimInProgress = true;
            Observable
                .Interval(TimeSpan.FromSeconds(DAMAGE_ANIM_INTERVAL))
                .TakeWhile(_ => damageAnimCounter > 0)
                .DoOnCompleted(() =>
                    {
                        mesh.SetActive(true);
                        damageAnimInProgress = false; 
                    })
                .Subscribe(_ =>
                    {
                        mesh.SetActive(!mesh.activeSelf);
                        damageAnimCounter--;
                    });
        }
    }
}
