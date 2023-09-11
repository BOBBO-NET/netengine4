using System;
using System.Collections.Generic;
using UnityEngine;

namespace BobboNet.Knowledge
{
    [System.Serializable]
    public abstract class KnowledgeCondition<KnowledgeType> : IKnowledgeCondition where KnowledgeType : IKnowledge
    {
        public KnowledgeType TargetKnowledge;

        public int TargetCase;

        //
        //  Interface Methods
        //

        public IKnowledge GetTargetKnowledge() => TargetKnowledge;
        public abstract IEnumerable<IKnowledgeCase> GetCases();
    }
}