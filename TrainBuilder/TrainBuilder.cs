namespace Parkitilities
{
    public class TrainBuilder<TResult> : BaseTrainBuilder<TResult, TrainBuilder<TResult>>, IBuildable<TResult>
        where TResult : CoasterCarInstantiator
    {
        public TResult Build(AssetManagerLoader loader)
        {
            throw new System.NotImplementedException();
        }
    }
}
