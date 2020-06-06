using UnityEngine;

namespace Parkitilities
{
    public class CarBuilder<T> : IRecolorable<CarBuilder<T>>, IBaseVehicleBuilder<T> where T : Car
    {
        private Color[] _colors = { };
        public GameObject Go { get; set; }

        public CarBuilder(GameObject go)
        {
            Go = go;
        }

        public CarBuilder()
        {
            throw new System.NotImplementedException();
        }

        public T Build(AssetManagerLoader loader)
        {

            // T target = go.AddComponent<T>();
            // return target;
            return null;
        }

        public CarBuilder<T> CustomColor(Color c1)
        {
            throw new System.NotImplementedException();
        }

        public CarBuilder<T> CustomColor(Color c1, Color c2)
        {
            throw new System.NotImplementedException();
        }

        public CarBuilder<T> CustomColor(Color c1, Color c2, Color c3)
        {
            throw new System.NotImplementedException();
        }

        public CarBuilder<T> CustomColor(Color c1, Color c2, Color c3, Color c4)
        {
            throw new System.NotImplementedException();
        }

        public CarBuilder<T> CustomColor(Color[] colors)
        {
            throw new System.NotImplementedException();
        }
    }
}
