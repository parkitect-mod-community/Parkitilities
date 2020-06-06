
using System;
using System.Collections.Generic;
using Parkitect.Mods.AssetPacks;
using UnityEngine;

namespace Parkitilities
{
    public class DecoBuilder<T> : IRecolorable<DecoBuilder<T>>, IBaseBuilder<T> where T : Deco
    {
        private Color[] _colors = { };

        private bool _isLightOnAtNight;
        private int _nightColorSlots = -1;

        private List<Bounds> _bounds = new List<Bounds>();
        private float _price;
        private bool _canBeRefunded = false;
        private bool _isStatic = true;
        private String _displayName = "";
        private String _guid = null;
        private GameObject _go = null;

        private List<Func<Deco,bool>> _toApply = new List<Func<Deco,bool>>();

        public DecoBuilder(GameObject go, String guid)
        {
            _go = go;
            _guid = guid;
        }


        public DecoBuilder<T> DisplayName(String name)
        {
            _displayName = name;
            return this;
        }

        public DecoBuilder<T> AddComponent<TComponent>() where TComponent : Component
        {
            _toApply.Add(deco => {
                deco.gameObject.AddComponent<TComponent>();
                return true;
            });
            return this;
        }

        // public DecoBuilder(Asset asset)
        // {
        //
        //     if (asset.CustomColors != null)
        //     {
        //         Color[] c1 = new Color[asset.CustomColors.Count];
        //         for (var i = 0; i < asset.CustomColors.Count; i++)
        //         {
        //             c1[i] = new Color(asset.CustomColors[i].Red, asset.CustomColors[i].Green,
        //                 asset.CustomColors[i].Blue, asset.CustomColors[i].Alpha);
        //         }
        //
        //
        //         CustomColor(c1);
        //
        //         SetLightEffects(asset.LightsTurnOnAtNight, asset.LightsUseCustomColors);
        //         SetLightColorSlot(asset.LightsCustomColorSlot);
        //         SetDisplayName(asset.Name);
        //         SetPrice(asset.Price);
        //     }
        // }

        public DecoBuilder<T> EnableLightsOnAtNight
        {
            get
            {
                _isLightOnAtNight = true;
                return this;
            }
        }

        public DecoBuilder<T> DisableLightsOnAtNight
        {
            get
            {
                _isLightOnAtNight = false;
                return this;
            }
        }

        public DecoBuilder<T> NightColorSlot(int slot)
        {
            _nightColorSlots = slot;
            return this;
        }


        public DecoBuilder<T> Price(float price)
        {
            _price = price;
            return this;
        }


        public DecoBuilder<T> CustomColor(Color c1)
        {
            _colors = new[] {c1};
            return this;
        }

        public DecoBuilder<T> CustomColor(Color c1, Color c2)
        {
            _colors = new[] {c1, c2};
            return this;
        }

        public DecoBuilder<T> CustomColor(Color c1, Color c2, Color c3)
        {
            _colors = new[] {c1, c2, c3};
            return this;
        }

        public DecoBuilder<T> CustomColor(Color c1, Color c2, Color c3, Color c4)
        {
            _colors = new[] {c1, c2, c3, c4};
            return this;
        }

        public DecoBuilder<T> CustomColor(Color[] colors)
        {
            int count = colors.Length > 4 ? 4 : colors.Length;
            _colors = new Color[count];
            for (var x = 0; x < count; x++)
            {
                _colors[x] = colors[x];
            }

            return this;
        }

        public DecoBuilder<T> SetLightColorSlot(int slot)
        {
            _nightColorSlots = slot;
            return this;
        }

        public DecoBuilder<T> AddBoundingBox(Bounds bound)
        {
            _bounds.Add(bound);
            return this;
        }

        public T Build(AssetManagerLoader loader)
        {
            GameObject go = UnityEngine.Object.Instantiate(Go);

            T deco = go.AddComponent<T>();

            //add re-color
            Parkitility.ApplyRecolorable(_colors, go);

            if (_isLightOnAtNight)
            {
                LightController lightController = go.AddComponent<LightController>();
                // lightController.useCustomColors = _useNightColorSlot;
                lightController.customColorSlot = _nightColorSlots;
            }

            foreach (var box in _bounds)
            {
                BoundingBox b = go.AddComponent<BoundingBox>();
                b.setBounds(box);
                b.layers = BoundingVolume.Layers.Buildvolume;
            }

            List<Transform> transforms = new List<Transform>();
            Utility.recursiveFindTransformsStartingWith("BuildMode", go.transform, transforms);
            foreach (var transform in transforms)
            {
                Parkitility.OnlyActiveInBuildMode(transform.gameObject);
            }

            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceParkitectMaterial(componentsInChild);
            }

            deco.price = _price;
            deco.canBeRefunded = _canBeRefunded;
            deco.isStatic = _isStatic;
            deco.isPreview = true;
            deco.dontSerialize = true;
            deco.setDisplayName(_displayName);

            loader.RegisterObject(deco);
            return deco;
        }
    }
}
