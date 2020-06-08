using System;
using UnityEngine;

namespace Parkitilities.PathAttachmentBuilder
{
    public class PathAttachmentBuilder<TResult>: BasePathAttachment<BaseObjectContainer<TResult>, TResult, PathAttachmentBuilder<TResult>>, IBuildable<TResult>
        where TResult : PathAttachment
    {
        private readonly GameObject _go = null;
        public PathAttachmentBuilder(GameObject go)
        {
            _go = go;
        }

        public TResult Build(AssetManagerLoader loader)
        {
            GameObject go = UnityEngine.Object.Instantiate(_go);
            if (!go.TryGetComponent<TResult>(out var pathAttachment))
            {
                pathAttachment = go.AddComponent<TResult>();
                if (!ContainsTag("GUID"))
                    throw new Exception("Guid is never set");
            }

            BaseObjectContainer<TResult> dc = new BaseObjectContainer<TResult>()
            {
                Target = pathAttachment,
                Go = go
            };
            ApplyGroup(BasePathAttachmentLiteral.SetupGroup, dc);
            ApplyGroup(BasePathAttachmentLiteral.ConfigurationGroup, dc);
            foreach (Renderer componentsInChild in go.GetComponentsInChildren<Renderer>())
            {
                Parkitility.ReplaceWithParkitectMaterial(componentsInChild);
            }

            return pathAttachment;
        }
    }
}
