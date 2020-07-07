using UnityEngine;

namespace Parkitilities
{
    public class BaseObjectContainer<T>
    {

        public BaseObjectContainer(AssetManagerLoader loader, T target, GameObject go)
        {
            Loader = loader;
            Target = target;
            Go = go;
        }


        public AssetManagerLoader Loader { get; }
        public T Target { get; }
        public GameObject Go { get; }
    }

    public class BaseContainer<T>
    {

        public BaseContainer(AssetManagerLoader loader, T target)
        {
            Loader = loader;
            Target = target;
        }

        public AssetManagerLoader Loader { get; }
        public T Target { get; }
    }
}
