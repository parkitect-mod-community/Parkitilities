using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Parkitilities
{
    public static class TrainBuilderLiterals
    {
        public const String SETUP_GROUP = "SETUP";
    }

    public class FrontBackTrainBuilder<TResult> : BaseTrainBuilder<TResult,FrontBackTrainBuilder<TResult>>, IBuildable<TResult> where TResult : CoasterCarInstantiatorFrontMiddleBack
    {

        public FrontBackTrainBuilder()
        {
        }

        public FrontBackTrainBuilder<TResult> BackVehicle<TVehicle>(IBuildable<TVehicle> vehicleBuilder, AssetManagerLoader loader)
            where TVehicle : Vehicle
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "BACK_VEHICLE", (container) =>
            {
                TVehicle vehicle = vehicleBuilder.Build(loader);
                container.rearVehicleGO = vehicle;
            });
            return this;
        }

        public FrontBackTrainBuilder<TResult> BackVehicle<TVehicle>(TVehicle vehicle, AssetManagerLoader loader)
            where TVehicle : Vehicle
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "BACK_VEHICLE", (container) =>
            {
                container.rearVehicleGO = vehicle;
            });
            return this;
        }

        public TResult Register(AssetManagerLoader loader, String refrenceName)
        {
            TResult result = Build(loader);
            foreach (Attraction attractionObject in ScriptableSingleton<AssetManager>.Instance.getAttractionObjects())
            {
                if (attractionObject is TrackedRide && attractionObject.getUnlocalizedName() == refrenceName)
                {
                    ScriptableSingleton<AssetManager>.Instance.registerCoasterCarInstantiator(
                        attractionObject.getReferenceName(), (CoasterCarInstantiator) result);
                    break;
                }
            }

            return result;
        }

        public TResult Build(AssetManagerLoader loader)
        {
            if (!ContainsTag("GUID"))
                throw new Exception("Guid unset");
            if (!ContainsTag("DISPLAY"))
                throw new Exception("Display unset");
            if (!ContainsTag("MIDDLE_VEHICLE"))
                throw new Exception("Middle Train is unset");


            TResult instantiate = ScriptableObject.CreateInstance<TResult>();
            ApplyGroup(TrainBuilderLiterals.SETUP_GROUP, instantiate);
            return instantiate;
        }

    }
}
