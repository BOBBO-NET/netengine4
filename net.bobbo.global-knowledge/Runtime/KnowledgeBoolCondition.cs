using System.Collections.Generic;

namespace BobboNet.Knowledge
{
    [System.Serializable]
    public class KnowledgeBoolCondition : KnowledgeCondition<KnowledgeBool>
    {
        //
        //  Cases
        //

        [System.Serializable]
        public class CaseEquals : KnowledgeCaseGenericEquals<bool> { }
        [System.Serializable]
        public class CaseEqualsKnowledge : KnowledgeCaseGenericEqualsKnowledge<KnowledgeBool> { }

        //
        //  Public Properties
        //

        public new CaseEquals Equals;
        public CaseEqualsKnowledge EqualsKnowledge;

        //
        //  Initialization
        //

        public override IEnumerable<IKnowledgeCase> GetCases() => new IKnowledgeCase[]
        {
            Equals,
            EqualsKnowledge
        };
    }
}