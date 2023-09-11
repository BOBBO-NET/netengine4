using UnityEngine;
using UnityEngine.Events;

namespace BobboNet.Knowledge
{
    public abstract class ScriptableKnowledgeObject : ScriptableObject { }

    /// <summary>
    /// A ScriptableObject based implementation of Knowledge. This holds knowledge as an asset so that it
    /// can be referenced easily in the Unity inspector. Runtime values are not saved outside of play mode.
    /// </summary>
    /// <typeparam name="T">The type of the held knowledge.</typeparam>
    public abstract class KnowledgeGeneric<T> : ScriptableKnowledgeObject, IKnowledge
    {
        private class ValueChangedUnityEvent : UnityEvent<T> { }

        /// <summary>
        /// The default state of this knowledge.
        /// </summary>
        public T DefaultValue;

        /// <summary>
        /// The current state of this knowledge, at runtime.
        /// </summary>
        private T runtimeValue;

        /// <summary>
        /// An event that will invoke every time the runtime value is changed.
        /// </summary>
        private ValueChangedUnityEvent onValueChangedEvent = new ValueChangedUnityEvent();

        //
        //  Unity Methods
        //

        /// <summary>
        /// From docs: This function is called when the object is loaded.
        /// Specifically, there are 3 cases in which a ScriptableObject receives an OnEnable() message from Unity:
        /// 1 - Immediately after the ScriptableObject's Awake() (before other callbacks on this or other objects)
        /// 2 - When the Unity Editor reloads IF in a scene that has a MonoBehaviour referencing that ScriptableObject asset (right after OnDisable())
        /// 3 - When entering play mode IF in a scene that has a MonoBehaviour referencing that ScriptableObject asset (right after OnDisable())
        /// (thanks to https://forum.unity.com/threads/scriptableobject-behaviour-discussion-how-scriptable-objects-work.541212/)
        /// </summary>
        public void OnEnable() => Reset();

        //
        //  Interface Methods
        //

        /// <returns>The default value of this bit of Knowledge.</returns>
        public T GetDefaultValue() => DefaultValue;
        object IKnowledge.GetDefaultValue() => this.GetDefaultValue();

        /// <returns>The current runtime value of this bit of Knowledge.</returns>
        public T GetCurrentValue() => runtimeValue;
        object IKnowledge.GetCurrentValue() => this.GetCurrentValue();

        /// <summary>
        /// Set the current runtime value for this bit of Knowledge.
        /// This will invoke the onValueChanged event.
        /// </summary>
        /// <param name="newValue">The new runtime value.</param>
        public void SetCurrentValue(T newValue)
        {
            runtimeValue = newValue;
            onValueChangedEvent.Invoke(newValue);
        }
        void IKnowledge.SetCurrentValue(object newValue) => this.SetCurrentValue((T)newValue);

        public void Reset() => SetCurrentValue(GetDefaultValue());

        /// <summary>
        /// When the runtime value of this knowledge is changed, the given method will be invoked.
        /// </summary>
        /// <param name="onValueChanged">The action to invoke.</param>
        public void AddOnValueChangedListener(UnityAction<T> onValueChanged) => onValueChangedEvent.AddListener(onValueChanged);
        void IKnowledge.AddOnValueChangedListener(UnityAction<object> onValueChanged) => this.AddOnValueChangedListener(onValueChanged as UnityAction<T>);

        /// <summary>
        /// Stop invoking the given method when the runtime value of this knowledge is changed.
        /// </summary>
        /// <param name="onValueChanged">The action to stop invoking.</param>
        public void RemoveOnValueChangedListener(UnityAction<T> onValueChanged) => onValueChangedEvent.RemoveListener(onValueChanged);
        void IKnowledge.RemoveOnValueChangedListener(UnityAction<object> onValueChanged) => this.RemoveOnValueChangedListener(onValueChanged as UnityAction<T>);
    }
}