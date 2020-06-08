using System;

namespace Parkitilities
{
    public interface IComponentUtilities<TBase>
    {
        TBase FindAndAttachComponent<TTarget>(String beginWith) where TTarget : UnityEngine.Component;
    }
}
