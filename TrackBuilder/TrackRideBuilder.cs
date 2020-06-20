using System;
using System.Collections.Generic;
using UnityEngine;

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

        public class TrackMeshGeneratorBuilderTrackRide<TResult, TFrom> : TrackMeshGeneratorBuilder<TResult>
            where TResult : MeshGenerator
        {
            private readonly TFrom _from;

            public TrackMeshGeneratorBuilderTrackRide(TFrom from)
            {
                this._from = from;
            }

            public TFrom End()
            {
                return _from;
            }
        }


        public TrackMeshGeneratorBuilderTrackRide<TMeshGenerator,TrackRideBuilder<TResult>> Generator<TMeshGenerator>()
            where TMeshGenerator : MeshGenerator
        {
            var target = new TrackMeshGeneratorBuilderTrackRide<TMeshGenerator,TrackRideBuilder<TResult>>(this);
            AddStep("MESH_GENERATOR", (handler) => { handler.Target.meshGenerator = target.Build(handler.Loader,handler.Target); });
            return target;
        }

        // public TrackRideBuilder<TResult> MeshGenerator<TMeshGenerator>(IBuildable<TMeshGenerator> generator)
        //     where TMeshGenerator : MeshGenerator
        // {
        //     AddStep("MESH_GENERATOR",
        //         (handler) => { handler.Target.meshGenerator = generator.Build(handler.Loader); });
        //     return this;
        // }

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

        public TrackRideBuilder<TResult> AddTrain<TTrain>(IBuildable<TTrain> train) where TTrain : CoasterCarInstantiator
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
