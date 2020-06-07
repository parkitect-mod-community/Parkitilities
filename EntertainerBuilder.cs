using UnityEngine;

namespace Parkitilities
{
    public class EntertainerBuilder<T> : BaseBuilder<T>, IBuildable<T>
    {
        public T Build(AssetManagerLoader loader)
        {
            throw new System.NotImplementedException();
        }
    }
}
