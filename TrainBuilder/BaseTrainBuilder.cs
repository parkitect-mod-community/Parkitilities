using System;

namespace Parkitilities
{

    public class TrainContainer<T>
    {
        public TrainContainer(AssetManagerLoader loader, T target)
        {
            Loader = loader;
            Target = target;
        }

        public AssetManagerLoader Loader { get;}
        public T Target { get;}
    }

    public abstract class BaseTrainBuilder<TResult, TSelf> : BaseBuilder<TrainContainer<TResult>>
        where TSelf : class
        where TResult : CoasterCarInstantiator
    {
        public TSelf Id(String uid)
        {
            AddStep( "GUID", container => { container.Target.name = uid; });
            return this as TSelf;
        }

        public TSelf DisplayName(String display)
        {
            AddStep( "DISPLAY",
                container => { container.Target.displayName = display; });
            return this as TSelf;
        }

        public TSelf FrontVehicle<TVehicle>(TVehicle vehicle)
            where TVehicle : Vehicle
        {
            AddStep( "FRONT_VEHICLE",
                (container) => { container.Target.frontVehicleGO = vehicle; });
            return this as TSelf;
        }

        public TSelf FrontVehicle<TVehicle>(IBuildable<TVehicle> vehicleBuilder, AssetManagerLoader loader)
            where TVehicle : Vehicle
        {
            AddStep("FRONT_VEHICLE",
                (container) =>
                {
                    TVehicle vehicle = vehicleBuilder.Build(loader);
                    container.Target.frontVehicleGO = vehicle;
                });
            return this as TSelf;
        }
        public TSelf MaxTrainLength(int length)
        {
            AddStep("MAX_TRAIN_LENGTH", (container) =>
            {
                container.Target.maxTrainLength = length;
            });
            return this as TSelf;
        }

        public TSelf MinTrainLength(int length)
        {
            AddStep( "MIN_TRAIN_LENGTH", (container) =>
            {
                container.Target.maxTrainLength = length;
            });
            return this as TSelf;
        }


        public TSelf DefaultTrainLength(int length)
        {
            AddStep("DEFAULT_TRAIN_LENGTH", (container) =>
            {
                container.Target.defaultTrainLength = length;
            });
            return this as TSelf;
        }

        public TSelf RainProtection(float value)
        {
            AddStep("RAIN_PROTECTION", (container) =>
            {
                container.Target.rainProtection = value;
            });
            return this as TSelf;
        }


        public TSelf MiddleVehicle<TVehicle>(TVehicle vehicle)
            where TVehicle : Vehicle
        {
            AddStep( "MIDDLE_VEHICLE",
                container =>
                {
                    container.Target.vehicleGO = vehicle;
                });
            return this as TSelf;
        }


        public TSelf MiddleVehicle<TVehicle>(IBuildable<TVehicle> vehicleBuilder, AssetManagerLoader loader)
            where TVehicle : Vehicle
        {
            AddStep( "MIDDLE_VEHICLE",
                (container) =>
                {
                    TVehicle vehicle = vehicleBuilder.Build(loader);
                    container.Target.vehicleGO = vehicle;
                });
            return this as TSelf;
        }

    }
}
