using System;
using System.Collections.Generic;
using UnityEngine;

namespace Parkitilities
{

    public class
        VehicleBuilder<TResult> : BaseVehicleBuilder<BaseObjectContainer<TResult>, TResult, VehicleBuilder<TResult>>,
            IBuildable<TResult> where TResult : Vehicle
    {
        private GameObject _go;

        public VehicleBuilder(GameObject go)
        {
            _go = go;
        }


        public TResult Build(AssetManagerLoader loader)
        {
            GameObject go = UnityEngine.Object.Instantiate(_go);
            TResult vehicle = go.GetComponent<TResult>();
            if (vehicle == null) // existing Decos are not evaluated. Assumed to be configured correctly
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

            loader.RegisterObject(vehicle);

            return vehicle;
        }
    }
}
