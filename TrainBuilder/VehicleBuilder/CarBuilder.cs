using System;
using System.Collections.Generic;
using UnityEngine;

namespace Parkitilities
{
    public class CarBuilder<TResult> : BaseVehicleBuilder<BaseObjectContainer<TResult>, TResult, CarBuilder<TResult>>,
        IBuildable<TResult>
        where TResult : Car
    {
        private readonly GameObject _go;

        public CarBuilder(GameObject go)
        {
            _go = go;
        }


        public TResult Build(AssetManagerLoader loader)
        {
            GameObject go = UnityEngine.Object.Instantiate(_go);
            // existing Decos are not evaluated. Assumed to be configured correctly
            TResult vehicle = go.GetComponent<TResult>();
            if (vehicle == null)
            {
                vehicle = go.AddComponent<TResult>();
                if (!ContainsTag("GUID"))
                    throw new Exception("Guid is never set");
            }

            Apply(new BaseObjectContainer<TResult>(loader, vehicle, go));
            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
            }

            List<Transform> transforms = new List<Transform>();
            Utility.recursiveFindTransformsStartingWith("WheelsFront", go.transform, transforms);
            vehicle.frontAxisArray = transforms.ToArray();
            transforms.Clear();
            Utility.recursiveFindTransformsStartingWith("WheelsBack", go.transform, transforms);
            vehicle.backAxisArray = transforms.ToArray();


            loader.RegisterObject(vehicle);

            return vehicle;
        }
    }
}
