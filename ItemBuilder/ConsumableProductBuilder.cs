using System;

namespace Parkitilities.ShopBuilder
{
    public class ConsumableProductBuilder<TResult> : BaseProductBuilder<TResult, ConsumableProductBuilder<TResult>>,
        IBuildable<TResult>
        where TResult : ConsumableProduct
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
            throw new System.NotImplementedException();
        }
    }
}
