using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Parkitilities
{
    public class AssetManagerLoader
    {
        private List<SerializedMonoBehaviour> _behaviours = new List<SerializedMonoBehaviour>();
        private List<ReferenceableScriptableObject> _scriptable = new List<ReferenceableScriptableObject>();
        private GameObject _hider = new GameObject("Hider");

        public void HideGo(GameObject go)
        {
            Object.DontDestroyOnLoad(go);
            go.transform.SetParent(_hider.transform);
        }

        public void RegisterObject(SerializedMonoBehaviour behaviour)
        {
            behaviour.dontSerialize = true;
            behaviour.isPreview = true;
            HideGo(behaviour.gameObject);
            ScriptableSingleton<AssetManager>.Instance.registerObject(behaviour);
            _behaviours.Add(behaviour);
            Object.DontDestroyOnLoad(behaviour.gameObject);
        }

        public void RegisterObject(ReferenceableScriptableObject scriptable)
        {
            ScriptableSingleton<AssetManager>.Instance.registerObject(scriptable);
            _scriptable.Add(scriptable);
        }

        public void Unload()
        {
            foreach (var behaviour in _behaviours)
            {
                ScriptableSingleton<AssetManager>.Instance.unregisterObject(behaviour);
            }

            foreach (var scriptable in _scriptable)
            {
                ScriptableSingleton<AssetManager>.Instance.unregisterObject(scriptable);
            }

            Object.Destroy(_hider);
        }
    }
}
