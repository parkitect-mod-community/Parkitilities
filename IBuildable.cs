namespace Parkitilities
{
    public interface IBuildable<TTarget>
    {
        TTarget Build(AssetManagerLoader loader);
    }

    public interface IBuildableNonNonSerializable<TTarget>
    {
        TTarget Build();
    }

    public interface IBuildable<TTarget,T1>
    {
        TTarget Build(AssetManagerLoader loader,T1 input);
    }

    public interface IBuildableNonNonSerializable<TTarget,T1>
    {
        TTarget Build(T1 input);
    }
}
