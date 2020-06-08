using System;

namespace Parkitilities
{

    public class BaseTrainBuilder<TResult, TSelf> : BaseBuilder<TResult>
        where TSelf : class
        where TResult : CoasterCarInstantiator
    {
        public TSelf Id(String uid)
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "GUID", container => { container.name = uid; });
            return this as TSelf;
        }

        public TSelf DisplayName(String display)
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "DISPLAY",
                container => { container.displayName = display; });
            return this as TSelf;
        }

        public TSelf FrontVehicle<TVehicle>(TVehicle vehicle)
            where TVehicle : Vehicle
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "FRONT_VEHICLE",
                (container) => { container.frontVehicleGO = vehicle; });
            return this as TSelf;
        }

        public TSelf FrontVehicle<TVehicle>(IBuildable<TVehicle> vehicleBuilder, AssetManagerLoader loader)
            where TVehicle : Vehicle
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "FRONT_VEHICLE",
                (container) =>
                {
                    TVehicle vehicle = vehicleBuilder.Build(loader);
                    container.frontVehicleGO = vehicle;
                });
            return this as TSelf;
        }


        public TSelf MaxTrainLength(int length)
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "MAX_TRAIN_LENGTH", (container) =>
            {
                container.maxTrainLength = length;
            });
            return this as TSelf;
        }

        public TSelf MinTrainLength(int length)
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "MIN_TRAIN_LENGTH", (container) =>
            {
                container.maxTrainLength = length;
            });
            return this as TSelf;
        }


        public TSelf DefaultTrainLength(int length)
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "DEFAULT_TRAIN_LENGTH", (container) =>
            {
                container.defaultTrainLength = length;
            });
            return this as TSelf;
        }

        public TSelf RainProtection(float value)
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "RAIN_PROTECTION", (container) =>
            {
                container.rainProtection = value;
            });
            return this as TSelf;
        }


        public TSelf MiddleVehicle<TVehicle>(TVehicle vehicle, AssetManagerLoader loader)
            where TVehicle : Vehicle
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "MIDDLE_VEHICLE",
                container =>
                {
                    container.vehicleGO = vehicle;
                });
            return this as TSelf;
        }


        public TSelf MiddleVehicle<TVehicle>(IBuildable<TVehicle> vehicleBuilder, AssetManagerLoader loader)
            where TVehicle : Vehicle
        {
            AddOrReplaceByTag(TrainBuilderLiterals.SETUP_GROUP, "MIDDLE_VEHICLE",
                (container) =>
                {
                    TVehicle vehicle = vehicleBuilder.Build(loader);
                    container.vehicleGO = vehicle;
                });
            return this as TSelf;
        }
    }
}
