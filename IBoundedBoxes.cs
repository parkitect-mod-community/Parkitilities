using UnityEngine;

namespace Parkitilities
{
    public interface IBoundedBoxes<TSelf>
    {
        TSelf AddBoundingBox(Bounds bound, BoundingVolume.Layers layers = BoundingVolume.Layers.Buildvolume);
        TSelf ClearBoundingBoxes();
    }
}
