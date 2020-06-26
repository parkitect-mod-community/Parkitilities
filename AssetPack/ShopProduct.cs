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
}
