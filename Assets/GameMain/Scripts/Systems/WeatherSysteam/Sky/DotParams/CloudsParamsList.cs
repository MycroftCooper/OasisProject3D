using System;
using Borodar.FarlandSkies.Core.DotParams;
using UnityEngine;

namespace Borodar.FarlandSkies.LowPoly.DotParams
{
    [Serializable]
    public class CloudsParamsList : SortedParamsList<CloudsParam>
    {
        public CloudsParam GetParamPerTime(float currentTime)
        {
            if (SortedParams.Count <= 0)
            {
                Debug.LogWarning("Clouds params list is empty");
                SortedParams.Add(0, new CloudsParam());
            }

            var index = SortedParams.FindIndexPerTime(currentTime);

            if (index < 1) index = SortedParams.Count;

            var timeKey1 = SortedParams.Keys[index - 1];
            var value = SortedParams.Values[index - 1];
            var tintColor1 = value.TintColor;

            if (index >= SortedParams.Count) index = 0;

            var timeKey2 = SortedParams.Keys[index];
            value = SortedParams.Values[index];
            var tintColor2 = value.TintColor;

            var t1 = (currentTime > timeKey1) ? currentTime - timeKey1 : currentTime + (100f - timeKey1);
            var t2 = (timeKey1 < timeKey2) ? timeKey2 - timeKey1 : 100f + timeKey2 - timeKey1;
            var t = t1 / t2;

            var currentParam = new CloudsParam()
            {
                TintColor = Color.Lerp(tintColor1, tintColor2, t),
            };

            return currentParam;
        }
    }
}