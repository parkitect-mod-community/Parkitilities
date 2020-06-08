namespace Parkitilities
{
    public interface IBuildable<TTarget>
    {
        TTarget Build(AssetManagerLoader loader);
    }

    public interface IBuildable<TTarget,T1>
    {
        TTarget Build(AssetManagerLoader loader,T1 input);
    }
}
