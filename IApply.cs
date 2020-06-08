namespace Parkitilities
{
    /**
     * <summary>Apply queue of changes to an existing object without Registering</summary>
     */
    public interface IApply<TResult>
    {
        TResult Apply();
    }
}
