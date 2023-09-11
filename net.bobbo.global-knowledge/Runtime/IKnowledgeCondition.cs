using System.Collections.Generic;

namespace BobboNet.Knowledge
{
    public interface IKnowledgeCondition
    {
        IKnowledge GetTargetKnowledge();
        IEnumerable<IKnowledgeCase> GetCases();
    }
}