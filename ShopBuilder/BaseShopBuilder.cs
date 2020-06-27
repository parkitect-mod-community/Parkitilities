using System;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Parkitilities.ShopBuilder
{
    public class BaseShopBuilder<TContainer, TResult, TSelf> : BaseBuilder<TContainer>
        where TResult : Shop
        where TContainer : BaseObjectContainer<TResult>
        where TSelf : class
    {

        public TSelf WalkableFlag(Block.WalkableFlagType flagType)
        {
            AddStep("WALKABLE_FLAG", (container => { container.Target.walkableFlag = flagType; }));
            return this as TSelf;
        }

        public TSelf Id(String id)
        {
            AddStep("GUID", (payload) => { payload.Go.name = id; });
            return this as TSelf;
        }

        public TSelf DisplayName(String display)
        {
            AddStep("DISPLAY", (handler) =>
            {
                handler.Target.setDisplayName(display);
            });
            return this as TSelf;
        }

        public TSelf AddBoundingBox(Bounds bound,
            BoundingVolume.Layers layers = BoundingVolume.Layers.Buildvolume)
        {
            AddStep("BOUNDING_BOX", (payload) =>
            {
                BoundingBox b = payload.Go.AddComponent<BoundingBox>();
                b.setBounds(bound);
                b.layers = layers;
            });
            return this as TSelf;
        }

        public TSelf ClearBoundingBoxes()
        {
            RemoveAllStepsByTag("BOUNDING_BOX");
            AddStep("BOUNDING_BOX", (payload) =>
            {
                foreach (var comp in payload.Go.GetComponents<BoundingBox>())
                {
                    Object.Destroy(comp);
                }
            });
            return this as TSelf;
        }


    }
}
