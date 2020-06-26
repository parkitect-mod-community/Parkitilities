namespace Parkitilities.ShopBuilder
{
    public class OngoingProductBuilder<TResult>: BaseProductBuilder<TResult, OngoingProductBuilder<TResult>>, IBuildable<TResult>
        where TResult : OngoingEffectProduct
    {

        public OngoingProductBuilder<TResult> Duration(int duration)
        {
            AddStep("DURATION", (handler) => { handler.Target.duration = duration; });
            return this;
        }
        public OngoingProductBuilder<TResult> RemoveFromInventoryWhenDepleted(bool state)
        {
            AddStep("REMOVE_WHEN_DEPLETED", (handler) => { handler.Target.removeFromInventoryWhenDepleted = state; });
            return this;
        }

        public OngoingProductBuilder<TResult> DestroyWhenDepleted(bool state)
        {
            AddStep("DESTROY_WHEN_DEPLETED", (handler) => { handler.Target.destroyWhenDepleted = state; });
            return this;
        }


        public TResult Build(AssetManagerLoader loader)
        {
            throw new System.NotImplementedException();
        }
    }
}
