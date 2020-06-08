using System;
using System.Collections.Generic;
using UnityEngine;

namespace Parkitilities
{

    public static class TrackBuilderLiterals
    {
        public const String SetupGroup = "SETUP";
        public const String ConfigurationGroup = "CONFIGURATION";
        public const String PostConfigurationGroup = "POST_CONFIGURATION";
    }


    public class TrackRideBuilder<TResult>: BaseBuilder<BaseObjectContainer<TResult>>, IApply<TResult>, IBuildable<TResult> where TResult: TrackedRide
    {
        private GameObject _go;

        public TrackRideBuilder(GameObject go)
        {
            _go = go;
        }

        public TrackRideBuilder<TResult> Generator<TMeshGenerator>(TMeshGenerator generator)
            where TMeshGenerator : MeshGenerator
        {
            AddOrReplaceByTag(TrackBuilderLiterals.SetupGroup, "MESH_GENERATOR", (handler) =>
            {
                handler.Target.meshGenerator = generator;
            });
            return this;
        }

        public TrackRideBuilder<TResult> Generator<TMeshGenerator>(IBuildable<TMeshGenerator> generator,
            AssetManagerLoader loader)
            where TMeshGenerator : MeshGenerator
        {
            AddOrReplaceByTag(TrackBuilderLiterals.SetupGroup, "MESH_GENERATOR",
                (handler) => { handler.Target.meshGenerator = generator.Build(loader); });
            return this;
        }

        public TrackRideBuilder<TResult> Support<TSupport>(TSupport support) where TSupport : SupportInstantiator
        {
            AddOrReplaceByTag(TrackBuilderLiterals.ConfigurationGroup, "SUPPORT", (handler) =>
            {
                handler.Target.meshGenerator.supportInstantiator = support;
            });
            return this;
        }

        public TrackRideBuilder<TResult> AddTrain<TTrain>(AssetManagerLoader loader,IBuildable<TTrain> train) where TTrain : CoasterCarInstantiator
        {
            AddStep(TrackBuilderLiterals.PostConfigurationGroup, "ADD_TRAIN", (handler) =>
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
            AddOrReplaceByTag(TrackBuilderLiterals.ConfigurationGroup, "CAN_CHANGE_LAPS", (handler) =>
            {
                handler.Target.canChangeLaps = state;
                handler.Target.defaultLaps = defaultLaps;
            });
            return this;
        }

        public TrackRideBuilder<TResult> CanCrashFromCollision(bool state)
        {
            AddOrReplaceByTag(TrackBuilderLiterals.ConfigurationGroup, "CAN_CRASH_FROM_COLLISION",
                (handler) => { handler.Target.canCrashFromCollisions = state; });
            return this;
        }

        public TrackRideBuilder<TResult> AddTrain<TTrain>(TTrain train) where TTrain : CoasterCarInstantiator
        {

            AddStep(TrackBuilderLiterals.PostConfigurationGroup, "ADD_TRAIN", (handler) =>
            {
                if (handler.Target.carTypes == null)
                {
                    handler.Target.carTypes = new CoasterCarInstantiator[] {train};
                }
                else
                {
                    Array.Resize(ref handler.Target.carTypes, handler.Target.carTypes.Length + 1);
                    handler.Target.carTypes[handler.Target.carTypes.Length - 1] = train;
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
