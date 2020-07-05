using System;
using UnityEngine;

namespace Parkitilities
{
    public class WallBuilder<TResult> : BaseDecoBuilder<BaseObjectContainer<TResult>, TResult, WallBuilder<TResult>>
        where TResult : Wall
    {

        private readonly GameObject _go;

        public WallBuilder(GameObject go)
        {
            _go = go;
            FindAndAttachComponent<OnlyActiveInBuildMode>("BuildMode");
        }

        public WallBuilder<TResult> BlockSides(Wall.BlockedSide blockedSide)
        {
            AddStep("BLOCK_SIDES",
                (payload) => { payload.Target.blockedSides = blockedSide; });
            return this;
        }

        public WallBuilder<TResult> RemoveAdjacentHandRails(bool value)
        {
            AddStep("REMOVE_HAND_RAILS",
                (payload) => { payload.Target.removeAdjacentHandrails = value; });
            return this;
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
