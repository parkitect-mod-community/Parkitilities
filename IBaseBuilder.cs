using UnityEngine;

namespace Parkitilities
{
    public interface IBaseBuilder<T>
    {
        T Build(AssetManagerLoader loader);
    }
}
