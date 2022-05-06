using System;
using Borodar.FarlandSkies.Core.DotParams;
using UnityEngine;

namespace Borodar.FarlandSkies.LowPoly.DotParams
{
    [Serializable]
    public class CelestialParamsList : SortedParamsList<CelestialParam>
    {
        public CelestialParam GetParamPerTime(float currentTime)
        {
            if (SortedParams.Count <= 0)
            {
                Debug.LogWarning("Celestial params list is empty");
                SortedParams.Add(0, new CelestialParam());
            }

            var index = SortedParams.FindIndexPerTime(currentTime);

            if (index < 1) index = SortedParams.Count;

            var timeKey1 = SortedParams.Keys[index - 1];
            var value = SortedParams.Values[index - 1];
            var tintColor1 = value.TintColor;
            var lightColor1 = value.LightColor;
            var lightIntencity1 = value.LightIntencity;

            if (index >= SortedParams.Count) index = 0;
             
            var timeKey2 = SortedParams.Keys[index];
            value = SortedParams.Values[index];
            var tintColor2 = value.TintColor;
            var lightColor2 = value.LightColor;
            var lightIntencity2 = value.LightIntencity;

            var t1 = (currentTime > timeKey1) ?  currentTime - timeKey1 : currentTime + (100f - timeKey1);
            var t2 = (timeKey1 < timeKey2) ? timeKey2 - timeKey1 : 100f + timeKey2 - timeKey1;
            var t = t1/t2;

            var currentParam = new CelestialParam()
            {
                TintColor = Color.Lerp(tintColor1, tintColor2, t),
                LightColor = Color.Lerp(lightColor1, lightColor2, t),
                LightIntencity = Mathf.Lerp(lightIntencity1, lightIntencity2, t)
            };

            return currentParam;
        }
    }
}