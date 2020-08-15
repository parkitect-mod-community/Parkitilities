using UnityEngine;

namespace Parkitilities.ShopBuilder
{
    public class ProductBuilder<TResult> : BaseProductBuilder<TResult, ProductBuilder<TResult>>, IBuildable<TResult>
        where TResult : Product
    {
        private readonly GameObject _go;

        public ProductBuilder(GameObject go)
        {
            _go = go;
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
