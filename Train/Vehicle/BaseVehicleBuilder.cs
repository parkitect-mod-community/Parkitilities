using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Parkitilities
{

    public static class BaseVehicleBuilderLiterals
    {
        public const String SETUP_GROUP = "SETUP";
        public const String CONFIGURATION_GROUP = "CONFIGURATION";

    }

    public class BaseVehicleContainer<T>
    {
        public T Vehicle;
        public GameObject Go;
    }

    public abstract class BaseVehicleBuilder<TContainer, TResult, TSelf> : BaseBuilder<TContainer>, IComponentUtilities<TSelf>, IRecolorable<TSelf>
        where TSelf : class
        where TResult : Vehicle
        where TContainer : BaseVehicleContainer<TResult>
    {

        public TSelf Id(String id)
        {
            AddOrReplaceByTag(BaseVehicleBuilderLiterals.CONFIGURATION_GROUP,
                "GUID",
                (payload) =>
                {
                    payload.Go.name = id;
                });
            return this as TSelf;
        }


        public TSelf RotationalControllerStartClosed(String transform, Vector3 openAngle, int order = 0)
        {
            AddStep(BaseVehicleBuilderLiterals.SETUP_GROUP,"VEHICLE_CONSTRAINT", (payload) =>
            {
                RestraintRotationController controller = payload.Go.AddComponent<RestraintRotationController>();
                controller.transformName = transform;
                controller.openAngles = openAngle;
                controller.startClosed = true;
                controller.order = order;
            });
            return this as TSelf;
        }

        public TSelf RotationalControllerStartOpen(String transform, Vector3 closeAngle, int order = 0)
        {
            AddStep(BaseVehicleBuilderLiterals.SETUP_GROUP,"VEHICLE_CONSTRAINT", (payload) =>
            {
                RestraintRotationController controller = payload.Go.AddComponent<RestraintRotationController>();
                controller.transformName = transform;
                controller.closedAngles = closeAngle;
                controller.startClosed = false;
                controller.order = order;
            });
            return this as TSelf;
        }


        public TSelf TranslationControllerStartClosed(String transform,  Vector3 closeTranslation, int order = 0) {
            AddStep(BaseVehicleBuilderLiterals.SETUP_GROUP,"VEHICLE_CONSTRAINT", (payload) =>
            {
                RestraintTranslationController controller = payload.Go.AddComponent<RestraintTranslationController>();
                controller.transformName = transform;
                controller.closedTranslation = closeTranslation;
                controller.order = order;
            });
            return this as TSelf;
        }

        public TSelf CustomRestraintController<TRestraint>(String transform, Action<TRestraint> configure = null,
            int order = 0) where TRestraint : RestraintController
        {
            AddStep(BaseVehicleBuilderLiterals.SETUP_GROUP, "VEHICLE_CONSTRAINT", (payload) =>
            {
                TRestraint controller = payload.Go.AddComponent<TRestraint>();
                controller.transformName = transform;
                controller.order = order;
                configure?.Invoke(controller);
            });
            return this as TSelf;
        }

        /**
         * <summary>remove all steps for Restraint</summary>
         */
        public TSelf ClearAllRestraint
        {
            get
            {
                AddOrReplaceByTag(BaseVehicleBuilderLiterals.SETUP_GROUP, "VEHICLE_CONSTRAINT", (payload) =>
                {
                    foreach (var controller in payload.Go.GetComponents<RestraintController>())
                    {
                        Object.Destroy(controller);
                    }
                });
                return this as TSelf;
            }
        }

        public TSelf BackOffset(float offset)
        {
            AddOrReplaceByTag(BaseVehicleBuilderLiterals.CONFIGURATION_GROUP,"BACK_OFFSET",
                (payload) =>
                {
                    payload.Vehicle.offsetBack = offset;
                });

            return this as TSelf;
        }

        public TSelf FrontOffset(float offset)
        {
            AddOrReplaceByTag(BaseVehicleBuilderLiterals.CONFIGURATION_GROUP,"FRONT_OFFSET",
                (payload) =>
                {
                    payload.Vehicle.offsetFront = offset;
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
            return this as TSelf;
        }


        public TSelf DisableCustomColors
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
                return this as TSelf;
            }
        }



        public TSelf FindAndAttachComponent<TTarget>(String beginWith, String tag) where TTarget : Component
        {
            AddStep(DecoBuilderLiterals.SETUP_GROUP, tag, (payload) =>
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

        public TSelf FindAndAttachComponent<TTarget>(String beginWith) where TTarget : Component
        {
            return FindAndAttachComponent<TTarget>(beginWith, FindAttachComponentTag(beginWith));
        }



    }
}
