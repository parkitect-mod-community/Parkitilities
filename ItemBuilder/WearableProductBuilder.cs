using UnityEngine;

namespace Parkitilities.ShopBuilder
{
    public class WearableProductBuilder<TResult> : BaseProductBuilder<TResult,WearableProductBuilder<TResult>>, IBuildable<TResult>
        where TResult : WearableProduct
    {
        private readonly GameObject _go;

        public WearableProductBuilder(GameObject go)
        {
            _go = go;
        }

        public WearableProductBuilder<TResult> HideHair(bool state)
        {
            AddStep("HIDE_HAIR", (handler) => { handler.Target.dontHideHair = !state; });
            return this;
        }

        public WearableProductBuilder<TResult> HideOnRide(bool state)
        {
            AddStep("HIDE_ON_RIDE", (handler) => { handler.Target.hideOnRides = state; });
            return this;
        }

        public WearableProductBuilder<TResult> TemperaturePreference(TemperaturePreference preference)
        {
            AddStep("TEMPERATURE_PREFERENCE", (handler) => { handler.Target.temperaturePreference = preference; });
            return this;
        }

        public WearableProductBuilder<TResult> BodyLocation(WearableProduct.BodyLocation location)
        {
            AddStep("BODY_LOCATION", (handler) => { handler.Target.bodyLocation = location; });
            return this;
        }

        public WearableProductBuilder<TResult> SeasonalPreference(WearableProduct.SeasonalPreference preference)
        {
            AddStep("SEASONAL_PREFERENCE", (handler) => { handler.Target.seasonalPreference = preference; });
            return this;
        }

        public TResult Build(AssetManagerLoader loader)
        {
            throw new System.NotImplementedException();
        }
    }
}
