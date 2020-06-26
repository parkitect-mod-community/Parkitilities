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

        public TSelf DisplayName(String display)
        {
            AddStep("DISPLAY", (handler) =>
            {
                FieldInfo fieldInfo = typeof(TResult).GetField("displayName",
                    BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    fieldInfo.SetValue(handler.Target, display);
                }
            });
            return this as TSelf;
        }

        public TSelf TwoHanded(bool state)
        {
            AddStep("TWO_HANDED", (handler) =>
            {
                handler.Target.isTwoHanded = state;
            });
            return this as TSelf;
        }

        public TSelf InterestingToLookAt( bool state){
            AddStep("INTERESTING_TO_LOOK_AT", (handler) =>
            {
                handler.Target.interestingToLookAt = state;
            });
            return this as TSelf;
        }

    }
}
