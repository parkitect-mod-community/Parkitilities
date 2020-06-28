using System;
using System.Collections.Generic;
using UnityEngine;

namespace Parkitilities.ShopBuilder
{
    public class IngredientBuilder<TSelf> : IBuildable<Ingredient>
    {
        private readonly TSelf _from;
        private Ingredient _ingredient = new Ingredient();
        List<ConsumableEffect> _effects = new List<ConsumableEffect>();
        private String _displayName;
        private float _cost;
        private bool _tweakable;
        private float _amount;
        private String _id;
        private Texture _texture;

        public IngredientBuilder(TSelf from)
        {
            _from = from;
        }

        public IngredientBuilder<TSelf> Id(String id)
        {
            _id = id;
            return this;
        }

        public IngredientBuilder<TSelf> Effect(ConsumableEffect.AffectedStat stat, float amount)
        {
            _effects.Add(new ConsumableEffect
            {
                affectedStat = stat,
                amount = amount
            });
            return this;
        }

        public IngredientBuilder<TSelf> DisplayName(String name)
        {
            _displayName = name;
            return this;
        }

        public IngredientBuilder<TSelf>  Tweakable(bool state)
        {
            _tweakable = state;
            return this;
        }

        public IngredientBuilder<TSelf> DefaultAmount(float amount)
        {
            _amount = amount;
            return this;
        }

        public IngredientBuilder<TSelf> Texture(Texture texture)
        {
            _texture = texture;
            return this;
        }

        public IngredientBuilder<TSelf> Cost(float cost)
        {
            _cost = cost;
            return this;
        }

        public TSelf End()
        {
            return _from;
        }


        public Ingredient Build(AssetManagerLoader loader)
        {
            Ingredient result = new Ingredient();
            var resource = ScriptableObject.CreateInstance<Resource>();
            resource.name = _id;
            resource.effects = _effects.ToArray();
            resource.setDisplayName(_displayName);
            resource.setCosts(_cost);
            resource.resourceTexture = _texture;

            result.resource = resource;
            result.tweakable = _tweakable;
            result.defaultAmount = _amount;

            loader.RegisterObject(resource);

            return result;
        }
    }

    public class BaseProductBuilder<TResult,TSelf> : BaseItemBuilder<BaseObjectContainer<TResult>, TResult, TSelf>
        where TResult : Product
        where TSelf : class
    {

        public TSelf DefaultPrice(float price)
        {
            AddStep("DEFAULT_PRICE", handler =>
            {
                handler.Target.defaultPrice = price;
            });
            return this as TSelf;
        }

        public TSelf ClearIngredient(AssetManagerLoader loader)
        {
            RemoveAllStepsByTag("INGREDIENT");
            AddStep("INGREDIENT", handler =>
            {
                handler.Target.ingredients = new Ingredient[] { };
            });
            return this as TSelf;
        }

        public IngredientBuilder<TSelf> AddIngredient(AssetManagerLoader loader)
        {
            IngredientBuilder<TSelf> builder = new IngredientBuilder<TSelf>(this as TSelf);
            AddStep("INGREDIENT", handler =>
            {
                if (handler.Target.ingredients == null)
                {
                    handler.Target.ingredients = new[] {builder.Build(loader)};
                }
                else
                {
                    Array.Resize(ref handler.Target.ingredients, handler.Target.ingredients.Length + 1);
                    handler.Target.ingredients[handler.Target.ingredients.Length - 1] = builder.Build(loader);
                }
            });
            return builder;
        }

    }
}
