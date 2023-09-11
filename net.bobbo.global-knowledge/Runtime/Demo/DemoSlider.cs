using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

namespace BobboNet.Knowledge.Demo
{
    [RequireComponent(typeof(Slider))]
    public class DemoSlider : MonoBehaviour
    {
        public enum Type
        {
            Float,
            Int
        }


        public Type type;

        [ShowIf("type", Type.Float)]
        [Required]
        public KnowledgeFloat knowledgeAsFloat;

        [ShowIf("type", Type.Int)]
        [Required]
        public KnowledgeInt knowledgeAsInt;

        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();

            switch (type)
            {
                case Type.Float:
                    slider.value = knowledgeAsFloat.GetCurrentValue();
                    knowledgeAsFloat.AddOnValueChangedListener(OnKnowledgeFloatChanged);
                    break;
                case Type.Int:
                    slider.value = knowledgeAsInt.GetCurrentValue();
                    knowledgeAsInt.AddOnValueChangedListener(OnKnowledgeIntChanged);
                    break;
            }

            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float newValue)
        {
            switch (type)
            {
                case Type.Float:
                    if (knowledgeAsFloat.GetCurrentValue() == newValue) return;
                    knowledgeAsFloat.SetCurrentValue(newValue);
                    break;
                case Type.Int:
                    if (knowledgeAsInt.GetCurrentValue() == Mathf.RoundToInt(newValue)) return;
                    knowledgeAsInt.SetCurrentValue(Mathf.RoundToInt(newValue));
                    break;
            }
        }

        private void OnKnowledgeFloatChanged(float newValue)
        {
            if (slider.value == newValue) return;
            slider.value = newValue;
        }

        private void OnKnowledgeIntChanged(int newValue) => OnKnowledgeFloatChanged(newValue);
    }
}

