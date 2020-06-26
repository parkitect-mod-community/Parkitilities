using System;
using UnityEngine;
using Object = System.Object;

namespace Parkitilities.ShopBuilder
{
    public class ProductShopBuilder<TResult> :
        BaseShopBuilder<BaseObjectContainer<TResult>, TResult, ProductShopBuilder<TResult>>
        , IBuildable<TResult>
        where TResult : ProductShop
    {
        private readonly GameObject _go;


        public ProductShopBuilder(GameObject go)
        {
            _go = go;
        }

        public ProductShopBuilder<TResult> ClearProducts()
        {
            RemoveAllStepsByTag("PRODUCT");
            AddStep("PRODUCT", (payload) => { payload.Target.products = new Product[] { }; });
            return this;
        }

        public ProductShopBuilder<TResult> AddProduct(AssetManagerLoader loader, IBuildable<Product> product)
        {
            AddStep("PRODUCT", (payload) =>
            {
                Array.Resize(ref payload.Target.products, payload.Target.products.Length + 1);
                payload.Target.products[payload.Target.products.Length - 1] = product.Build(loader);
            });
            return this;
        }

        public ProductShopBuilder<TResult> AddProduct(AssetManagerLoader loader, Product product)
        {
            AddStep("PRODUCT", (payload) =>
            {
                Array.Resize(ref payload.Target.products, payload.Target.products.Length + 1);
                payload.Target.products[payload.Target.products.Length - 1] = product;
            });
            return this;
        }

        public TResult Build(AssetManagerLoader loader)
        {
            GameObject go = UnityEngine.Object.Instantiate(_go);

            TResult shop = go.GetComponent<TResult>();
            if (shop == null)
            {
                shop = go.AddComponent<TResult>();
                if (!ContainsTag("GUID"))
                    throw new Exception("GUID is never set");
            }

            Apply(new BaseObjectContainer<TResult>(loader, shop, go));
            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
            }

            // register shop
            loader.RegisterObject(shop);
            return shop;
        }
    }
}
