namespace Parkitilities
{
    public interface IBuildable<TTarget>
    {
        TTarget Build(AssetManagerLoader loader);
    }
}
