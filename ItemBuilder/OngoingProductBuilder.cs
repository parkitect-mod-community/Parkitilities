using UnityEngine;

namespace Parkitilities.ShopBuilder
{
    public class OngoingProductBuilder<TResult>: BaseProductBuilder<TResult, OngoingProductBuilder<TResult>>, IBuildable<TResult>
        where TResult : OngoingEffectProduct
    {
        private readonly GameObject _go;

        public OngoingProductBuilder(GameObject go)
        {
            _go = go;
        }

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
