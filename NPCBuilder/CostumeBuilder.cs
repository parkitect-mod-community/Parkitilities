using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace Parkitilities.NPCBuilder
{
    public class CostumeBuilder : BaseBuilder<BaseContainer<EmployeeCostume>>, IBuildable<EmployeeCostume>,
        IRecolorable<CostumeBuilder>
    {

        public CostumeBuilder Id(String id)
        {
            AddStep("GUID", (handler) => { handler.Target.name = id; });
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

        public CostumeBuilder CostumeSprite(String name, Sprite sprite, float width, float height)
        {
            AddStep("COSTUME_SPRITE", (handler) =>
            {
                // set texture
                var spriteAsset = ScriptableObject.CreateInstance<TMP_SpriteAsset>();
                spriteAsset.spriteInfoList = new List<TMP_Sprite>();
                spriteAsset.spriteInfoList.Add(new TMP_Sprite()
                {
                    name = "ui_icon_entertainer_" + name,
                    unicode = 0,
                    scale = 1.0f,
                    sprite = sprite,
                    height = height,
                    width = width,
                    pivot = new Vector2(0, 0),
                    x = 0,
                    y = 0,
                    yOffset = height,
                    xAdvance = width
                });
                spriteAsset.spriteSheet = sprite.texture;
                ShaderUtilities.GetShaderPropertyIDs();
                Material material = new Material(Shader.Find("TextMeshPro/Sprite"));
                material.SetTexture(ShaderUtilities.ID_MainTex, spriteAsset.spriteSheet);
                material.hideFlags = HideFlags.HideInHierarchy;
                spriteAsset.material = material;
                spriteAsset.hashCode = TMP_TextUtilities.GetSimpleHashCode("ui_icon_entertainer_panda");
                typeof(TMP_SpriteAsset).GetField("m_FaceInfo",
                        BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(spriteAsset, FontEngine.GetFaceInfo());
                spriteAsset.UpdateLookupTables();
                MaterialReferenceManager.AddSpriteAsset(spriteAsset);

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
