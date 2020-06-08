namespace Parkitilities.Shop
{
    public class ProductShopBuilder<TResult> : BaseBuilder<BaseObjectContainer<TResult>>, IBuildable<TResult> where TResult: ProductShop
    {
        public TResult Build(AssetManagerLoader loader)
        {
            throw new System.NotImplementedException();
        }
    }
}
