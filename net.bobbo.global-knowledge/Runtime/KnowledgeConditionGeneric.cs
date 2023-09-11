using UnityEngine;

namespace BobboNet.Knowledge
{
    [System.Serializable]
    public class KnowledgeConditionGeneric
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
        //  Properties
        //

        public KnowledgeType Type;

        public KnowledgeFloatCondition conditionKnowledgeFloat;
        public KnowledgeIntCondition conditionKnowledgeInt;
        public KnowledgeBoolCondition conditionKnowledgeBool;

        //
        //  Public Methods
        //

        public IKnowledgeCondition Get()
        {
            switch (Type)
            {
                case KnowledgeType.KnowledgeFloat:
                    return conditionKnowledgeFloat;
                case KnowledgeType.KnowledgeInt:
                    return conditionKnowledgeInt;
                case KnowledgeType.KnowledgeBool:
                    return conditionKnowledgeBool;

                default:
                    return null;
            }
        }
    }
}