using UnityEngine;

namespace Infrastructure
{
    public interface IObstacle
    {
        ObstacleType Type { get; }

        GameObject Prefab { get; }
    }
}
