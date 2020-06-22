using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace Parkitilities
{

    public class TrackRideBuilder<TResult> : BaseBuilder<BaseObjectContainer<TResult>>, IApply<TResult>,
        IBuildable<TResult> where TResult : TrackedRide
    {
        private GameObject _go;

        public TrackRideBuilder(GameObject go)
        {
            _go = go;
        }

        public TrackMeshGeneratorBuilder<TMeshGenerator, TResult, TrackRideBuilder<TResult>> Generator<TMeshGenerator>()
            where TMeshGenerator : MeshGenerator
        {
            var target = new TrackMeshGeneratorBuilder<TMeshGenerator, TResult, TrackRideBuilder<TResult>>(this);
            AddStep("MESH_GENERATOR",
                handler => { handler.Target.meshGenerator = ScriptableObject.CreateInstance<TMeshGenerator>(); });
            return target;
        }

        /// <summary>
        /// build mesh generator from target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="loader"></param>
        /// <typeparam name="TMeshGenerator"></typeparam>
        /// <returns></returns>
        public TrackMeshGeneratorBuilder<TMeshGenerator, TResult, TrackRideBuilder<TResult>> Generator<TMeshGenerator>(MeshGenerator target, AssetManagerLoader loader)
            where TMeshGenerator : MeshGenerator
        {
            var builder = new TrackMeshGeneratorBuilder<TMeshGenerator, TResult, TrackRideBuilder<TResult>>(this);
            AddStep("MESH_GENERATOR",
                handler =>
                {
                    handler.Target.meshGenerator = ScriptableObject.CreateInstance<TMeshGenerator>();

                    handler.Target.meshGenerator.stationPlatformGO = UnityEngine.Object.Instantiate(target.stationPlatformGO);
                    handler.Target.meshGenerator.stationHandRailGO = UnityEngine.Object.Instantiate(target.stationHandRailGO);
                    handler.Target.meshGenerator.material = target.material;
                    handler.Target.meshGenerator.liftMaterial = target.liftMaterial;
                    handler.Target.meshGenerator.frictionWheelsGO = UnityEngine.Object.Instantiate(target.frictionWheelsGO);
                    handler.Target.meshGenerator.supportInstantiator = target.supportInstantiator;
                    handler.Target.meshGenerator.crossBeamGO = UnityEngine.Object.Instantiate(target.crossBeamGO);
                    handler.Target.meshGenerator.customColors = target.customColors;
                    handler.Target.meshGenerator.tunnelMeshGenerator = target.tunnelMeshGenerator;
                    handler.Target.meshGenerator.lsmFinGO = target.lsmFinGO;

                    loader.HideGo(handler.Target.meshGenerator.frictionWheelsGO);
                    loader.HideGo(handler.Target.meshGenerator.crossBeamGO);
                    loader.HideGo(handler.Target.meshGenerator.stationHandRailGO);
                });
            return builder;
        }

        public TrackRideBuilder<TResult> AddTrain<TTrain>(AssetManagerLoader loader, IBuildable<TTrain> train)
            where TTrain : CoasterCarInstantiator
        {
            AddStep("ADD_TRAIN", (handler) =>
            {
                TTrain target = train.Build(loader);
                if (handler.Target.carTypes == null)
                {
                    handler.Target.carTypes = new CoasterCarInstantiator[] {target};
                }
                else
                {
                    Array.Resize(ref handler.Target.carTypes, handler.Target.carTypes.Length + 1);
                    handler.Target.carTypes[handler.Target.carTypes.Length - 1] = target;
                }
            });
            return this;
        }

        public TrackRideBuilder<TResult> ClearTrains()
        {
            AddStep("CLEAR_TRAIN", (handler) => { handler.Target.carTypes = new CoasterCarInstantiator[] { }; });
            return this;
        }

        /// <summary>
        /// Allows users to change laps
        /// </summary>
        /// <param name="defaultLaps"></param>
        /// <returns></returns>
        public TrackRideBuilder<TResult> CanChangeLaps(bool state, int defaultLaps = 1)
        {
            AddStep("CAN_CHANGE_LAPS", (handler) =>
            {
                handler.Target.canChangeLaps = state;
                handler.Target.defaultLaps = defaultLaps;
            });
            return this;
        }

        public TrackRideBuilder<TResult> CanCrashFromCollision(bool state)
        {
            AddStep("CAN_CRASH_FROM_COLLISION",
                (handler) => { handler.Target.canCrashFromCollisions = state; });
            return this;
        }

        public TrackRideBuilder<TResult> AddTrain<TTrain>(IBuildable<TTrain> train)
            where TTrain : CoasterCarInstantiator
        {
            AddStep("ADD_TRAIN", (handler) =>
            {
                if (handler.Target.carTypes == null)
                {
                    handler.Target.carTypes = new CoasterCarInstantiator[] {train.Build(handler.Loader)};
                }
                else
                {
                    Array.Resize(ref handler.Target.carTypes, handler.Target.carTypes.Length + 1);
                    handler.Target.carTypes[handler.Target.carTypes.Length - 1] = train.Build(handler.Loader);
                }
            });
            return this;
        }

        public TResult Build(AssetManagerLoader loader)
        {
            throw new System.NotImplementedException();
        }

        public TResult Apply()
        {
            throw new NotImplementedException();
        }
    }
}
