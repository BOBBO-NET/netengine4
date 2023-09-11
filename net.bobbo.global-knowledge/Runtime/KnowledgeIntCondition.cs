using System.Collections.Generic;

namespace BobboNet.Knowledge
{
    [System.Serializable]
    public class KnowledgeIntCondition : KnowledgeCondition<KnowledgeInt>
    {
        //
        //  Cases
        //

        [System.Serializable]
        public class CaseEquals : KnowledgeCaseGenericEquals<int> { }
        [System.Serializable]
        public class CaseGreaterThan : KnowledgeCaseGenericGreaterThan<int> { }
        [System.Serializable]
        public class CaseLessThan : KnowledgeCaseGenericLessThan<int> { }
        [System.Serializable]
        public class CaseEqualsKnowledge : KnowledgeCaseGenericEqualsKnowledge<KnowledgeInt> { }

        //
        //  Public Properties
        //

        public new CaseEquals Equals;
        public CaseGreaterThan GreaterThan;
        public CaseLessThan LessThan;
        public CaseEqualsKnowledge EqualsKnowledge;

        //
        //  Initialization
        //

        public override IEnumerable<IKnowledgeCase> GetCases() => new IKnowledgeCase[]
        {
            Equals,
            EqualsKnowledge,
            GreaterThan,
            LessThan
        };
    }
}