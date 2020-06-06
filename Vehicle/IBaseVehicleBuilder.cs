using UnityEngine;

namespace Parkitilities
{
    public interface IBaseVehicleBuilder<T> : IBaseBuilder<T> where T : Vehicle
    {
    }
}
