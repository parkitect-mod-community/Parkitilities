using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Parkitilities
{
    public static class TrainBuilderLiterals
    {
        public const String SETUP_GROUP = "SETUP";
    }

    public class TrainBuilder<T> : BaseBuilder<T>, IBuildable<T> where T : CoasterCarInstantiatorFrontMiddleBack
    {

        public TrainBuilder()
        {
        }

        public TrainBuilder<T> Id(String uid)
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "GUID", inst =>
            {
                inst.name = uid;
            });
            return this;
        }

        public TrainBuilder<T> DisplayName(String display)
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "DISPLAY", inst =>
            {
                inst.displayName = display;
            });
            return this;
        }

        public TrainBuilder<T> FrontVehicle<TVehicle>(TVehicle vehicle)
            where TVehicle : Vehicle
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "FRONT_VEHICLE",
                (instantiator) =>
                {
                    instantiator.frontVehicleGO = vehicle;
                });
            return this;
        }

        public TrainBuilder<T> FrontVehicle<TVehicle>(IBuildable<TVehicle> vehicleBuilder, AssetManagerLoader loader)
            where TVehicle : Vehicle
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "FRONT_VEHICLE",
                (instantiator) =>
                {
                    TVehicle vehicle = vehicleBuilder.Build(loader);
                    instantiator.frontVehicleGO = vehicle;
                });
            return this;
        }

        public TrainBuilder<T> MiddleVehicle<TVehicle>(TVehicle vehicle, AssetManagerLoader loader)
            where TVehicle : Vehicle
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "MIDDLE_VEHICLE",
                instantiator =>
                {
                    instantiator.vehicleGO = vehicle;
                });
            return this;
        }


        public TrainBuilder<T> MiddleVehicle<TVehicle>(IBuildable<TVehicle> vehicleBuilder, AssetManagerLoader loader)
            where TVehicle : Vehicle
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "MIDDLE_VEHICLE",
                (instantiator) =>
                {
                    TVehicle vehicle = vehicleBuilder.Build(loader);
                    instantiator.vehicleGO = vehicle;
                });
            return this;
        }

        public TrainBuilder<T> MaxTrainLength(int length)
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "MAX_TRAIN_LENGTH", (inst) =>
            {
                inst.maxTrainLength = length;
            });
            return this;
        }

        public TrainBuilder<T> MinTrainLength(int length)
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "MIN_TRAIN_LENGTH", (inst) =>
            {
                inst.maxTrainLength = length;
            });
            return this;
        }


        public TrainBuilder<T> DefaultTrainLength(int length)
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "DEFAULT_TRAIN_LENGTH", (inst) =>
            {
                inst.defaultTrainLength = length;
            });
            return this;
        }

        public TrainBuilder<T> RainProtection(float value)
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "RAIN_PROTECTION", (inst) =>
            {
                inst.rainProtection = value;
            });
            return this;
        }


        public TrainBuilder<T> BackVehicle<TVehicle>(IBuildable<TVehicle> vehicleBuilder, AssetManagerLoader loader)
            where TVehicle : Vehicle
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "BACK_VEHICLE", (instantiator) =>
            {
                TVehicle vehicle = vehicleBuilder.Build(loader);
                instantiator.rearVehicleGO = vehicle;
            });
            return this;
        }

        public TrainBuilder<T> BackVehicle<TVehicle>(TVehicle vehicle, AssetManagerLoader loader)
            where TVehicle : Vehicle
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "BACK_VEHICLE", (instantiator) =>
            {
                instantiator.rearVehicleGO = vehicle;
            });
            return this;
        }

        public T Build(AssetManagerLoader loader)
        {
            if (!ContainsTag("GUID"))
            {
                throw new Exception("Guid unset");
            }

            if (!ContainsTag("DISPLAY"))
            {
                throw new Exception("Display unset");
            }

            if (!ContainsTag("MIDDLE_VEHICLE"))
            {
                throw new Exception("Middle Train is unset");
            }

            if (!ContainsTag("DEFAULT_TRAIN_LENGTH"))
            {
                throw new Exception("Default Train length is unset");
            }

            if (!ContainsTag("MIN_TRAIN_LENGTH"))
            {
                throw new Exception("Min Train length is unset");
            }

            if (!ContainsTag("MAX_TRAIN_LENGTH"))
            {
                throw new Exception("Max Train length is Unset");
            }

            T instantiate = ScriptableObject.CreateInstance<T>();
            ApplyGroup(TrainBuilderLiterals.SETUP_GROUP, instantiate);
            return instantiate;
        }
    }
}
