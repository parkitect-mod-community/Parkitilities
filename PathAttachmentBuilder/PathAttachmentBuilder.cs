using System;
using UnityEngine;

namespace Parkitilities.PathAttachmentBuilder
{
    public class PathAttachmentBuilder<TResult>: BasePathAttachment<BaseObjectContainer<TResult>, TResult, PathAttachmentBuilder<TResult>>
        where TResult : PathAttachment
    {
        private readonly GameObject _go = null;
        public PathAttachmentBuilder(GameObject go)
        {
            _go = go;
        }

        public override TResult Build(AssetManagerLoader loader)
        {
            GameObject go = UnityEngine.Object.Instantiate(_go);
            if (!go.TryGetComponent<TResult>(out var pathAttachment))
            {
                pathAttachment = go.AddComponent<TResult>();
                if (!ContainsTag("GUID"))
                    throw new Exception("Guid is never set");
            }


            Apply(new BaseObjectContainer<TResult>(loader,pathAttachment,go));
            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
            }

            return pathAttachment;
        }
    }
}
