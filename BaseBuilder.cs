using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Parkitilities
{
    public abstract class BaseBuilder<T>
    {
        protected struct Operation
        {
            public String Group;
            public String Tag;
            public int Priority;
            public Func<T, String> Handler;
        }
        private readonly List<Operation> _toApply = new List<Operation>();
        private int _idx = 0;


        private HashSet<String> _tags = new HashSet<string>();

        protected void RemoveByTag(String tag)
        {
            if (_tags.Contains(tag))
            {
                _tags.Remove(tag);
                _toApply.RemoveAll((cond) => cond.Tag.Equals(tag));
            }
        }

        protected void AddStep(String group, Func<T, String> handler)
        {
            _toApply.Add(new Operation()
            {
                Group = group,
                Priority = ++_idx,
                Handler = handler
            });
        }

        protected void AddStep(String group, String tag, Func<T, String> handler)
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

        protected IOrderedEnumerable<Operation> IterGroup(String group) {
            return _toApply.FindAll((t) => t.Group.Equals(group)).OrderBy(t => t.Priority);
        }

        protected bool ContainsTag(String tag)
        {
            return _tags.Contains(tag);
        }


        protected bool ApplyGroup(String group, T target)
        {
            foreach (var op in IterGroup(group))
            {
                Debug.Log("=== " + group + " ===");
                try
                {
                    Debug.Log(op.Handler(target));
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return true;
        }

        public abstract T Build(AssetManagerLoader loader);
    }
}
