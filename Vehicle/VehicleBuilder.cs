using UnityEngine;

namespace Parkitilities
{
    public class VehicleBuilder<T>: IRecolorable<VehicleBuilder<T>>, IBaseVehicleBuilder<T> where T: Vehicle
    {
        private Color[] _colors = { };
        public float OffsetBack { get; set; }
        public float OffsetFront { get; set; }
        public GameObject Go { get; set; }

        public VehicleBuilder()
        {

        }

        public T Build(AssetManagerLoader loader)
        {
            T vehicle = Go.AddComponent<T>();
            vehicle.offsetBack = OffsetBack;
            vehicle.offsetFront = OffsetFront;
            return vehicle;
        }

        public VehicleBuilder<T> BackOffset(float offset)
        {
            OffsetBack = offset;
            return this;
        }

        public VehicleBuilder<T> CustomColor(Color c1)
        {
            _colors = new[] {c1};
            return this;
        }

        public VehicleBuilder<T> CustomColor(Color c1, Color c2)
        {
            _colors = new[] {c1, c2};
            return this;
        }

        public VehicleBuilder<T> CustomColor(Color c1, Color c2, Color c3)
        {
            _colors = new[] {c1, c2, c3};
            return this;
        }

        public VehicleBuilder<T> CustomColor(Color c1, Color c2, Color c3, Color c4)
        {
            _colors = new[] {c1, c2, c3, c4};
            return this;
        }

        public VehicleBuilder<T> CustomColor(Color[] colors)
        {
            int count = colors.Length > 4 ? 4 : colors.Length;
            _colors = new Color[count];
            for (var x = 0; x < count; x++)
            {
                _colors[x] = colors[x];
            }

            return this;
        }
    }
}
