using UnityEngine;
using UnityEngine.Events;

namespace BobboNet.Knowledge
{
    /// <summary>
    /// A single unit of game knowledge. This is some data that exists outside of a single scene.
    /// </summary>
    public interface IKnowledge
    {
        /// <returns>The default value of this bit of Knowledge.</returns>
        object GetDefaultValue();

        /// <returns>The current runtime value of this bit of Knowledge.</returns>
        object GetCurrentValue();

        /// <summary>
        /// Set the current runtime value for this bit of Knowledge.
        /// This will invoke the onValueChanged event.
        /// </summary>
        /// <param name="newValue">The new runtime value.</param>
        void SetCurrentValue(object newValue);

        /// <summary>
        /// Reset this bit of Knowledge to it's starting state.
        /// This will return the current runtime value to it's default value.
        /// This will invoke the onValueChanged event.
        /// </summary>
        void Reset();

        /// <summary>
        /// When the runtime value of this knowledge is changed, the given method will be invoked.
        /// </summary>
        /// <param name="onValueChanged">The action to invoke.</param>
        void AddOnValueChangedListener(UnityAction<object> onValueChanged);

        /// <summary>
        /// Stop invoking the given method when the runtime value of this knowledge is changed.
        /// </summary>
        /// <param name="onValueChanged">The action to stop invoking.</param>
        void RemoveOnValueChangedListener(UnityAction<object> onValueChanged);
    }
}