using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Parkitilities.NPCBuilder
{

    public class BodyPartBuilder : BaseBuilder<BaseContainer<BodyPartsContainer>>, IBuildable<BodyPartsContainer>
    {

        private BodyPartsContainer _container;

        public BodyPartBuilder(BodyPartsContainer container)
        {
            _container = container;
        }

        public BodyPartBuilder()
        {

        }

        private void _appendFieldProductWearable(BodyPartsContainer container, String field, WearableProduct value)
        {
            FieldInfo info = typeof(BodyPartsContainer)
                .GetField(field, BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic);
            WearableProduct[] wearables = info.GetValue(container) as WearableProduct[];
            if (wearables == null)
            {
                info.SetValue(container, new[] {value});
            }
            else
            {
                Array.Resize(ref wearables, wearables.Length + 1);
                wearables[wearables.Length - 1] = value;
                info.SetValue(container, wearables);
            }
        }


        private void _appendFieldGo<TTarget>(BodyPartsContainer container, String field, TTarget value)
        {
            FieldInfo info = typeof(BodyPartsContainer)
                .GetField(field, BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic);
            TTarget[] gos = info.GetValue(container) as TTarget[];
            if (gos == null)
            {
                info.SetValue(container, new[] {value});
            }
            else
            {
                Array.Resize(ref gos, gos.Length + 1);
                gos[gos.Length - 1] = value;
                info.SetValue(container, gos);
            }
        }

        public BodyPartBuilder AddTorso(GameObject go)
        {
            AddStep("TORSO", (handler) => { _appendFieldGo(handler.Target, "torsos", go); });
            return this;
        }

        public BodyPartBuilder AddHead(GameObject go)
        {
            AddStep("HEAD", (handler) => { _appendFieldGo(handler.Target, "heads", go); });
            return this;
        }

        public BodyPartBuilder AddLeg(GameObject go)
        {
            AddStep("LEG", (handler) => { _appendFieldGo(handler.Target, "legs", go); });
            return this;
        }

        public BodyPartBuilder AddHairstyle(GameObject go)
        {
            AddStep("HAIRSTYLE", (handler) => { _appendFieldGo(handler.Target, "hairstyles", go); });
            return this;
        }

        public BodyPartBuilder AddAccessoryProduct<TResult>(AssetManagerLoader loader,
            IBuildable<TResult> builder)
            where TResult : WearableProduct
        {
            AddStep("ACCESSORY_PRODUCT",
                (handler) => { _appendFieldProductWearable(handler.Target, "accessories", builder.Build(loader)); });
            return this;
        }

        public BodyPartBuilder AddAccessoryProduct(WearableProduct product)
        {
            AddStep("ACCESSORY_PRODUCT",
                (handler) => { _appendFieldProductWearable(handler.Target, "accessories", product); });
            return this;
        }

        public BodyPartBuilder AddHeadProduct(WearableProduct product)
        {
            AddStep("HEAD_PRODUCT",
                (handler) => { _appendFieldProductWearable(handler.Target, "headItems", product); });
            return this;
        }


        public BodyPartBuilder AddHeadProduct<TResult>(AssetManagerLoader loader, IBuildable<TResult> builder)
            where TResult : WearableProduct
        {
            AddStep("HEAD_PRODUCT",
                (handler) => { _appendFieldProductWearable(handler.Target, "headItems", builder.Build(loader)); });
            return this;
        }

        public BodyPartBuilder AddFaceProduct(WearableProduct wearable)
        {
            AddStep("FACE_PRODUCT",
                (handler) => { _appendFieldProductWearable(handler.Target, "faceItems", wearable); });
            return this;
        }

        public BodyPartBuilder AddFaceProduct<TResult>(AssetManagerLoader loader,
            IBuildable<TResult> builder)
            where TResult : WearableProduct
        {
            AddStep("FACE_PRODUCT",
                (handler) => { _appendFieldProductWearable(handler.Target, "faceItems", builder.Build(loader)); });
            return this;
        }

        public BodyPartsContainer Build(AssetManagerLoader loader)
        {
            if (_container == null)
            {
                _container = ScriptableObject.CreateInstance<BodyPartsContainer>();

                typeof(BodyPartsContainer)
                    .GetField("torsos", BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(_container, new GameObject[]{});
                typeof(BodyPartsContainer)
                    .GetField("heads", BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(_container, new GameObject[]{});
                typeof(BodyPartsContainer)
                    .GetField("legs", BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(_container, new GameObject[]{});
                typeof(BodyPartsContainer)
                    .GetField("hairstyles", BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(_container, new GameObject[]{});

                typeof(BodyPartsContainer)
                    .GetField("accessories", BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(_container, new WearableProduct[] { });
                typeof(BodyPartsContainer)
                    .GetField("headItems", BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(_container, new WearableProduct[] { });
                typeof(BodyPartsContainer)
                    .GetField("faceItems", BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(_container, new WearableProduct[] { });

            }
            Apply(new BaseContainer<BodyPartsContainer>(loader, _container));

            Debug.Log("HEADS:" + _container.getHeadsCount());
            Debug.Log("HEADS:" + _container.getTorsosCount());
            Debug.Log("HEADS:" + _container.getHairstylesCount());


            return _container;
        }
    }
}
