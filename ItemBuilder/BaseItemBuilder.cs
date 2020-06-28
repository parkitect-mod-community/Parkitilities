using System;
using System.Reflection;
using UnityEngine;

namespace Parkitilities.ShopBuilder
{
    public class BaseItemBuilder<TContainer, TResult, TSelf> : BaseBuilder<TContainer>
        where TResult : Item
        where TContainer : BaseObjectContainer<TResult>
        where TSelf : class
    {

        public TSelf HandSide(Hand.Side side) {
            AddStep("GUID", handler =>
            {
                handler.Target.handSide = side;
            });
            return this as TSelf;
        }


        public TSelf Id(string id) {
            AddStep("GUID", handler =>
            {
                handler.Target.name = id;
            });
            return this as TSelf;
        }


        public TSelf DisplayName(String display)
        {
            AddStep("DISPLAY", handler =>
            {
                typeof(Item).GetField("displayName", BindingFlags.NonPublic
                                                     | BindingFlags.Instance)
                    ?.SetValue(handler.Target, display);
            });
            return this as TSelf;
        }

        public TSelf TwoHanded(bool state)
        {
            AddStep("TWO_HANDED", handler =>
            {
                handler.Target.isTwoHanded = state;
            });
            return this as TSelf;
        }

        public TSelf InterestingToLookAt( bool state){
            AddStep("INTERESTING_TO_LOOK_AT", handler =>
            {
                handler.Target.interestingToLookAt = state;
            });
            return this as TSelf;
        }

    }
}
