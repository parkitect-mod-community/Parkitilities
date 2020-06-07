using System;
using UnityEngine;

namespace Parkitilities
{

    public class CarContainer<T> : BaseVehicleContainer<T>
    {

    }

    public class CarBuilder<TResult> : BaseVehicleBuilder<CarContainer<TResult>, TResult, CarBuilder<TResult>>
        where TResult : Car
    {
        private GameObject _go;

        public CarBuilder(GameObject go)
        {
            _go = go;
        }

        public TResult Build(AssetManagerLoader loader)
        {
            GameObject go = UnityEngine.Object.Instantiate(_go);
            if (!go.TryGetComponent<TResult>(out var vehicle)
            ) // existing Decos are not evaluated. Assumed to be configured correctly
            {
                vehicle = go.AddComponent<TResult>();
                if (!ContainsTag("GUID"))
                    throw new Exception("Guid is never set");
            }

            CarContainer<TResult> dc = new CarContainer<TResult>()
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
