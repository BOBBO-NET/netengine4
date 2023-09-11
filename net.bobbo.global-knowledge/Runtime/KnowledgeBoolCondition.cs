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

        //
        //  Public Properties
        //

        public new CaseEquals Equals;

        //
        //  Initialization
        //

        public override IEnumerable<IKnowledgeCase> GetCases() => new IKnowledgeCase[]
        {
            Equals
        };
    }
}