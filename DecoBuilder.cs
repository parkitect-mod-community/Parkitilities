
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Parkitilities
{
    public class DecoBuilder<T> : BaseBuilder<T>, IRecolorable<DecoBuilder<T>> where T : Deco
    {
        public static readonly String SETUP = "SETUP";
        public static readonly String CONFIGURATION = "CONFIGURATION";

        public static readonly String SET_GUID = "SET_GUID";
        private GameObject _go = null;

        public DecoBuilder(GameObject go)
        {
            _go = go;
        }

        public DecoBuilder<T> Id(String id)
        {
            AddStep(CONFIGURATION,SET_GUID, (deco) =>
            {
                deco.name = id;
                return "Set Guid: " + deco.name;
            });
            return this;
        }


        public DecoBuilder<T> DisplayName(String name)
        {
            AddStep(CONFIGURATION,"SetDisplayName", (deco) =>
            {
                deco.setDisplayName(name);
                return "Set Display Name: " + name;
            });
            return this;
        }

        public DecoBuilder<T> EnableLightsOnAtNight
        {
            get
            {
                RemoveByTag("AddLightController");
                AddStep(SETUP, "AddLightController", (deco) =>
                {
                    deco.gameObject.AddComponent<LightController>();
                    return "Added LightController";
                });
                return this;
            }
        }

        public DecoBuilder<T> DisableLightsOnAtNight
        {
            get
            {
                RemoveByTag("AddLightController");
                return this;
            }
        }

        public DecoBuilder<T> NightColorSlot(int slot)
        {
            AddStep(CONFIGURATION, (deco) =>
            {
                LightController controller = deco.gameObject.GetComponent<LightController>();
                if (controller != null)
                {
                    controller.useCustomColors = true;
                    controller.customColorSlot = slot;
                    return "Enable Night Color for slot " + slot;
                }

                return "Object Does not have LightController";
            });
            return this;
        }


        public DecoBuilder<T> Price(float price)
        {
            AddStep(CONFIGURATION, (deco) =>
            {
                deco.price = price;
                return "Set Price to: " + price;
            });
            return this;
        }


        public DecoBuilder<T> CustomColor(Color c1)
        {
            return CustomColor(new[] {c1});
        }

        public DecoBuilder<T> CustomColor(Color c1, Color c2)
        {
            return CustomColor(new[] {c1, c2});
        }

        public DecoBuilder<T> CustomColor(Color c1, Color c2, Color c3)
        {
            return CustomColor(new[] {c1, c2, c3});
        }

        public DecoBuilder<T> CustomColor(Color c1, Color c2, Color c3, Color c4)
        {
            return CustomColor(new[] {c1, c2, c3, c4});
        }

        public DecoBuilder<T> CustomColor(Color[] colors)
        {

            RemoveByTag("AddCustomColors");
            AddStep(SETUP, "AddColorComponent", (deco) =>
            {
                deco.gameObject.AddComponent<CustomColors>();
                return "Add Custom Color Component";
            });

            AddStep(CONFIGURATION, "SetCustomColors", (deco) =>
            {
                CustomColors customColors = deco.GetComponent<CustomColors>();
                customColors.setColors(colors);
                return "Set Custom Colors to: " + colors;
            });
            return this;
        }

        public DecoBuilder<T> ClearBoundingBox()
        {
            RemoveByTag("AddBoundingBox");
            return this;
        }


        public DecoBuilder<T> AddBoundingBox(Bounds bound)
        {
            AddStep(SETUP, "AddBoundingBox", (deco) =>
            {
                BoundingBox b = deco.gameObject.AddComponent<BoundingBox>();
                b.setBounds(bound);
                b.layers = BoundingVolume.Layers.Buildvolume;

                return "Add Bounding Box " + bound;
            });
            return this;
        }

        public override T Build(AssetManagerLoader loader)
        {
            if (!ContainsTag(SET_GUID))
                throw new Exception("Guid is never set");


            GameObject go = UnityEngine.Object.Instantiate(_go);
            T deco = go.AddComponent<T>();
            ApplyGroup(SETUP, deco);
            ApplyGroup(CONFIGURATION, deco);
            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
            }

            List<Transform> transforms = new List<Transform>();
            Utility.recursiveFindTransformsStartingWith("BuildMode", go.transform, transforms);
            foreach (var transform in transforms)
            {
                Parkitility.OnlyActiveInBuildMode(transform.gameObject);
            }

            deco.dontSerialize = true;
            deco.isPreview = true;
            deco.isStatic = true;

            return deco;
        }
    }
}
