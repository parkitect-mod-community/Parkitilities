using System;
using UnityEngine;

namespace Parkitilities
{
    public class VehicleContainer<T> : BaseVehicleContainer<T>
    {

    }

    public class VehicleBuilder<TResult>: BaseVehicleBuilder<VehicleContainer<TResult>,TResult,VehicleBuilder<TResult>>, IBuildable<TResult> where TResult: Vehicle
    {
        private GameObject _go;

        public VehicleBuilder(GameObject go) {
            _go = go;
        }


        public TResult Build(AssetManagerLoader loader)
        {
            GameObject go = UnityEngine.Object.Instantiate(_go);
            if (!go.TryGetComponent<TResult>(out var vehicle)) // existing Decos are not evaluated. Assumed to be configured correctly
            {
                vehicle = go.AddComponent<TResult>();
                if (!ContainsTag("GUID"))
                    throw new Exception("Guid is never set");
            }

            VehicleContainer<TResult> dc = new VehicleContainer<TResult>()
            {
                Go = go,
                Vehicle = vehicle
            };

            ApplyGroup(DecoBuilderLiterals.SETUP_GROUP, dc);
            ApplyGroup(DecoBuilderLiterals.CONFIGURATION_GROUP, dc);
            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
            }
            return vehicle;
        }
    }
}
