using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Parkitilities.PathAttachmentBuilder
{
    public static class BasePathAttachmentLiteral
    {
        public const String SetupGroup = "SETUP";
        public const String ConfigurationGroup = "CONFIGURATION";
    }

    public abstract class BasePathAttachment<TContainer, TResult, TSelf> : BaseBuilder<TContainer>, IComponentUtilities<TSelf>,
        IRecolorable<TSelf>, IBoundedBoxes<TSelf>
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
            AddOrReplaceByTag(BasePathAttachmentLiteral.SetupGroup, "SETUP_CUSTOM_COLOR", (payload) =>
            {
                if (payload.Go.GetComponent<CustomColors>() == null)
                {
                    payload.Go.AddComponent<CustomColors>();
                }
            });

            AddOrReplaceByTag(BasePathAttachmentLiteral.ConfigurationGroup, "CUSTOM_COLOR", (payload) =>
            {
                CustomColors customColors = payload.Go.GetComponent<CustomColors>();
                customColors.setColors(colors);
            });
            return this as TSelf;
        }

        public TSelf DisableCustomColors()
        {
            AddOrReplaceByTag(BasePathAttachmentLiteral.SetupGroup, "SETUP_CUSTOM_COLOR",
                (payload) =>
                {
                    foreach (var comp in payload.Go.GetComponents<CustomColors>())
                    {
                        Object.Destroy(comp);
                    }
                });
            RemoveByTag("CUSTOM_COLOR");
            return this as TSelf;
        }

        public TSelf FindAndAttachComponent<TTarget>(string beginWith) where TTarget : Component
        {
            AddStep(BasePathAttachmentLiteral.SetupGroup, (payload) =>
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
            AddStep(BasePathAttachmentLiteral.SetupGroup, "BOUNDING_BOX", (payload) =>
            {
                BoundingBox b = payload.Go.AddComponent<BoundingBox>();
                b.setBounds(bound);
                b.layers = layers;
            });
            return this as TSelf;
        }

        public TSelf ClearBoundingBoxes()
        {
            AddOrReplaceByTag(BasePathAttachmentLiteral.SetupGroup, "BOUNDING_BOX", (payload) =>
            {
                foreach (var comp in payload.Go.GetComponents<BoundingBox>())
                {
                    Object.Destroy(comp);
                }
            });
            return this as TSelf;
        }
    }
}
