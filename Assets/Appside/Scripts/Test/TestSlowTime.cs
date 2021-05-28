using System.Collections.Generic;
using UnityEngine;

namespace Appside.Scripts.Test
{
    public class TestSlowTime : MonoBehaviour
    {
        [SerializeField]
        private List<float> timeScales;
        [SerializeField]
        private int currentTimeScaleIndex;

        private void Start()
        {
            SetTimeScale();
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.T))
            {
                currentTimeScaleIndex++;
                SetTimeScale();
            }
        }

        private void SetTimeScale()
        {
            if (timeScales == null || timeScales.Count == 0) return;
            currentTimeScaleIndex %= timeScales.Count;
            Time.timeScale = timeScales[currentTimeScaleIndex];
        }
    }
}
