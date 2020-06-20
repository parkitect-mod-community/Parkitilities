using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Parkitilities
{
    public class AssetManagerLoader
    {
        private List<SerializedMonoBehaviour> _behaviours = new List<SerializedMonoBehaviour>();
        private List<ReferenceableScriptableObject> _scriptable = new List<ReferenceableScriptableObject>();
        private GameObject _hider = null;

        public void HideGo(GameObject go)
        {
            if (_hider == null)
            {
                _hider = new GameObject("Hider");
                _hider.SetActive(false);
            }

            Object.DontDestroyOnLoad(go);
            go.transform.SetParent(_hider.transform);
        }

        public void RegisterObject(SerializedMonoBehaviour behaviour)
        {
            Debug.Log("Register GameObject Object " + behaviour.name);
            behaviour.dontSerialize = true;
            behaviour.isPreview = true;
            HideGo(behaviour.gameObject);
            ScriptableSingleton<AssetManager>.Instance.registerObject(behaviour);
            _behaviours.Add(behaviour);
            Object.DontDestroyOnLoad(behaviour.gameObject);
        }

        public void RegisterObject(ReferenceableScriptableObject scriptable)
        {
            Debug.Log("Register Scriptable Object " + scriptable.name);
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
            _behaviours.Clear();
            _scriptable.Clear();

            Object.Destroy(_hider);
            _hider = null;
        }
    }
}
