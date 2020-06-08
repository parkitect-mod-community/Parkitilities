using UnityEngine;

namespace Parkitilities
{
    public class BaseObjectContainer<T>
    {
        public T Target { get; set; }
        public GameObject Go { get; set; }
    }
}
