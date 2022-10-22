using System.Linq;
using UnityEngine;
using UniRx;
using Models;

namespace Presenters
{
    public class ObstacleSpawnerPresenter : BasePresenter
    {
        [SerializeField] float minDelay;
        [SerializeField] float maxDelay;

        [SerializeField] GameObject[] obstacles;

        private ObstacleSpawnerModel model;

        void Start()
        {
            model = new ObstacleSpawnerModel(obstacles, minDelay, maxDelay, -55/2, 55/2);

            GameStaticModel.State
                .Where(s => s == GameState.Started)
                .Subscribe(_ => StartSpawn())
                .AddTo(this);
        }

        private void StartSpawn()
        {
            Observable
                .EveryUpdate()
                .Where(_ => Time.time > model.NextSpawnTime)
                .Subscribe(_ =>
                    {
                        var obstacleData = model.CreateObstacle(Time.time);
                        Instantiate(obstacleData.Prefab, 
                            new Vector3(transform.position.x + obstacleData.XShift, transform.position.y, transform.position.z),
                            Quaternion.identity,
                            transform);
                    })
                .AddTo(this);
        }
    }
}
