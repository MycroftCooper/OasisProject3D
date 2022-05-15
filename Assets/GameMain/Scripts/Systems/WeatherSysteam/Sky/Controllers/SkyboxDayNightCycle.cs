#pragma warning disable 649
using System.Diagnostics.CodeAnalysis;
using Borodar.FarlandSkies.Core.Helpers;
using Borodar.FarlandSkies.LowPoly.DotParams;
using UnityEngine;

namespace Borodar.FarlandSkies.LowPoly {
    [ExecuteInEditMode]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    [HelpURL("http://www.borodar.com/stuff/farlandskies/lowpoly/docs/QuickStart_v2.5.1.pdf")]
    public class SkyboxDayNightCycle : Singleton<SkyboxDayNightCycle> {
        // Sky

        [SerializeField]
        private SkyParamsList _skyParamsList = new SkyParamsList();

        // Stars

        [SerializeField]
        private StarsParamsList _starsParamsList = new StarsParamsList();

        // Sun

        [SerializeField]
        [Range(0, 100)]
        private float _sunrise = 6f;

        [SerializeField]
        [Range(0, 100)]
        private float _sunset = 20f;

        [SerializeField]
        private float _sunAltitude = 45f;

        [SerializeField]
        [Tooltip("Angle between z-axis and the center of sun’s disk at sunrise")]
        private float _sunLongitude = 0f;

        [SerializeField]
        [Tooltip("A pair of angles that limit visible orbit of the sun")]
        private Vector2 _sunOrbit = new Vector2(-20f, 200f);

        [SerializeField]
        private CelestialParamsList _sunParamsList = new CelestialParamsList();

        // Moon

        [SerializeField]
        [Range(0, 100)]
        private float _moonrise = 22f;

        [SerializeField]
        [Range(0, 100)]
        private float _moonset = 5f;

        [SerializeField]
        [Tooltip("Max angle between the horizon and the center of moon’s disk")]
        private float _moonAltitude = 45f;

        [SerializeField]
        [Tooltip("Angle between z-axis and the center of moon’s disk at moonrise")]
        private float _moonLongitude = 0f;

        [SerializeField]
        [Tooltip("A pair of angles that limit visible orbit of the moon")]
        private Vector2 _moonOrbit = new Vector2(-20f, 200f);

        [SerializeField]
        private CelestialParamsList _moonParamsList = new CelestialParamsList();

        // Clouds

        [SerializeField]
        private CloudsParamsList _cloudsParamsList = new CloudsParamsList();

        // General

        [SerializeField]
        [Tooltip("Reduce the skybox day-night cycle update to run every \"n\" frames")]
        private int _framesInterval = 2;

        // Private

        private SkyboxController _skyboxController;

        private float _sunDuration;
        private Vector3 _sunAttitudeVector;
        private float _moonDuration;
        private Vector3 _moonAttitudeVector;
        private int _framesToSkip;
        private bool _initialized;

        //---------------------------------------------------------------------
        // Properties
        //---------------------------------------------------------------------

        private float _timeOfDay;

        /// <summary>
        /// Time of day, in percents (0-100).</summary>
        public float TimeOfDay {
            get { return _timeOfDay; }
            set { _timeOfDay = value % 100; }
        }

        public SkyParam CurrentSkyParam { get; private set; }
        public StarsParam CurrentStarsParam { get; private set; }
        public CelestialParam CurrentSunParam { get; private set; }
        public CelestialParam CurrentMoonParam { get; private set; }
        public CloudsParam CurrentCloudsParam { get; private set; }

        //---------------------------------------------------------------------
        // Messages
        //---------------------------------------------------------------------

        protected void Awake() {
            // Sun position
            _sunDuration = (_sunrise < _sunset) ? _sunset - _sunrise : 100f - _sunrise + _sunset;

            var radAngle = _sunAltitude * Mathf.Deg2Rad;
            _sunAttitudeVector = new Vector3(Mathf.Sin(radAngle), Mathf.Cos(radAngle));

            // Moon position
            _moonDuration = (_moonrise < _moonset) ? _moonset - _moonrise : 100f - _moonrise + _moonset;

            radAngle = _moonAltitude * Mathf.Deg2Rad;
            _moonAttitudeVector = new Vector3(Mathf.Sin(radAngle), Mathf.Cos(radAngle));

            // DOT params
            _skyParamsList.Init();
            _starsParamsList.Init();
            _sunParamsList.Init();
            _moonParamsList.Init();
            _cloudsParamsList.Init();
        }

        public void Start() {
            _skyboxController = SkyboxController.Instance;
            CurrentSkyParam = _skyParamsList.GetParamPerTime(TimeOfDay);
            CurrentStarsParam = _starsParamsList.GetParamPerTime(TimeOfDay);
            CurrentSunParam = _sunParamsList.GetParamPerTime(TimeOfDay);
            CurrentMoonParam = _moonParamsList.GetParamPerTime(TimeOfDay);
            CurrentCloudsParam = _cloudsParamsList.GetParamPerTime(TimeOfDay);
            _initialized = true;
        }

