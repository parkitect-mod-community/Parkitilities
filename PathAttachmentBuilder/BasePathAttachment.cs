using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Parkitilities.PathAttachmentBuilder
{

    public abstract class BasePathAttachment<TContainer, TResult, TSelf> : BaseBuilder<TContainer>, IComponentUtilities<TSelf>,
        IRecolorable<TSelf>, IBoundedBoxes<TSelf>, IBuildable<TResult>
        where TSelf : class
        where TContainer : BaseObjectContainer<TResult>
        where TResult : PathAttachment
    {
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
            AddStep("CUSTOM_COLOR", (payload) =>
            {
                if (!payload.Go.TryGetComponent<CustomColors>(out var component))
                {
                    component = payload.Go.AddComponent<CustomColors>();
                }

                component.setColors(colors);
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

        public TSelf FindAndAttachComponent<TTarget>(string beginWith) where TTarget : Component
        {
            AddStep( (payload) =>
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


        public TSelf AddBoundingBox(Bounds bound, BoundingVolume.Layers layers = BoundingVolume.Layers.Buildvolume)
        {
            AddStep( "BOUNDING_BOX", (payload) =>
            {
                BoundingBox b = payload.Go.AddComponent<BoundingBox>();
                b.setBounds(bound);
                b.layers = layers;
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

        public abstract TResult Build(AssetManagerLoader loader);
    }
}
