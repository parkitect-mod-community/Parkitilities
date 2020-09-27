using UnityEngine;

namespace Parkitilities.ShopBuilder
{
    public class BalloonProductBuilder<TResult>: BaseProductBuilder<TResult, BalloonProductBuilder<TResult>>, IBuildable<TResult>
        where TResult : Balloon
    {
        private readonly GameObject _go;

        public BalloonProductBuilder(GameObject go)
        {
            _go = go;
        }

        public BalloonProductBuilder<TResult> Duration(int duration)
        {
            AddStep("DURATION", (handler) => { handler.Target.duration = duration; });
            return this;
        }
        public BalloonProductBuilder<TResult> RemoveFromInventoryWhenDepleted(bool state)
        {
            AddStep("REMOVE_WHEN_DEPLETED", (handler) => { handler.Target.removeFromInventoryWhenDepleted = state; });
            return this;
        }

        public BalloonProductBuilder<TResult> DestroyWhenDepleted(bool state)
        {
            AddStep("DESTROY_WHEN_DEPLETED", (handler) => { handler.Target.destroyWhenDepleted = state; });
            return this;
        }


        public BalloonProductBuilder<TResult> DefaultMass(float mass)
        {
            AddStep("DEFAULT_MASS", (handler) => { handler.Target.defaultMass = mass; });
            return this;
        }


        public BalloonProductBuilder<TResult> DefaultDrag(float drag)
        {
            AddStep("DEFAULT_DRAG", (handler) => { handler.Target.defaultDrag = drag; });
            return this;
        }


        public BalloonProductBuilder<TResult> DefaultAngularDrag(float angularDrag)
        {
            AddStep("DEFAULT_ANGULAR_DRAG", (handler) => { handler.Target.defaultAngularDrag = angularDrag; });
            return this;
        }


        public TResult Build(AssetManagerLoader loader)
        {
            GameObject go = Object.Instantiate(_go);
            TResult balloon = go.GetComponent<TResult>();
            if (balloon == null)
            {
                Balloon cpy = (Balloon) AssetManager.Instance.getPrefab(Prefabs.BalloonRegular);
                balloon = go.AddComponent<TResult>();
                balloon.pokeSound = cpy.pokeSound;
                balloon.popSound = cpy.popSound;
                balloon.popParticlesGO = cpy.popParticlesGO;
            }

            if (balloon.transform.childCount == 0)
            {
                GameObject stringAttachment = new GameObject("String");
                stringAttachment.transform.parent = balloon.transform;
                LineRenderer renderer = stringAttachment.AddComponent<LineRenderer>();
                renderer.positionCount = 2;
                renderer.material = new Material(Shader.Find("Sprites/Default"));
            }
            Apply(new BaseObjectContainer<TResult>(loader, balloon, go));
            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
            }

            loader.RegisterObject(balloon);
            return balloon;
        }
    }
}
