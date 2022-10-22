using UnityEngine;

namespace Models
{
    public class ObstacleSpawnerModel : BaseModel
    {
        public float NextSpawnTime { get; private set; }

        private GameObject[] obstacles;
        private float minDelay;
        private float maxDelay;
        private float minXShift;
        private float maxXShift;

        public ObstacleSpawnerModel(GameObject[] obstacles, float minDelay, float maxDelay, float minXShift, float maxXShift)
        {
            this.obstacles = obstacles;
            this.minDelay = minDelay;
            this.maxDelay = maxDelay;
            this.minXShift = minXShift;
            this.maxXShift = maxXShift;
        }

        public ObstacleData CreateObstacle(float time)
        {
            var xShift = Random.Range(minXShift, maxXShift);

            var obstacleIndex = Random.Range(0, 3);
            var obstacle = obstacles[obstacleIndex];

            NextSpawnTime = time + Random.Range(minDelay, maxDelay);
            return new ObstacleData
            { 
                Prefab = obstacle, 
                XShift = xShift 
            };
        }

        public struct ObstacleData
        {
            public GameObject Prefab;
            public float XShift;
        }
    }
}
