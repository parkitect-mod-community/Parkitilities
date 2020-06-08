using UnityEngine;

namespace Parkitilities
{
    public class TrackRideObjectContainer<TResult> : BaseObjectContainer<TResult>
    {
        public TrackRideObjectContainer(AssetManagerLoader loader, TResult target, GameObject go) : base(loader, target, go)
        {
        }
    }

    public class TrackMeshGeneratorBuilder<TResult> : BaseBuilder<BaseObjectContainer<TResult>>,
        IBuildable<TResult,TrackedRide> where TResult : MeshGenerator
    {

        public TrackMeshGeneratorBuilder<TResult> CrossBeam(GameObject gameObject)
        {
            AddStep("CROSS_BEAM",
                (handler) => { handler.Target.crossBeamGO = gameObject; });
            return this;
        }

        public TrackMeshGeneratorBuilder<TResult> Support<TSupport>()
            where TSupport : SupportInstantiator
        {
            AddStep("SUPPORT", (handler) =>
            {
                //handler.Target.supportInstantiator = support;
            });
            return this;
        }

        public TrackMeshGeneratorBuilder<TResult> FrictionWheel(GameObject gameObject)
        {
            AddStep("FRICTION_WHEEL",
                (handler) => { handler.Target.crossBeamGO = gameObject; });
            return this;
        }

        public TrackMeshGeneratorBuilder<TResult> LsmFins(GameObject gameObject)
        {

            AddStep("LSM_FIN",
                (handler) => { handler.Target.lsmFinGO = gameObject; });
            return this;
        }

        public TrackMeshGeneratorBuilder<TResult> StationHandRail(GameObject gameObject)
        {

            AddStep("STATION_HAND_RAIL",
                (handler) => { handler.Target.stationHandRailGO = gameObject; });
            return this;
        }

        public TrackMeshGeneratorBuilder<TResult> StationPlatform(StationPlatform platform)
        {
            AddStep("STATION_HAND_RAIL",
                (handler) => { handler.Target.stationPlatformGO = platform; });
            return this;
        }

        public TResult Build(AssetManagerLoader loader, TrackedRide input)
        {
            throw new System.NotImplementedException();
        }
    }

}
