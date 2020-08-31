
using System;
using UnityEngine;

namespace Parkitilities
{
    public class DecoBuilder<TResult> : BaseDecoBuilder<BaseObjectContainer<TResult>, TResult, DecoBuilder<TResult>>,
        IBuildable<TResult>
        where TResult : Deco
    {
        private readonly GameObject _go;

        public DecoBuilder(GameObject go)
        {
            _go = go;
            FindAndAttachComponent<OnlyActiveInBuildMode>("BuildMode");
        }

        public TResult Build(AssetManagerLoader loader)
        {

            GameObject go = UnityEngine.Object.Instantiate(_go);
            // existing Decos are not evaluated. Assumed to be configured correctly
            TResult deco = go.GetComponent<TResult>();
            if (deco == null)
            {
                deco = go.AddComponent<TResult>();
                if (!ContainsTag("GUID"))
                    throw new Exception("Guid is never set");
            }

            Apply(new BaseObjectContainer<TResult>(loader, deco, go));
            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
            }

            // register deco
            loader.RegisterObject(deco);
            return deco;
        }
    }
}
