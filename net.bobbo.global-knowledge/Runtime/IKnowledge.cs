using UnityEngine;

namespace BobboNet.Knowledge
{
    public interface IKnowledge<T>
    {
        /// <returns>The default value of this bit of Knowledge.</returns>
        T GetDefaultValue();

        /// <returns>The current runtime value of this bit of Knowledge.</returns>
        T GetCurrentValue();

        /// <summary>
        /// Set the current runtime value for this bit of Knowledge.
        /// </summary>
        /// <param name="newValue">The new runtime value.</param>
        void SetCurrentValue(T newValue);

        /// <summary>
        /// Reset this bit of Knowledge to it's starting state.
        /// This will return the current runtime value to it's default value.
        /// </summary>
        void Reset();
    }
}