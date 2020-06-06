using System;
using UnityEngine;

namespace Parkitilities
{
    public class FrontMiddleBackTrainBuilder : TrainBuilder<CoasterCarInstantiatorFrontMiddleBack>
    {
        private IBaseVehicleBuilder<Vehicle> FrontVehicle { get; set; } = null;
        private IBaseVehicleBuilder<Vehicle> MiddleVehicle { get; set; } = null;
        private IBaseVehicleBuilder<Vehicle> RearVehicle { get; set; } = null;

        public String Display { get; }
        public String Guid { get; }
        public float RainProtection { get; set; } = 0;
        public int DefaultTrainLength { get; set; } = 5;
        public int MinTrainLength { get; set; } = 3;
        public int MaxTrainLength { get; set; } = 8;


        public FrontMiddleBackTrainBuilder(String display, String guid)
        {
            Guid = guid;
            Display = display;
        }

        public CoasterCarInstantiatorFrontMiddleBack Build(AssetManagerLoader loader)
        {
            CoasterCarInstantiatorFrontMiddleBack coasterCarInstantiator =
                ScriptableObject.CreateInstance<CoasterCarInstantiatorFrontMiddleBack>();
            coasterCarInstantiator.name = Guid;
            coasterCarInstantiator.displayName = Display;
            coasterCarInstantiator.rainProtection = RainProtection;
            coasterCarInstantiator.defaultTrainLength = DefaultTrainLength;
            coasterCarInstantiator.minTrainLength = MinTrainLength;
            coasterCarInstantiator.maxTrainLength = MaxTrainLength;

            if (RearVehicle != null)
            {
                coasterCarInstantiator.rearVehicleGO = RearVehicle.Build(loader);
            }

            if (MiddleVehicle != null)
            {
                coasterCarInstantiator.rearVehicleGO = MiddleVehicle.Build(loader);
            }

            if (FrontVehicle != null)
            {
                coasterCarInstantiator.rearVehicleGO = FrontVehicle.Build(loader);
            }

            return coasterCarInstantiator;
        }
    }
}
