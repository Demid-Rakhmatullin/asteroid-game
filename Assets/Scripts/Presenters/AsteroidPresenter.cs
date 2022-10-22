﻿using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Utils;
using Data;
using Messages;

namespace Presenters
{
    public class AsteroidPresenter : BasePresenter
    {
        [SerializeField] float rotationSpeed;
        [SerializeField] float minSpeed;
        [SerializeField] float maxSpeed;
        [SerializeField] GameObject explosion;

        [SerializeField] int scoreBonus;

        void Start()
        {
            Launch();

            this.OnTriggerEnterAsObservable()
                .Where(other => other.CompareTagEnum(Tags.PlayerProjectile))
                .Subscribe(_ => GetShot());

            this.OnTriggerEnterAsObservable()
                .Where(other => other.CompareTagEnum(Tags.Player))
                .Subscribe(_ => Explode());

            DataHub.GameState
                .Where(s => s == GameState.Win)
                .Subscribe(_ => Explode())
                .AddTo(this);

            DataHub.GameState
                .Where(s => s == GameState.Stopped)
                .Subscribe(_ => Destroy(gameObject))
                .AddTo(this);            
        }

        private void Launch()
        {
            var rb = GetComponent<Rigidbody>();
            rb.angularVelocity = Random.insideUnitSphere * rotationSpeed;
            rb.velocity = Vector3.back * Random.Range(minSpeed, maxSpeed);
        }

        private void GetShot()
        {
            MessageBroker.Default.Publish(
                new ScoreChangedMessage { Delta = scoreBonus });

            Explode();
        }

        private void Explode()
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
