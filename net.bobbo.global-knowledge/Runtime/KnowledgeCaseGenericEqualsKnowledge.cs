using System.Collections.Generic;

namespace BobboNet.Knowledge
{
    public abstract class KnowledgeCaseGenericEqualsKnowledge<T> : IKnowledgeCase where T : IKnowledge
    {
        public T Value;

        public string GetName() => "EqualsKnowledge";
        public bool IsCase(IKnowledge knowledge) => IComparer<T>.Equals(knowledge.GetCurrentValue(), Value.GetCurrentValue());
    }
}