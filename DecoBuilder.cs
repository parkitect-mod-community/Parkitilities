
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Parkitilities
{
    public struct DecoContainer<T>
    {
        public T Deco;
        public GameObject Go;
    }

    public static class DecoBuilderLiterals
    {
        public const String SETUP_GROUP = "SETUP";
        public const String CONFIGURATION_GROUP = "CONFIGURATION";
    }

    public class DecoBuilder<TResult> : BaseBuilder<DecoContainer<TResult>>, IBuildable<TResult>, IComponentUtilities<DecoBuilder<TResult>>, IRecolorable<DecoBuilder<TResult>> where TResult : Deco
    {

        private readonly GameObject _go = null;
        public DecoBuilder(GameObject go)
        {
            _go = go;
        }


        public DecoBuilder<TResult> Resizable(float min, float max)
        {
            AddOrReplaceByTag(DecoBuilderLiterals.CONFIGURATION_GROUP, "RESIZABLE", (payload) =>
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

        public DecoBuilder<TResult> NotResizable {
            get
            {
                AddOrReplaceByTag(DecoBuilderLiterals.CONFIGURATION_GROUP, "RESIZABLE", (payload) =>
                {
                    if (payload.Go.TryGetComponent<CustomSize>(out var component))
                    {
                        Object.Destroy(component);
                    }
                });
                return this;
            }

        }

        public DecoBuilder<TResult> Id(String id)
        {
            AddOrReplaceByTag(DecoBuilderLiterals.CONFIGURATION_GROUP,"GUID", (payload) =>
            {
                payload.Go.name = id;
            });
            return this;
        }


        public DecoBuilder<TResult> SeeThrough {
            get
            {
                AddOrReplaceByTag(DecoBuilderLiterals.CONFIGURATION_GROUP, "SEE_THROUGH", (payload) =>
                {
                    payload.Deco.canSeeThrough = true;
                });

                return this;
            }
        }

        public DecoBuilder<TResult> CantSeeThrough {
            get
            {
                AddOrReplaceByTag(DecoBuilderLiterals.CONFIGURATION_GROUP,"SEE_THROUGH", (payload) =>
                {
                    payload.Deco.canSeeThrough = false;
                });

                return this;
            }
        }

        public DecoBuilder<TResult> BlockRain
        {
            get
            {
                AddOrReplaceByTag(DecoBuilderLiterals.CONFIGURATION_GROUP,"BLOCK_RAIN", (payload) =>
                {
                    payload.Deco.canBlockRain = true;
                });

                return this;
            }
        }

        public DecoBuilder<TResult> DisableBlockRain
        {
            get
            {
                AddOrReplaceByTag(DecoBuilderLiterals.CONFIGURATION_GROUP,"BLOCK_RAIN", (payload) =>
                {
                    payload.Deco.canBlockRain = false;
                });
                return this;
            }
        }


        public DecoBuilder<TResult> Group(String group, String subGroup = "")
        {
            AddOrReplaceByTag(DecoBuilderLiterals.CONFIGURATION_GROUP,"GROUP", (payload) =>
            {
                if (String.IsNullOrEmpty(subGroup))
                {
                    payload.Deco.categoryTag = group;
                }
                else
                {
                    payload.Deco.categoryTag = group + "/" + subGroup;
                }

            });
            return this;
        }

        public DecoBuilder<TResult> DisplayName(String name)
        {
            AddOrReplaceByTag(DecoBuilderLiterals.CONFIGURATION_GROUP, "DISPLAY",(payload) =>
            {
                payload.Deco.setDisplayName(name);
            });
            return this;
        }

        public DecoBuilder<TResult> EnableLightsOnAtNight
        {
            get
            {
                AddOrReplaceByTag(DecoBuilderLiterals.SETUP_GROUP, "LIGHTS_ON_NIGHT", (payload) =>
                {
                    if (payload.Go.GetComponent<LightController>() == null)
                    {
                        payload.Go.AddComponent<LightController>();
                    }
                });

                return this;
            }
        }

        public DecoBuilder<TResult> DisableLightsOnAtNight
        {
            get
            {
                AddOrReplaceByTag(DecoBuilderLiterals.SETUP_GROUP, "LIGHTS_ON_NIGHT", (payload) =>
                {
                    foreach (var controller in payload.Go.GetComponents<LightController>())
                    {
                        Object.Destroy(controller);
                    }
                });
                return this;
            }
        }

        public DecoBuilder<TResult> NightColorSlot(int slot)
        {
            AddOrReplaceByTag(DecoBuilderLiterals.CONFIGURATION_GROUP,"NIGHT_SLOT", (payload) =>
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
            AddOrReplaceByTag(DecoBuilderLiterals.CONFIGURATION_GROUP, "PRICE", (payload) =>
            {
                payload.Deco.price = price;
            });
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
            AddOrReplaceByTag(DecoBuilderLiterals.SETUP_GROUP, "SETUP_CUSTOM_COLOR", (payload) =>
            {
                if (payload.Go.GetComponent<CustomColors>() == null) {
                    payload.Go.AddComponent<CustomColors>();
                }
            });

            AddOrReplaceByTag(DecoBuilderLiterals.CONFIGURATION_GROUP, "CUSTOM_COLOR", (payload) =>
            {
                CustomColors customColors = payload.Go.GetComponent<CustomColors>();
                customColors.setColors(colors);
            });
            return this;
        }

        public DecoBuilder<TResult> DisableCustomColors
        {
            get
            {
                AddOrReplaceByTag(DecoBuilderLiterals.SETUP_GROUP, "SETUP_CUSTOM_COLOR",
                    (payload) =>
                    {
                        foreach (var comp in payload.Go.GetComponents<CustomColors>()) {
                            Object.Destroy(comp);
                        }
                    });
                RemoveByTag("CUSTOM_COLOR");
                return this;
            }
        }

        public DecoBuilder<TResult> AddBoundingBox(Bounds bound, BoundingVolume.Layers layers = BoundingVolume.Layers.Buildvolume)
        {
            AddStep(DecoBuilderLiterals.SETUP_GROUP, "BOUNDING_BOX", (payload) =>
            {
                BoundingBox b = payload.Go.AddComponent<BoundingBox>();
                b.setBounds(bound);
                b.layers = layers;
            });
            return this;
        }

        public DecoBuilder<TResult> ClearBoundingBoxes
        {
            get
            {
                AddOrReplaceByTag(DecoBuilderLiterals.SETUP_GROUP, "BOUNDING_BOX", (payload) =>
                {
                    foreach (var comp in payload.Go.GetComponents<BoundingBox>()) {
                        Object.Destroy(comp);
                    }
                });
                return this;
            }
        }

        public DecoBuilder<TResult> FindAndAttachComponent<TTarget>(String beginWith, String tag) where TTarget : Component
        {
            AddStep(DecoBuilderLiterals.SETUP_GROUP,tag, (payload) =>
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

        public DecoBuilder<TResult> FindAndAttachComponent<TTarget>(String beginWith) where TTarget : Component
        {
            return FindAndAttachComponent<TTarget>(beginWith, FindAttachComponentTag(beginWith));
        }

        public TResult Build(AssetManagerLoader loader)
        {
            GameObject go = UnityEngine.Object.Instantiate(_go);
            if (!go.TryGetComponent<TResult>(out var deco)) // existing Decos are not evaluated. Assumed to be configured correctly
            {
                deco = go.AddComponent<TResult>();
                if (!ContainsTag("GUID"))
                    throw new Exception("Guid is never set");
            }

            DecoContainer<TResult> dc = new DecoContainer<TResult>()
            {
                Deco = deco,
                Go = go
            };

            try
            {
                ApplyGroup(DecoBuilderLiterals.SETUP_GROUP, dc);
                ApplyGroup(DecoBuilderLiterals.CONFIGURATION_GROUP, dc);
                foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
                {
                    Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
                }

            }
            catch (Exception ex)
            {
                // ignored
            }

            return deco;
        }
    }
}
