using System;
using UnityEngine;

namespace Parkitilities
{
    public static class TrackMeshGeneratorBuilderLiteral
    {
        public const String CONFIGURATION_GROUP = "CONFIGURATION";
    }

    public class TrackMeshGeneratorBuilder<TResult> : BaseBuilder<BaseObjectContainer<TResult>>, IBuildable<TResult>
        where TResult : MeshGenerator
    {
        public TrackMeshGeneratorBuilder<TResult> CrossBeam(GameObject gameObject)
        {
            AddOrReplaceByTag(TrackMeshGeneratorBuilderLiteral.CONFIGURATION_GROUP, "CROSS_BEAM",
                (handler) => { handler.Target.crossBeamGO = gameObject; });
            return this;
        }

        public TrackMeshGeneratorBuilder<TResult> FrictionWheel(GameObject gameObject)
        {
            AddOrReplaceByTag(TrackMeshGeneratorBuilderLiteral.CONFIGURATION_GROUP, "FRICTION_WHEEL",
                (handler) => { handler.Target.crossBeamGO = gameObject; });
            return this;
        }

        public TrackMeshGeneratorBuilder<TResult> LsmFins(GameObject gameObject)
        {

            AddOrReplaceByTag(TrackMeshGeneratorBuilderLiteral.CONFIGURATION_GROUP, "LSM_FIN",
                (handler) => { handler.Target.lsmFinGO = gameObject; });
            return this;
        }

        public TrackMeshGeneratorBuilder<TResult> StationHandRail(GameObject gameObject)
        {

            AddOrReplaceByTag(TrackMeshGeneratorBuilderLiteral.CONFIGURATION_GROUP, "STATION_HAND_RAIL",
                (handler) => { handler.Target.stationHandRailGO = gameObject; });
            return this;
        }

        public TrackMeshGeneratorBuilder<TResult> StationPlatform(StationPlatform platform)
        {
            AddOrReplaceByTag(TrackMeshGeneratorBuilderLiteral.CONFIGURATION_GROUP, "STATION_HAND_RAIL",
                (handler) => { handler.Target.stationPlatformGO = platform; });
            return this;
        }

        public TResult Build(AssetManagerLoader loader)
        {
            return null;
        }
    }
}
