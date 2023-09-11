using System.Collections.Generic;

namespace BobboNet.Knowledge
{
    public abstract class KnowledgeCaseGenericEquals<T> : IKnowledgeCase
    {
        public T Value;

        public string GetName() => "Equals";
        public bool IsCase(IKnowledge knowledge) => IComparer<T>.Equals(knowledge.GetCurrentValue(), Value);
    }
}