using System;

namespace Data
{
    [Serializable]
    public class LevelData
    {        
        public LevelDataState State;
        public int WinScore;
        public ObstacleType[] ObstacleTypes;
        public float ObstacleSpawnMaxDelay;
    }
}
