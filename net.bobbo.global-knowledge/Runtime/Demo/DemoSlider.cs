using UnityEngine;
using UnityEngine.UI;

namespace BobboNet.Knowledge.Demo
{
    [RequireComponent(typeof(Slider))]
    public class DemoSlider : MonoBehaviour
    {
        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float newValue)
        {

        }
    }
}

