using System;
using UnityEngine;

namespace Parkitilities.PathAttachmentBuilder
{
    public class TrashBinBuilder<TResult>: BasePathAttachment<BaseObjectContainer<TResult>, TResult, PathAttachmentBuilder<TResult>>, IBuildable<TResult>
        where TResult : TrashBin
    {

        private readonly GameObject _go;

        public TrashBinBuilder(GameObject go)
        {
            _go = go;
        }

        public TrashBinBuilder<TResult> Volume(float volume)
        {
            this.AddOrReplaceByTag(BasePathAttachmentLiteral.ConfigurationGroup, "TRASH_BIN_VOLUME", (container) =>
            {
                container.Target.volume = volume;
            });
            return this;
        }

        public TResult Build(AssetManagerLoader loader)
        {
            GameObject go = UnityEngine.Object.Instantiate(_go);
            if (!go.TryGetComponent<TResult>(out var trashBin))
            {
                trashBin = go.AddComponent<TResult>();
                if (!ContainsTag("GUID"))
                    throw new Exception("Guid is never set");
            }
            BaseObjectContainer<TResult> dc = new BaseObjectContainer<TResult>()
            {
                Target = trashBin,
                Go = go
            };

            ApplyGroup(BasePathAttachmentLiteral.SetupGroup, dc);
            ApplyGroup(BasePathAttachmentLiteral.ConfigurationGroup, dc);
            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
            }

            return trashBin;
        }
    }
}