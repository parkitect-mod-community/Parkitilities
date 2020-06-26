using System;
using System.Reflection;
using UnityEngine;

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

    }
}
