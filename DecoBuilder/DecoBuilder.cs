
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Parkitilities
{

    public static class DecoBuilderLiterals
    {
        public const String SetupGroup = "SETUP";
        public const String ConfigurationGroup = "CONFIGURATION";
    }

    public class DecoBuilder<TResult> : BaseBuilder<BaseObjectContainer<TResult>>, IBuildable<TResult>,
        IBoundedBoxes<DecoBuilder<TResult>>, IComponentUtilities<DecoBuilder<TResult>>,
        ICategory<DecoBuilder<TResult>>, IRecolorable<DecoBuilder<TResult>> where TResult : Deco
    {

        private readonly GameObject _go = null;

        public DecoBuilder(GameObject go)
        {
            _go = go;
            FindAndAttachComponent<OnlyActiveInBuildMode>("BuildMode");
        }


        public DecoBuilder<TResult> Resizable(float min, float max)
        {
            AddOrReplaceByTag(DecoBuilderLiterals.ConfigurationGroup, "RESIZABLE", (payload) =>
            {
                if (!payload.Go.TryGetComponent<CustomSize>(out var component))
                {
                    component = payload.Go.AddComponent<CustomSize>();
                }

                component.minSize = min;
                component.maxSize = max;
            });
            return this;
        }

        public DecoBuilder<TResult> ClearResizable()
        {
            AddOrReplaceByTag(DecoBuilderLiterals.ConfigurationGroup, "RESIZABLE", (payload) =>
            {
                foreach (var component in payload.Go.GetComponents<CustomSize>())
                {
                    Object.Destroy(component);
                }
            });
            return this;
        }

        public DecoBuilder<TResult> Id(String id)
        {
            AddOrReplaceByTag(DecoBuilderLiterals.ConfigurationGroup, "GUID", (payload) => { payload.Go.name = id; });
            return this;
        }


        public DecoBuilder<TResult> SeeThrough(bool state)
        {
                AddOrReplaceByTag(DecoBuilderLiterals.ConfigurationGroup, "SEE_THROUGH",
                    (payload) => { payload.Target.canSeeThrough = state; });

            return this;
        }


        public DecoBuilder<TResult> BlockRain(bool state)
        {
            AddOrReplaceByTag(DecoBuilderLiterals.ConfigurationGroup, "BLOCK_RAIN",
                (payload) => { payload.Target.canBlockRain = state; });

            return this;
        }

        public DecoBuilder<TResult> HeightChangeDelta(float delta)
        {
            AddOrReplaceByTag(DecoBuilderLiterals.ConfigurationGroup, "HEIGHT_CHANGE_DELTA", (payload) =>
            {
                payload.Target.heightChangeDelta = delta;
            });
            return this;
        }

        public DecoBuilder<TResult> Category(String group, String subGroup = "")
        {
            AddOrReplaceByTag(DecoBuilderLiterals.ConfigurationGroup, "CATEGORY", (payload) =>
            {
                if (String.IsNullOrEmpty(subGroup))
                {
                    payload.Target.categoryTag = group;
                }
                else
                {
                    payload.Target.categoryTag = group + "/" + subGroup;
                }
            });
            return this;
        }

        public DecoBuilder<TResult> DisplayName(String name)
        {
            AddOrReplaceByTag(DecoBuilderLiterals.ConfigurationGroup, "DISPLAY",
                (payload) => { payload.Target.setDisplayName(name); });
            return this;
        }

        public DecoBuilder<TResult> EnableLightsOnAtNight()
        {
            AddOrReplaceByTag(DecoBuilderLiterals.SetupGroup, "LIGHTS_ON_NIGHT", (payload) =>
            {
                if (payload.Go.GetComponent<LightController>() == null)
                {
                    payload.Go.AddComponent<LightController>();
                }
            });
            return this;
        }

        public DecoBuilder<TResult> DisableLightsOnAtNight()
        {
            AddOrReplaceByTag(DecoBuilderLiterals.SetupGroup, "LIGHTS_ON_NIGHT", (payload) =>
            {
                foreach (var controller in payload.Go.GetComponents<LightController>())
                {
                    Object.Destroy(controller);
                }
            });
            return this;

        }

        public DecoBuilder<TResult> NightColorSlot(int slot)
        {
            AddOrReplaceByTag(DecoBuilderLiterals.ConfigurationGroup, "NIGHT_SLOT", (payload) =>
            {
                LightController controller = payload.Go.GetComponent<LightController>();
                if (controller != null)
                {
                    controller.useCustomColors = true;
                    controller.customColorSlot = slot;
                }
            });
            return this;
        }


        public DecoBuilder<TResult> Price(float price)
        {
            AddOrReplaceByTag(DecoBuilderLiterals.ConfigurationGroup, "PRICE",
                (payload) => { payload.Target.price = price; });
            return this;
        }


        public DecoBuilder<TResult> CustomColor(Color c1)
        {
            return CustomColor(new[] {c1});
        }

        public DecoBuilder<TResult> CustomColor(Color c1, Color c2)
        {
            return CustomColor(new[] {c1, c2});
        }

        public DecoBuilder<TResult> CustomColor(Color c1, Color c2, Color c3)
        {
            return CustomColor(new[] {c1, c2, c3});
        }

        public DecoBuilder<TResult> CustomColor(Color c1, Color c2, Color c3, Color c4)
        {
            return CustomColor(new[] {c1, c2, c3, c4});
        }

        public DecoBuilder<TResult> CustomColor(Color[] colors)
        {
            AddOrReplaceByTag(DecoBuilderLiterals.SetupGroup, "SETUP_CUSTOM_COLOR", (payload) =>
            {
                if (payload.Go.GetComponent<CustomColors>() == null)
                {
                    payload.Go.AddComponent<CustomColors>();
                }
            });

            AddOrReplaceByTag(DecoBuilderLiterals.ConfigurationGroup, "CUSTOM_COLOR", (payload) =>
            {
                CustomColors customColors = payload.Go.GetComponent<CustomColors>();
                customColors.setColors(colors);
            });
            return this;
        }


        public DecoBuilder<TResult> DisableCustomColors()
        {
            AddOrReplaceByTag(DecoBuilderLiterals.SetupGroup, "SETUP_CUSTOM_COLOR",
                (payload) =>
                {
                    foreach (var comp in payload.Go.GetComponents<CustomColors>())
                    {
                        Object.Destroy(comp);
                    }
                });
            RemoveByTag("CUSTOM_COLOR");
            return this;
        }


        public DecoBuilder<TResult> SnapGridToCenter(bool state)
        {
            AddOrReplaceByTag(DecoBuilderLiterals.ConfigurationGroup, "SNAP_GRID_CENTER",
                (payload) => { payload.Target.defaultSnapToGridCenter = state; });
            return this;
        }

        public DecoBuilder<TResult> GridSubdivisions(float divisions)
        {
            AddOrReplaceByTag(DecoBuilderLiterals.ConfigurationGroup, "GRID_SUBDIVISION",
                (payload) => { payload.Target.defaultGridSubdivision = divisions; });
            return this;
        }

        public DecoBuilder<TResult> AddBoundingBox(Bounds bound,
            BoundingVolume.Layers layers = BoundingVolume.Layers.Buildvolume)
        {
            AddStep(DecoBuilderLiterals.ConfigurationGroup, "BOUNDING_BOX", (payload) =>
            {
                BoundingBox b = payload.Go.AddComponent<BoundingBox>();
                b.setBounds(bound);
                b.layers = layers;
            });
            return this;
        }

        public DecoBuilder<TResult> ClearBoundingBoxes()
        {
            AddOrReplaceByTag(DecoBuilderLiterals.SetupGroup, "BOUNDING_BOX", (payload) =>
            {
                foreach (var comp in payload.Go.GetComponents<BoundingBox>())
                {
                    Object.Destroy(comp);
                }
            });
            return this;
        }

        public DecoBuilder<TResult> FindAndAttachComponent<TTarget>(String beginWith) where TTarget : Component
        {
            AddStep(DecoBuilderLiterals.SetupGroup, (payload) =>
            {
                List<Transform> transforms = new List<Transform>();
                Utility.recursiveFindTransformsStartingWith(beginWith, payload.Go.transform, transforms);
                foreach (var transform in transforms)
                {
                    transform.gameObject.AddComponent<TTarget>();
                }

            });
            return this;
        }

        public TResult Build(AssetManagerLoader loader)
        {
            GameObject go = UnityEngine.Object.Instantiate(_go);
            // existing Decos are not evaluated. Assumed to be configured correctly
            if (!go.TryGetComponent<TResult>(out var deco))
            {
                deco = go.AddComponent<TResult>();
                if (!ContainsTag("GUID"))
                    throw new Exception("Guid is never set");
            }

            BaseObjectContainer<TResult> dc = new BaseObjectContainer<TResult>()
            {
                Target = deco,
                Go = go
            };

            ApplyGroup(DecoBuilderLiterals.SetupGroup, dc);
            ApplyGroup(DecoBuilderLiterals.ConfigurationGroup, dc);
            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
            }


            return deco;
        }
    }
}
