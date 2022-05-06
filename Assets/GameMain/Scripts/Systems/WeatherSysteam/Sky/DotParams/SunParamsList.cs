using Borodar.FarlandSkies.Core.DotParams;
using UnityEngine;

namespace Borodar.FarlandSkies.LowPoly.DotParams
{
    public class SunParamsList : DotParamsList<SunParam>
    {
        //---------------------------------------------------------------------
        // Ctors
        //---------------------------------------------------------------------

        public SunParamsList(int capacity) : base(capacity) { }

        //---------------------------------------------------------------------
        // Public
        //---------------------------------------------------------------------

        public SunParam GetParamPerTime(float currentTime)
        {
            if (Count <= 0)
            {
                Debug.LogWarning("Sun params list is empty");
                Add(0, new SunParam());
            }

            var index = FindIndexPerTime(currentTime);

            if (index < 1) index = Count;

            var timeKey1 = Keys[index - 1];
            var value = Values[index - 1];
            var tintColor1 = value.TintColor;
            var lightColor1 = value.LightColor;
            var lightIntencity1 = value.LightIntencity;

            if (index >= Count) index = 0;
             
            var timeKey2 = Keys[index];
            value = Values[index];
            var tintColor2 = value.TintColor;
            var lightColor2 = value.LightColor;
            var lightIntencity2 = value.LightIntencity;

            var t1 = (currentTime > timeKey1) ?  currentTime - timeKey1 : currentTime + (100f - timeKey1);
            var t2 = (timeKey1 < timeKey2) ? timeKey2 - timeKey1 : 100f + timeKey2 - timeKey1;
            var t = t1/t2;

            var currentParam = new SunParam()
            {
                TintColor = Color.Lerp(tintColor1, tintColor2, t),
                LightColor = Color.Lerp(lightColor1, lightColor2, t),
                LightIntencity = Mathf.Lerp(lightIntencity1, lightIntencity2, t)
            };

            return currentParam;
        }
    }
}