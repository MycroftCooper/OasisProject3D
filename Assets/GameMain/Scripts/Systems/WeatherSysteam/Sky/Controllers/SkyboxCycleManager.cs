using UnityEngine;
using QuickGameFramework.Runtime;

namespace Borodar.FarlandSkies.LowPoly {
    [ExecuteInEditMode]
    [HelpURL("http://www.borodar.com/stuff/farlandskies/lowpoly/docs/QuickStart_v2.5.1.pdf")]
    public class SkyboxCycleManager : MonoSingleton<SkyboxCycleManager> {
        [Tooltip("Day-night cycle duration from 0% to 100% (in seconds)")]
        public float CycleDuration = 10f;

        [Tooltip("Current time of day (in percents)")]
        public float CycleProgress;

        public bool Paused;

        private SkyboxDayNightCycle _dayNightCycle;

        //---------------------------------------------------------------------
        // Messages
        //---------------------------------------------------------------------

        protected void Start() {
            _dayNightCycle = SkyboxDayNightCycle.Instance;
            UpdateTimeOfDay();
        }

        protected void Update() {
            if (Application.isPlaying && !Paused) {
                CycleProgress += (Time.deltaTime / CycleDuration) * 100f;
                CycleProgress %= 100f;
            }

            UpdateTimeOfDay();
        }

        protected void OnValidate() {
            UpdateTimeOfDay();
        }

        //---------------------------------------------------------------------
        // Helpers
        //---------------------------------------------------------------------

        private void UpdateTimeOfDay() {
            if (_dayNightCycle != null)
                _dayNightCycle.TimeOfDay = CycleProgress;
        }
    }
}