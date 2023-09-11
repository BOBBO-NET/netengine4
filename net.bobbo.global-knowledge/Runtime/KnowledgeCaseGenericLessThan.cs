using System;

namespace BobboNet.Knowledge
{
    public abstract class KnowledgeCaseGenericLessThan<T> : IKnowledgeCase where T : IComparable
    {
        public T Value;

        public string GetName() => "LessThan";
        public bool IsCase(IKnowledge knowledge) => ((T)knowledge.GetCurrentValue()).CompareTo(Value) < 0;
    }
}