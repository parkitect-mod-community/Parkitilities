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


    public class ConsumableProductBuilder<TResult> : BaseProductBuilder<TResult, ConsumableProductBuilder<TResult>>,
        IBuildable<TResult>
        where TResult : ConsumableProduct
    {

        private readonly GameObject _go;


        public ConsumableProductBuilder(GameObject go)
        {
            _go = go;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="trash"></param>
        /// <returns></returns>
        public ConsumableProductBuilder<TResult> Trash(Trash trash)
        {
            //TODO: ADD Trash configuration
            AddStep("TRASH", (handler) => { handler.Target.trash = trash; });
            return this;
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
