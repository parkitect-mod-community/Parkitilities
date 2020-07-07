using System;
using UnityEngine;

namespace Parkitilities.NPCBuilder
{
    public class CostumeBuilder : BaseBuilder<BaseContainer<EmployeeCostume>>, IBuildable<EmployeeCostume>,
        IRecolorable<CostumeBuilder>
    {

        public CostumeBuilder Id(String id)
        {
            AddStep("DISPLAY_NAME", (handler) => { handler.Target.name = id; });
            return this;
        }

        public CostumeBuilder DisplayName(String name)
        {
            AddStep("DISPLAY_NAME", (handler) => { handler.Target.costumeName = name; });
            return this;
        }

        public CostumeBuilder GuestThoughtAboutCostume(String thought)
        {
            AddStep("DISPLAY_NAME", (handler) => { handler.Target.guestThoughtAboutCostume = thought; });
            return this;
        }

        public CostumeBuilder CostumeSprite(String name, Sprite sprite)
        {
            AddStep("COSTUME_SPRITE", (handler) =>
            {
                handler.Target.costumeSpriteName = name;
                handler.Target.customeSprite = sprite;
            });
            return this;
        }

        public CostumeBuilder CustomColor(Color c1)
        {
            return CustomColor(new[] {c1});
        }

        public CostumeBuilder CustomColor(Color c1, Color c2)
        {
            return CustomColor(new[] {c1, c2});
        }

        public CostumeBuilder CustomColor(Color c1, Color c2, Color c3)
        {
            return CustomColor(new[] {c1, c2, c3});
        }

        public CostumeBuilder CustomColor(Color c1, Color c2, Color c3, Color c4)
        {
            return CustomColor(new[] {c1, c2, c3, c4});
        }

        public CostumeBuilder CustomColor(Color[] colors)
        {
            AddStep("CUSTOM_COLOR", handler => { handler.Target.customColors = colors; });
            return this;
        }

        public CostumeBuilder DisableCustomColors()
        {
            AddStep("CUSTOM_COLOR", handler => { handler.Target.customColors = new Color[] { }; });
            return this;
        }

        public CostumeBuilder BodyPartMale<TResult>(AssetManagerLoader loader, IBuildable<TResult> container)
            where TResult : BodyPartsContainer
        {
            AddStep("BODY_PART_MALE", handler => { handler.Target.bodyPartsMale = container.Build(loader); });
            return this;
        }

        public CostumeBuilder MeshAnimations(MeshAnimationCollection meshAnimations)
        {
            AddStep("MESH_ANIMATION", handler => { handler.Target.meshAnimations = meshAnimations; });
            return this;
        }

        public CostumeBuilder AnimatorController(RuntimeAnimatorController animatorController)
        {
            AddStep("MESH_ANIMATION", handler => { handler.Target.animatorController = animatorController; });
            return this;
        }


        public CostumeBuilder BodyPartMale(BodyPartsContainer bodyPartsContainer)
        {
            AddStep("BODY_PART_MALE", handler => { handler.Target.bodyPartsMale = bodyPartsContainer; });
            return this;
        }

        public CostumeBuilder BodyPartFemale<TResult>(AssetManagerLoader loader,
            IBuildable<BodyPartsContainer> container)
            where TResult : BodyPartsContainer
        {
            AddStep("BODY_PART_FEMALE", handler => { handler.Target.bodyPartsFemale = container.Build(loader); });
            return this;
        }

        public CostumeBuilder BodyPartFemale(BodyPartsContainer bodyPartsContainer)
        {
            AddStep("BODY_PART_MALE", handler => { handler.Target.bodyPartsFemale = bodyPartsContainer; });
            return this;
        }

        public EmployeeCostume Build(AssetManagerLoader loader)
        {
            var employeeCostume = ScriptableObject.CreateInstance<EmployeeCostume>();
            employeeCostume.customColors = new Color[] { };
            loader.RegisterObject(employeeCostume);
            Apply(new BaseContainer<EmployeeCostume>(loader, employeeCostume));
            return employeeCostume;
        }

        public EmployeeCostume Register(AssetManagerLoader loader, Entertainer entertainer)
        {
            Array.Resize(ref entertainer.costumes, entertainer.costumes.Length + 1);
            entertainer.costumes[entertainer.costumes.Length - 1] = Build(loader);
            return entertainer.costumes[entertainer.costumes.Length - 1];
        }

    }
}
