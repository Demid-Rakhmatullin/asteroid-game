using System.Linq;
using UnityEngine;
using UniRx;
using Models;
using Data;
using System;

namespace Presenters
{
    public class ObstacleSpawnerPresenter : BasePresenter
    {
        [SerializeField] float minDelay;
        [SerializeField] float maxDelay;

        [SerializeField] GameObject[] obstacles;

        private ObstacleSpawnerModel model;
        private IDisposable spawnSubscription;

        void Start()
        {
            model = new ObstacleSpawnerModel(obstacles, minDelay, maxDelay, -55/2, 55/2);

            DataHub.GameState
                .Where(s => s == GameState.Started)
                .Subscribe(_ => StartSpawn())
                .AddTo(this);

            DataHub.GameState
                .Where(s => s == GameState.Stopped || s == GameState.Win)
                .Subscribe(_ => StopSpawn())
                .AddTo(this);
        }

        private void StartSpawn()
        {
            spawnSubscription = 
                Observable
                    .EveryUpdate()
                    .Where(_ => Time.time > model.NextSpawnTime)
                    .Subscribe(_ =>
                        {
                            var obstacleData = model.CreateObstacle(Time.time);
                            Instantiate(obstacleData.Prefab, 
                                new Vector3(transform.position.x + obstacleData.XShift,
                                    transform.position.y,
                                    transform.position.z),
                                Quaternion.identity,
                                transform);
                        })
                    .AddTo(this);
        }

        private void StopSpawn()
        {
            spawnSubscription?.Dispose();
        }
    }
}
