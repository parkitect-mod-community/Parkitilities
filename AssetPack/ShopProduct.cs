using System.Collections.Generic;

namespace Parkitilities.AssetPack
{
    public enum Temperature
    {
        NONE,
        COLD,
        HOT
    }

    public enum HandSide
    {
        LEFT,
        RIGHT
    }

    public enum ConsumeAnimation
    {
        GENERIC,
        DRINK_STRAW,
        LICK,
        WITH_HANDS
    }

    public enum ProductType
    {
        ON_GOING,
        CONSUMABLE,
        WEARABLE
    }

    public enum Seasonal
    {
        WINTER,
        SPRING,
        SUMMER,
        AUTUMN,
        NONE
    }

    public enum Body
    {
        HEAD,
        FACE,
        BACK
    }

    public enum EffectTypes
    {
        HUNGER,
        THIRST,
        HAPPINESS,
        TIREDNESS,
        SUGARBOOST
    }


    public class ShopProduct
    {
        public List<ShopIngredient> Ingredients = new List<ShopIngredient>();
        public ProductType ProductType { get; set; }
        public string Guid { get; set; }

        //base
        public string Name { get; set; }
        public float Price { get; set; }

        public bool IsTwoHanded { get; set; }
        public bool IsInterestingToLookAt { get; set; }
        public HandSide HandSide { get; set; }

        //ongoing
        public int Duration { get; set; }
        public bool RemoveWhenDepleted { get; set; }
        public bool DestroyWhenDepleted { get; set; }

        //wearable
        public Body BodyLocation { get; set; }
        public Seasonal SeasonalPreference { get; set; }
        public Temperature TemperaturePreference { get; set; }
        public bool HideOnRide { get; set; }
        public bool HideHair { get; set; }

        //consumable
        public ConsumeAnimation ConsumeAnimation { get; set; }
        public Temperature Temprature { get; set; }
        public int Portions { get; set; }
    }

    public class ShopIngredient
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
        public bool Tweakable { get; set; }
        public List<Effect> Effects = new List<Effect>();
    }


    public class Effect
    {
        public EffectTypes Type { get; set; }
        public float Amount { get; set; }
    }

    public static class ProductShopUtility
    {
        public static Hand.Side ConvertToSide(HandSide side)
        {
            if (side == HandSide.LEFT) return Hand.Side.LEFT;
            return Hand.Side.RIGHT;
        }

        public static TemperaturePreference ConvertTemperaturePreference(Temperature temperature)
        {
            switch (temperature)
            {
                case Temperature.HOT: return TemperaturePreference.HOT;
                case Temperature.COLD: return TemperaturePreference.COLD;
            }

            return TemperaturePreference.NONE;
        }

        public static WearableProduct.SeasonalPreference ConvertSeasonalPreference(Seasonal seasonal)
        {
            switch (seasonal)
            {
                case Seasonal.AUTUMN: return WearableProduct.SeasonalPreference.AUTUMN;
                case Seasonal.SPRING: return WearableProduct.SeasonalPreference.SPRING;
                case Seasonal.SUMMER: return WearableProduct.SeasonalPreference.SUMMER;
                case Seasonal.WINTER: return WearableProduct.SeasonalPreference.WINTER;
            }

            return WearableProduct.SeasonalPreference.NONE;
        }

        public static WearableProduct.BodyLocation ConvertBodyLocation(Body body)
        {
            switch (body)
            {
                case Body.BACK: return WearableProduct.BodyLocation.BACK;
                case Body.FACE: return WearableProduct.BodyLocation.FACE;
                case Body.HEAD: return WearableProduct.BodyLocation.HEAD;
            }

            return WearableProduct.BodyLocation.HEAD;
        }

        public static ConsumableEffect.AffectedStat ConvertEffectType(EffectTypes type)
        {
            switch (type)
            {
                case EffectTypes.HUNGER: return ConsumableEffect.AffectedStat.HUNGER;
                case EffectTypes.THIRST: return ConsumableEffect.AffectedStat.THIRST;
                case EffectTypes.HAPPINESS: return ConsumableEffect.AffectedStat.HAPPINESS;
                case EffectTypes.TIREDNESS: return ConsumableEffect.AffectedStat.TIREDNESS;
                case EffectTypes.SUGARBOOST: return ConsumableEffect.AffectedStat.SUGARBOOST;
            }

            return ConsumableEffect.AffectedStat.HUNGER;
        }

        public static ConsumableProduct.ConsumeAnimation ConvertConsumeAnimation(ConsumeAnimation animation)
        {
            switch (animation)
            {
                case ConsumeAnimation.LICK: return ConsumableProduct.ConsumeAnimation.LICK;
                case ConsumeAnimation.GENERIC: return ConsumableProduct.ConsumeAnimation.GENERIC;
                case ConsumeAnimation.WITH_HANDS: return ConsumableProduct.ConsumeAnimation.WITH_HANDS;
                case ConsumeAnimation.DRINK_STRAW: return ConsumableProduct.ConsumeAnimation.DRINK_STRAW;
            }

            return ConsumableProduct.ConsumeAnimation.GENERIC;
        }
    }
}
