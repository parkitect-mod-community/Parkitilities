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
            public String Tag;
            public Action<TPayload> Handler { get; set; }
        }

        public static String FindAttachComponentTag(String beginWith)
        {
            return BaseBuilderLiterals.FIND_ATTACH_COMPONENT + "_" + beginWith;
        }

        private readonly List<Operation> _actions = new List<Operation>();
        private int _idx = 0;

        private readonly HashSet<String> _tags = new HashSet<string>();

        public void RemoveAllStepsByTag(String tag)
        {
            if (_tags.Contains(tag))
            {
                _tags.Remove(tag);
                _actions.RemoveAll((cond) => cond.Tag.Equals(tag));
            }
        }

        public bool ContainsTag(String tag)
        {
            return _tags.Contains(tag);
        }


        public void AddStep(String tag, Action<TPayload> handler)
        {
            _tags.Add(tag);
            _actions.Add(new Operation()
            {
                Tag = tag,
                Handler = handler
            });
        }

        public void AddStep(Action<TPayload> handler)
        {
            _actions.Add(new Operation()
            {
                Handler = handler
            });
        }


        protected void Apply(TPayload target)
        {
            Debug.Log("-------------------------------------------------------");
            foreach (var action in _actions)
            {
                Debug.Log("Applying Action: " + action.Tag);
                action.Handler(target);
            }

            Debug.Log("-------------------------------------------------------");
        }
    }
}
