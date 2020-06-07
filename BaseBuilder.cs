using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Parkitilities
{
    public static class BaseBuilderLiterals
    {

        public const String FIND_ATTACH_COMPONENT = "FIND_ATTACH_COMPONENT";
    }

    public abstract class BaseBuilder<TPayload>
    {
        protected struct Operation
        {
            public String Group;
            public String Tag;
            public int Priority;
            public Action<TPayload> Handler;
        }

        public static String FindAttachComponentTag(String beginWith)
        {
            return BaseBuilderLiterals.FIND_ATTACH_COMPONENT + "_" + beginWith;
        }

        private readonly List<Operation> _toApply = new List<Operation>();
        private int _idx = 0;

        private readonly HashSet<String> _tags = new HashSet<string>();

        public void RemoveByTag(String tag)
        {
            if (_tags.Contains(tag))
            {
                _tags.Remove(tag);
                _toApply.RemoveAll((cond) => cond.Tag.Equals(tag));
            }
        }

        public bool ContainsTag(String tag)
        {
            return _tags.Contains(tag);
        }

        protected void AddOrReplaceByTag(String group, String tag, Action<TPayload> handler)
        {
            if (ContainsTag(tag))
            {
                RemoveByTag(tag);
            }
            AddStep(group, handler);
        }

        protected void AddStep(String group, Action<TPayload> handler)
        {
            _toApply.Add(new Operation()
            {
                Group = group,
                Priority = ++_idx,
                Handler = handler
            });
        }

        protected void AddStep(String group, String tag, Action<TPayload> handler)
        {
            _tags.Add(tag);
            _toApply.Add(new Operation()
            {
                Group = group,
                Tag = tag,
                Priority = ++_idx,
                Handler = handler
            });
        }

        protected IOrderedEnumerable<Operation> IterGroup(String group)
        {
            return _toApply.FindAll((t) => t.Group.Equals(group)).OrderBy(t => t.Priority);
        }

        protected bool ApplyGroup(String group, TPayload target)
        {
            Debug.Log("=== " + group + " ===");
            foreach (var op in IterGroup(group))
            {
                op.Handler(target);
            }

            return true;
        }
    }
}