        public void Update() {
            if (--_framesToSkip > 0) return;
            _framesToSkip = _framesInterval;

            UpdateSky();
            UpdateStars();
            UpdateSun();
            UpdateMoon();
            UpdateClouds();
        }

        protected void OnValidate() {
            if (!_initialized) return;
            _skyboxController = SkyboxController.Instance;

            // Sky
            _skyParamsList.Update();

            // Stars
            if (_skyboxController.StarsEnabled) {
                _starsParamsList.Update();
            }

            // Sun
            if (_skyboxController.SunEnabled) {
                _sunParamsList.Update();

                // position
                _sunDuration = (_sunrise < _sunset) ? _sunset - _sunrise : 100f - _sunrise + _sunset;
                var radAngle = _sunAltitude * Mathf.Deg2Rad;
                _sunAttitudeVector = new Vector3(Mathf.Sin(radAngle), Mathf.Cos(radAngle));
            }

            // Moon
            if (_skyboxController.MoonEnabled) {
                _moonParamsList.Update();

                // position
                _moonDuration = (_moonrise < _moonset) ? _moonset - _moonrise : 100f - _moonrise + _moonset;
                var radAngle = _moonAltitude * Mathf.Deg2Rad;
                _moonAttitudeVector = new Vector3(Mathf.Sin(radAngle), Mathf.Cos(radAngle));
            }

            // Clouds
            if (_skyboxController.CloudsEnabled) {
                _cloudsParamsList.Update();
            }
        }

        //---------------------------------------------------------------------
        // Helpers
        //---------------------------------------------------------------------

        private void UpdateSky() {
            CurrentSkyParam = _skyParamsList.GetParamPerTime(TimeOfDay);

            _skyboxController.TopColor = CurrentSkyParam.TopColor;
            _skyboxController.MiddleColor = CurrentSkyParam.MiddleColor;
            _skyboxController.BottomColor = CurrentSkyParam.BottomColor;
            _skyboxController.CloudsTint = CurrentSkyParam.CloudsTint;
        }

        private void UpdateStars() {
            if (!_skyboxController.StarsEnabled) return;
            CurrentStarsParam = _starsParamsList.GetParamPerTime(TimeOfDay);
            _skyboxController.StarsTint = CurrentStarsParam.TintColor;
        }

        private void UpdateSun() {
            if (!_skyboxController.SunEnabled) return;

            // rotation
            if (TimeOfDay > _sunrise || TimeOfDay < _sunset) {
                var sunCurrent = (_sunrise < TimeOfDay) ? TimeOfDay - _sunrise : 100f + TimeOfDay - _sunrise;
                var ty = (sunCurrent < _sunDuration) ? sunCurrent / _sunDuration : (_sunDuration - sunCurrent) / _sunDuration;
                var dy = Mathf.Lerp(_sunOrbit.x, _sunOrbit.y, ty);
                var rotation = Quaternion.AngleAxis(_sunLongitude - 180, Vector3.up) * Quaternion.AngleAxis(dy, _sunAttitudeVector);
                rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
                _skyboxController.SunLight.transform.rotation = rotation;
            }

            // colors
            CurrentSunParam = _sunParamsList.GetParamPerTime(TimeOfDay);

            _skyboxController.SunTint = CurrentSunParam.TintColor;
            _skyboxController.SunLight.color = CurrentSunParam.LightColor;
            _skyboxController.SunLight.intensity = CurrentSunParam.LightIntencity;
        }

        private void UpdateMoon() {
            if (!_skyboxController.MoonEnabled) return;

            // rotation
            if (TimeOfDay > _moonrise || TimeOfDay < _moonset) {
                var moonCurrent = (_moonrise < TimeOfDay) ? TimeOfDay - _moonrise : 100f + TimeOfDay - _moonrise;
                var ty = (moonCurrent < _moonDuration) ? moonCurrent / _moonDuration : (_moonDuration - moonCurrent) / _moonDuration;
                var dy = Mathf.Lerp(_moonOrbit.x, _moonOrbit.y, ty);
                var rotation = Quaternion.AngleAxis(_moonLongitude - 180, Vector3.up) * Quaternion.AngleAxis(dy, _moonAttitudeVector);
                rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
                _skyboxController.MoonLight.transform.rotation = rotation;
            }

            // colors
            CurrentMoonParam = _moonParamsList.GetParamPerTime(TimeOfDay);

            _skyboxController.MoonTint = CurrentMoonParam.TintColor;
            _skyboxController.MoonLight.color = CurrentMoonParam.LightColor;
            _skyboxController.MoonLight.intensity = CurrentMoonParam.LightIntencity;
        }

        private void UpdateClouds() {
            if (!_skyboxController.CloudsEnabled) return;
            CurrentCloudsParam = _cloudsParamsList.GetParamPerTime(TimeOfDay);
            _skyboxController.CloudsTint = CurrentCloudsParam.TintColor;
        }
    }
}
