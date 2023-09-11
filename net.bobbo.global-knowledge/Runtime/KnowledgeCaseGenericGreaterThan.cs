using System;

namespace BobboNet.Knowledge
{
    public abstract class KnowledgeCaseGenericGreaterThan<T> : IKnowledgeCase where T : IComparable
    {
        public T Value;

        public string GetName() => "GreaterThan";
        public bool IsCase(IKnowledge knowledge) => ((T)knowledge.GetCurrentValue()).CompareTo(Value) > 0;
    }
}