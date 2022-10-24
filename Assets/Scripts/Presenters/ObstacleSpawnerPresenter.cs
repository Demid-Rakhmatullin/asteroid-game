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

        [SerializeField] AsteroidPresenter[] obstacles;

        private ObstacleSpawnerModel model;
        private IDisposable spawnSubscription;

        void Start()
        {           
            DataHub.GameState
                .Where(s => s == GameState.Started)
                .Subscribe(_ => StartSpawn())
                .AddTo(this);

            DataHub.GameState
                .Where(s => s == GameState.Stopped || s == GameState.SelectLevel || s == GameState.Win)
                .Subscribe(_ => StopSpawn())
                .AddTo(this);
        }

        private void StartSpawn()
        {
            model = new ObstacleSpawnerModel(
                DataHub.CurrentLevelData.ObstacleTypes,
                obstacles,
                minDelay,
                DataHub.CurrentLevelData.ObstacleSpawnMaxDelay,
                -55 / 2, 55 / 2);

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
