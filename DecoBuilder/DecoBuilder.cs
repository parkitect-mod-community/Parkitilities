
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Parkitilities
{

    public class LightControllerBuilder<TFrom, TResult> where TFrom: DecoBuilder<TResult> where TResult : Deco
    {
        private readonly TFrom _from;

        public LightControllerBuilder(TFrom from)
        {
            _from = from;
        }

        public LightControllerBuilder<TFrom, TResult> Slot(int slot)
        {
            _from.AddStep("NIGHT_SLOT", (payload) =>
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

        public DecoBuilder<TResult> End()
        {
            return _from;
        }
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
            return this;
        }

        public DecoBuilder<TResult> DisableResizable()
        {
            RemoveAllStepsByTag("RESIZABLE");
            AddStep("RESIZABLE", (payload) =>
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
            AddStep("GUID", (payload) => { payload.Go.name = id; });
            return this;
        }


        public DecoBuilder<TResult> SeeThrough(bool state)
        {
            AddStep("SEE_THROUGH",
                payload => { payload.Target.canSeeThrough = state; });
            return this;
        }


        public DecoBuilder<TResult> BlockRain(bool state)
        {
            AddStep("BLOCK_RAIN",
                (payload) => { payload.Target.canBlockRain = state; });

            return this;
        }

        public DecoBuilder<TResult> HeightChangeDelta(float delta)
        {
            AddStep("HEIGHT_CHANGE_DELTA", (payload) => { payload.Target.heightChangeDelta = delta; });
            return this;
        }

        public DecoBuilder<TResult> Category(String group, String subGroup = "")
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
            return this;
        }

        public DecoBuilder<TResult> DisplayName(String name)
        {
            AddStep("DISPLAY",
                (payload) => { payload.Target.setDisplayName(name); });
            return this;
        }


        public LightControllerBuilder<DecoBuilder<TResult>,TResult> LightsOnAtNight()
        {
            AddStep("LIGHTS_ON_NIGHT", (payload) =>
            {
                foreach (var controller in payload.Go.GetComponents<LightController>())
                {
                    Object.Destroy(controller);
                }
            });
            return new LightControllerBuilder<DecoBuilder<TResult>,TResult>(this);
        }

        public DecoBuilder<TResult> DisableLightsOnAtNight()
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
            return this;
        }

        public DecoBuilder<TResult> Price(float price)
        {
            AddStep("PRICE", payload => { payload.Target.price = price; });
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
            AddStep("CUSTOM_COLOR", (payload) =>
            {
                CustomColors customColors = payload.Go.GetComponent<CustomColors>();
                if (customColors == null)
                {
                    customColors = payload.Go.AddComponent<CustomColors>();
                }
                customColors.setColors(colors);
            });
            return this;
        }


        public DecoBuilder<TResult> DisableCustomColors()
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
            return this;
        }


        public DecoBuilder<TResult> SnapGridToCenter(bool state)
        {
            AddStep("SNAP_GRID_CENTER",
                (payload) => { payload.Target.defaultSnapToGridCenter = state; });
            return this;
        }

        public DecoBuilder<TResult> GridSubdivisions(float divisions)
        {
            AddStep("GRID_SUBDIVISION",
                (payload) => { payload.Target.defaultGridSubdivision = divisions; });
            return this;
        }

        public DecoBuilder<TResult> AddBoundingBox(Bounds bound,
            BoundingVolume.Layers layers = BoundingVolume.Layers.Buildvolume)
        {
            AddStep("BOUNDING_BOX", (payload) =>
            {
                BoundingBox b = payload.Go.AddComponent<BoundingBox>();
                b.setBounds(bound);
                b.layers = layers;
            });
            return this;
        }

        public DecoBuilder<TResult> ClearBoundingBoxes()
        {
            RemoveAllStepsByTag("BOUNDING_BOX");
            AddStep("BOUNDING_BOX", (payload) =>
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
            AddStep((payload) =>
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
            TResult deco = go.GetComponent<TResult>();
            if (deco == null)
            {
                deco = go.AddComponent<TResult>();
                if (!ContainsTag("GUID"))
                    throw new Exception("Guid is never set");
            }

            Apply(new BaseObjectContainer<TResult>(loader, deco, go));
            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
            }
            // register deco
            loader.RegisterObject(deco);


            return deco;
        }
    }
}
