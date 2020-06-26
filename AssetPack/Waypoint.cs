using System.Collections.Generic;

namespace Parkitilities.AssetPack
{
    public class Waypoint
    {
        public List<int> ConnectedTo = new List<int>();
        public float[] Position;
        public bool IsOuter;
        public bool IsRabbitHoleGoal;
    }
}
