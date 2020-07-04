using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Parkitilities.ShopBuilder
{
    public static class TrashType
    {
        public static Trash ChipBagTrash
        {
            get { return AssetManager.Instance.getPrefab<Trash>("ChipBagTrash"); }
        }

        public static Trash PopCanTrash
        {
            get { return AssetManager.Instance.getPrefab<Trash>("PopCanTrash"); }
        }

        public static Trash MeatLegTrash
        {
            get { return AssetManager.Instance.getPrefab<Trash>("MeatLegTrash"); }
        }

        public static Trash BubbleTeaTrash
        {
            get { return AssetManager.Instance.getPrefab<Trash>("BubbleTeaTrash"); }
        }
    }

    public class TrashBuilder<TSelf, TResult> : BaseItemBuilder<BaseObjectContainer<TResult>, TResult, TrashBuilder<TSelf,TResult>>,
        IBuildable<Trash> where TResult : Trash
    {
        private readonly TSelf _from;
        private readonly GameObject _go;


        public TrashBuilder(TSelf from, GameObject go)
        {
            _go = go;
            _from = from;
        }

        public TrashBuilder<TSelf, TResult> Disgust(float value)
        {
            AddStep("DISGUST", container =>
            {
                container.Target.disgustFactor = value;
            });
            return this;
        }

        public TrashBuilder<TSelf, TResult> Volume(float value)
        {
            AddStep("VOLUME", container =>
            {
                container.Target.volume = value;
            });
            return this;
        }

        public TrashBuilder<TSelf, TResult> CanWiggle(bool value)
        {
            AddStep("CAN_WIGGLE", container =>
            {
                container.Target.canWiggle = value;
            });
            return this;
        }


        public TSelf End()
        {
            return _from;
        }


        public Trash Build(AssetManagerLoader loader)
        {
            GameObject go = UnityEngine.Object.Instantiate(_go);
            TResult trash = go.GetComponent<TResult>();
            if (trash == null)
            {
                trash = go.AddComponent<TResult>();
            }
            Apply(new BaseObjectContainer<TResult>(loader, trash, go));
            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
            }

            loader.RegisterObject(trash);
            return trash;
        }
    }


    public class ConsumableProductBuilder<TResult> : BaseProductBuilder<TResult, ConsumableProductBuilder<TResult>>,
        IBuildable<TResult>
        where TResult : ConsumableProduct
    {

        private readonly GameObject _go;


        public ConsumableProductBuilder(GameObject go)
        {
            _go = go;
        }


        public TrashBuilder<ConsumableProductBuilder<TResult>, TTrashResult> Trash<TTrashResult>(GameObject trashGo,
            AssetManagerLoader loader) where TTrashResult : Trash
        {
            TrashBuilder<ConsumableProductBuilder<TResult>, TTrashResult> builder =
                new TrashBuilder<ConsumableProductBuilder<TResult>, TTrashResult>(this, trashGo);
            AddStep("TRASH", handler => { handler.Target.trash = builder.Build(loader); });
            return builder;
        }


        public ConsumableProductBuilder<TResult> ConsumeAnimation(ConsumableProduct.ConsumeAnimation animation)
        {
            AddStep("CONSUME_ANIMATION", (handler) => { handler.Target.consumeAnimation = animation; });
            return this;
        }

        public ConsumableProductBuilder<TResult> TemperaturePreference(TemperaturePreference preference)
        {
            AddStep("CONSUME_ANIMATION", (handler) => { handler.Target.temperaturePreference = preference; });
            return this;
        }

        public ConsumableProductBuilder<TResult> Portions(int portions)
        {
            AddStep("PORTIONS", (handler) => { handler.Target.portions = portions; });
            return this;
        }

        public TResult Build(AssetManagerLoader loader)
        {
            GameObject go = Object.Instantiate(_go);

            TResult product = go.GetComponent<TResult>();
            if (product == null)
            {
                product = go.AddComponent<TResult>();
            }

            Apply(new BaseObjectContainer<TResult>(loader, product, go));
            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
            }

            // register shop
            loader.RegisterObject(product);
            return product;
        }
    }
}
