using System;
using System.Reflection;
using UnityEngine;

namespace Parkitilities
{
    public class TrackRideObjectContainer<TResult> : BaseObjectContainer<TResult>
    {
        public TrackRideObjectContainer(AssetManagerLoader loader, TResult target, GameObject go) : base(loader, target,
            go)
        {
        }
    }

    public class TrackMeshGeneratorBuilder<TResult, TResult1, TFrom>
        where TResult : MeshGenerator
        where TFrom : TrackRideBuilder<TResult1>
        where TResult1 : TrackedRide

    {
        private readonly TFrom _from;

        public TrackMeshGeneratorBuilder(TFrom from)
        {
            _from = from;
        }

        public TrackMeshGeneratorBuilder<TResult, TResult1, TFrom> CrossBeam(GameObject gameObject, bool bentCrossBeams,
            AssetManagerLoader loader)
        {
            _from.AddStep("CROSS_BEAM",
                (handler) =>
                {
                    GameObject go = GameObject.Instantiate(gameObject);
                    loader.HideGo(go);
                    typeof(TResult)
                        .GetField("bendCrossBeamsToTrackShape",
                            BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic)
                        .SetValue(handler.Target.meshGenerator, bentCrossBeams);
                    handler.Target.meshGenerator.crossBeamGO = go;
                });
            return this;
        }

        public TrackMeshGeneratorBuilder<TResult, TResult1, TFrom> HeartLineOffset(float offset)
        {
            _from.AddStep("HEARTLINE_OFFSET",
                (handler) =>
                {
                    handler.Target.meshGenerator.heartlineOffset = offset;
                });
            return this;
        }

        public TrackMeshGeneratorBuilder<TResult, TResult1, TFrom> Support<TSupport>(Action<TSupport> consumer)
            where TSupport : SupportInstantiator
        {
            _from.AddStep("SUPPORT", (handler) =>
            {
                TSupport support = ScriptableObject.CreateInstance<TSupport>();
                consumer(support);
                handler.Target.meshGenerator.supportInstantiator = support;
            });
            return this;
        }


        public TrackMeshGeneratorBuilder<TResult, TResult1, TFrom> FrictionWheel(GameObject gameObject)
        {
            _from.AddStep("FRICTION_WHEEL",
                (handler) =>
                {
                    handler.Target.meshGenerator.frictionWheelsGO = gameObject;

                    FrictionWheelAnimator animator = handler.Target.meshGenerator.frictionWheelsGO
                        .GetComponent<FrictionWheelAnimator>();
                    if (animator == null)
                    {
                        handler.Target.meshGenerator.frictionWheelsGO.AddComponent<FrictionWheelAnimator>();
                    }

                });
            return this;
        }

        public TrackMeshGeneratorBuilder<TResult, TResult1, TFrom> LsmFins(GameObject gameObject)
        {

            _from.AddStep("LSM_FIN",
                (handler) => { handler.Target.meshGenerator.lsmFinGO = gameObject; });
            return this;
        }

        public TrackMeshGeneratorBuilder<TResult, TResult1, TFrom> StationHandRail(GameObject gameObject)
        {

            _from.AddStep("STATION_HAND_RAIL",
                (handler) => { handler.Target.meshGenerator.stationHandRailGO = gameObject; });
            return this;
        }

        public TrackMeshGeneratorBuilder<TResult, TResult1, TFrom> StationPlatform(StationPlatform platform)
        {
            _from.AddStep("STATION_HAND_RAIL",
                (handler) => { handler.Target.meshGenerator.stationPlatformGO = platform; });
            return this;
        }

        public TFrom End()
        {
            return _from;
        }
    }
}
