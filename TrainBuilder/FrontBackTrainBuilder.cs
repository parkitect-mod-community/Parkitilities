using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Parkitilities
{

    public class FrontBackTrainBuilder<TResult> : BaseTrainBuilder<TResult,FrontBackTrainBuilder<TResult>>, IBuildable<TResult> where TResult : CoasterCarInstantiatorFrontMiddleBack
    {

        public FrontBackTrainBuilder()
        {
        }

        public FrontBackTrainBuilder<TResult> BackVehicle<TVehicle>(IBuildable<TVehicle> vehicleBuilder)
            where TVehicle : Vehicle
        {
            AddStep("BACK_VEHICLE", (container) =>
            {
                TVehicle vehicle = vehicleBuilder.Build(container.Loader);
                container.Target.rearVehicleGO = vehicle;
            });
            return this;
        }

        public FrontBackTrainBuilder<TResult> BackVehicle<TVehicle>(TVehicle vehicle)
            where TVehicle : Vehicle
        {
            AddStep( "BACK_VEHICLE", (container) =>
            {
                container.Target.rearVehicleGO = vehicle;
            });
            return this;
        }

        /// <summary>
        /// Register to an existing coaster that has been loaded
        /// </summary>
        /// <param name="loader"></param>
        /// <param name="attractionName"></param>
        /// <returns></returns>
        public TResult Register(AssetManagerLoader loader, String attractionName)
        {
            TResult result = Build(loader);

            TrackedRide ride = AssetPackUtilities.TrackedRide(attractionName);
            if (ride != null)
            {
                ScriptableSingleton<AssetManager>.Instance.registerCoasterCarInstantiator(attractionName, result);
                Debug.Log("Register Coaster Ride " + attractionName);
            }
            else
            {
                Debug.Log("Cant find Coaster Ride " + attractionName);
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


            TResult result = ScriptableObject.CreateInstance<TResult>();
            Apply(new TrainContainer<TResult>(loader, result));
            loader.RegisterObject(result);
            return result;
        }

    }
}
