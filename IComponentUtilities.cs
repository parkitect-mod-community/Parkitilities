using System;

namespace Parkitilities
{
    public interface IComponentUtilities<TBase>
    {
        TBase FindAndAttachComponent<TTarget>(String beginWith) where TTarget : UnityEngine.Component;
        /**
         * <summary>Find transform that begins with and apply TTarget</summary>
         */
        TBase FindAndAttachComponent<TTarget>(String beginWith, String tag) where TTarget : UnityEngine.Component;
    }
}
