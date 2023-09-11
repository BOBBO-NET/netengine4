using UnityEngine;
using UnityEngine.Events;

namespace BobboNet.Knowledge
{
    [System.Serializable]
    public class KnowledgeAny : IKnowledge
    {
        //
        //  Types
        //

        public enum KnowledgeType
        {
            None = 0,
            KnowledgeFloat,
            KnowledgeInt,
            KnowledgeBool
        }

        //
        //  Public Properties
        //

        public KnowledgeType Type;

        public KnowledgeFloat itemKnowledgeFloat;
        public KnowledgeInt itemKnowledgeInt;
        public KnowledgeBool itemKnowledgeBool;

        //
        //  Public Methods
        //

        public IKnowledge Get()
        {
            switch (Type)
            {
                case KnowledgeType.KnowledgeFloat:
                    return itemKnowledgeFloat;
                case KnowledgeType.KnowledgeInt:
                    return itemKnowledgeInt;
                case KnowledgeType.KnowledgeBool:
                    return itemKnowledgeBool;

                default:
                    return null;
            }
        }

        //
        //  Interface Methods
        //

        public void AddOnValueChangedListener(UnityAction<object> onValueChanged) => Get().AddOnValueChangedListener(onValueChanged);
        public object GetCurrentValue() => Get().GetCurrentValue();
        public object GetDefaultValue() => Get().GetDefaultValue();
        public void RemoveOnValueChangedListener(UnityAction<object> onValueChanged) => Get().RemoveOnValueChangedListener(onValueChanged);
        public void Reset() => Get().Reset();
        public void SetCurrentValue(object newValue) => Get().SetCurrentValue(newValue);
    }
}