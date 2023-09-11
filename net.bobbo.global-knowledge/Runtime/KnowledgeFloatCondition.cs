using System.Collections.Generic;

namespace BobboNet.Knowledge
{
    [System.Serializable]
    public class KnowledgeFloatCondition : KnowledgeCondition<KnowledgeFloat>
    {
        //
        //  Cases
        //

        [System.Serializable]
        public class CaseEquals : KnowledgeCaseGenericEquals<float> { }
        [System.Serializable]
        public class CaseGreaterThan : KnowledgeCaseGenericGreaterThan<float> { }
        [System.Serializable]
        public class CaseLessThan : KnowledgeCaseGenericLessThan<float> { }
        [System.Serializable]
        public class CaseEqualsKnowledge : KnowledgeCaseGenericEqualsKnowledge<KnowledgeFloat> { }

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