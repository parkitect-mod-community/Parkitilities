using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Parkitilities
{
    public class BaseDecoBuilder<TContainer, TResult, TSelf> : BaseBuilder<TContainer>,
        IBoundedBoxes<TSelf>,
        IComponentUtilities<TSelf>,
        ICategory<TSelf>,
        IRecolorable<TSelf>
        where TSelf : class
        where TContainer : BaseObjectContainer<TResult>
        where TResult : Deco
    {

        public class LightControllerBuilder<TSelf>
        {
            private readonly TSelf _from;

            public bool CustomColors { get; private set; }
            public int CustomColorSlot { get; private set; }

            public LightControllerBuilder(TSelf from)
            {
                _from = from;
            }

            public LightControllerBuilder<TSelf> Slot(int slot)
            {
                CustomColorSlot = slot;
                CustomColors = true;
                return this;
            }

            public TSelf End()
            {
                return _from;
            }
        }

        public TSelf Resizable(float min, float max)
        {
            AddStep("RESIZABLE", (payload) =>
            {

                CustomSize customSize = payload.Go.GetComponent<CustomSize>();
                if (customSize == null)
                {
                    customSize = payload.Go.AddComponent<CustomSize>();
                }

                customSize.minSize = min;
                customSize.maxSize = max;
            });
            return this as TSelf;
        }

        public TSelf DisableResizable()
        {
            RemoveAllStepsByTag("RESIZABLE");
            AddStep("RESIZABLE", (payload) =>
            {
                foreach (var component in payload.Go.GetComponents<CustomSize>())
                {
                    Object.Destroy(component);
                }
            });
            return this as TSelf;
        }

        public TSelf Id(String id)
        {
            AddStep("GUID", (payload) => { payload.Go.name = id; });
            return this as TSelf;
        }

        public TSelf SeeThrough(bool state)
        {
            AddStep("SEE_THROUGH",
                payload => { payload.Target.canSeeThrough = state; });
            return this as TSelf;
        }

        public TSelf BlockRain(bool state)
        {
            AddStep("BLOCK_RAIN",
                (payload) => { payload.Target.canBlockRain = state; });

            return this as TSelf;
        }

        public TSelf HeightChangeDelta(float delta)
        {
            AddStep("HEIGHT_CHANGE_DELTA", (payload) => { payload.Target.heightChangeDelta = delta; });
            return this as TSelf;
        }

        public TSelf Category(String group, String subGroup = "")
        {
            AddStep("CATEGORY", (payload) =>
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
            return this as TSelf;
        }

        public TSelf DisplayName(String name)
        {
            AddStep("DISPLAY",
                (payload) => { payload.Target.setDisplayName(name); });
            return this as TSelf;
        }

        public TSelf Price(float price, bool canRefunded)
        {
            AddStep("PRICE", payload =>
            {
                payload.Target.price = price;
                payload.Target.canBeRefunded = canRefunded;
            });
            return this as TSelf;
        }

        public TSelf AddBoundingBox(Bounds bound, BoundingVolume.Layers layers = BoundingVolume.Layers.Buildvolume)
        {
            AddStep("BOUNDING_BOX", (payload) =>
            {
                BoundingBox b = payload.Go.AddComponent<BoundingBox>();
                b.setBounds(bound);
                b.layers = layers;
            });
            return this as TSelf;
        }

        public TSelf BuildLayerMask(LayerMask mask)
        {
            AddStep("LAYER_MASK", (payload) =>
            {
                payload.Target.buildOnLayerMask = mask;
            });
            return this as TSelf;
        }

        public TSelf ClearBoundingBoxes()
        {
            RemoveAllStepsByTag("BOUNDING_BOX");
            AddStep("BOUNDING_BOX", (payload) =>
            {
                foreach (var comp in payload.Go.GetComponents<BoundingBox>())
                {
                    Object.Destroy(comp);
                }
            });
            return this as TSelf;
        }

        public TSelf FindAndAttachComponent<TTarget>(string beginWith) where TTarget : Component
        {
            AddStep((payload) =>
            {
                List<Transform> transforms = new List<Transform>();
                Utility.recursiveFindTransformsStartingWith(beginWith, payload.Go.transform, transforms);
                foreach (var transform in transforms)
                {
                    transform.gameObject.AddComponent<TTarget>();
                }

            });
            return this as TSelf;
        }

        public LightControllerBuilder<TSelf> LightsOnAtNight()
        {
            LightControllerBuilder<TSelf> builder = new LightControllerBuilder<TSelf>(this as TSelf);
            AddStep("LIGHTS_ON_NIGHT", (payload) =>
            {
                LightController lightController = payload.Go.GetComponent<LightController>();
                if (lightController == null)
                {
                    lightController = payload.Go.AddComponent<LightController>();
                }

                lightController.customColorSlot = builder.CustomColorSlot;
                lightController.useCustomColors = builder.CustomColors;
            });
            return builder;
        }

        public TSelf DisableLightsOnAtNight()
        {
            RemoveAllStepsByTag("LIGHTS_ON_NIGHT");
            RemoveAllStepsByTag("NIGHT_SLOT");
            AddStep("LIGHTS_ON_NIGHT", (payload) =>
            {
                foreach (var controller in payload.Go.GetComponents<LightController>())
                {
                    Object.Destroy(controller);
                }
            });
            return this as TSelf;
        }

        public TSelf CustomColor(Color c1)
        {
            return CustomColor(new[] {c1});
        }

        public TSelf CustomColor(Color c1, Color c2)
        {
            return CustomColor(new[] {c1, c2});
        }

        public TSelf CustomColor(Color c1, Color c2, Color c3)
        {
            return CustomColor(new[] {c1, c2, c3});
        }

        public TSelf CustomColor(Color c1, Color c2, Color c3, Color c4)
        {
            return CustomColor(new[] {c1, c2, c3, c4});
        }

        public TSelf CustomColor(Color[] colors)
        {
            if (colors.Length == 0)
                return this as TSelf;

            AddStep("CUSTOM_COLOR", (payload) =>
            {
                CustomColors customColors = payload.Go.GetComponent<CustomColors>();
                if (customColors == null)
                {
                    customColors = payload.Go.AddComponent<CustomColors>();
                }
                customColors.setColors(colors);
            });
            return this as TSelf;
        }


        public TSelf DisableCustomColors()
        {
            RemoveAllStepsByTag("CUSTOM_COLOR");
            AddStep("CUSTOM_COLOR",
                (payload) =>
                {
                    foreach (var comp in payload.Go.GetComponents<CustomColors>())
                    {
                        Object.Destroy(comp);
                    }
                });
            return this as TSelf;
        }

        public TSelf OnGrid(bool state)
        {
            AddStep("ON_GRID",
                (payload) => { payload.Target.buildOnGrid = state; });
            return this as TSelf;
        }


        public TSelf SnapGridToCenter(bool state)
        {
            AddStep("SNAP_GRID_CENTER",
                (payload) => { payload.Target.defaultSnapToGridCenter = state; });
            return this as TSelf;
        }

        public TSelf GridSubdivisions(float divisions)
        {
            AddStep("GRID_SUBDIVISION",
                (payload) => { payload.Target.defaultGridSubdivision = divisions; });
            return this as TSelf;
        }
    }
}
