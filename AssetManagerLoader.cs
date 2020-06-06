using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Parkitilities
{
    public class AssetManagerLoader
    {
        private List<SerializedMonoBehaviour> _behaviours = new List<SerializedMonoBehaviour>();
        private GameObject _hider = new GameObject("Hider");

        public void HideGo(GameObject go)
        {
            Object.DontDestroyOnLoad(go);
            go.transform.SetParent(_hider.transform);
        }

        public void RegisterObject(SerializedMonoBehaviour behaviour)
        {
            ScriptableSingleton<AssetManager>.Instance.registerObject(behaviour);
            _behaviours.Add(behaviour);
        }

        public void Unload()
        {
            foreach (var behaviour in _behaviours)
            {
                ScriptableSingleton<AssetManager>.Instance.unregisterObject(behaviour);
            }
            Object.Destroy(_hider);
        }
    }
}
