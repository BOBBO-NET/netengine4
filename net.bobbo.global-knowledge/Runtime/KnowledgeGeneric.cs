using UnityEngine;

namespace BobboNet.Knowledge
{
    public abstract class KnowledgeGeneric<T> : ScriptableObject, IKnowledge<T>
    {
        /// <summary>
        /// The default state of this knowledge.
        /// </summary>
        public T DefaultValue;

        /// <summary>
        /// The current state of this knowledge, at runtime.
        /// </summary>
        private T runtimeValue;

        //
        //  Interface Methods
        //

        public T GetDefaultValue() => DefaultValue;

        public T GetCurrentValue() => runtimeValue;

        public void SetCurrentValue(T newValue) => runtimeValue = newValue;

        public void Reset() => runtimeValue = DefaultValue;
    }
}