using System;
using UnityEngine;

namespace Parkitilities
{


    public class CarBuilder<TResult> : BaseVehicleBuilder<BaseObjectContainer<TResult>, TResult, CarBuilder<TResult>>, IBuildable<TResult>
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
            if (!go.TryGetComponent<TResult>(out var vehicle))
            {
                vehicle = go.AddComponent<TResult>();
                if (!ContainsTag("GUID"))
                    throw new Exception("Guid is never set");
            }

            Apply(new BaseObjectContainer<TResult>(loader,vehicle,go));
            // ApplyGroup(DecoBuilderLiterals.SetupGroup, dc);
            // ApplyGroup(DecoBuilderLiterals.ConfigurationGroup, dc);
            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
            }

            return vehicle;
        }
    }
}
