using UnityEngine;

namespace Borodar.FarlandSkies.LowPoly
{
    //[CreateAssetMenu(fileName = "SkyboxControllerPresets", menuName = "SkyboxControllerPresets", order = 1)]
    public class SkyboxControllerPresets : ScriptableObject
    {
        [Header("Stars")]

        public Cubemap[] StarsCubemaps;
        public string[] StarsCubemapNames;

        [Header("Sun")]

        public Texture2D[] SunTextures;
        public string[] SunTextureNames;

        [Header("Moon")]

        public Texture2D[] MoonTextures;
        public string[] MoonTextureNames;

        [Header("Clouds")]

        public Cubemap[] CloudsCubemaps;
        public string[] CloudsCubemapNames;
    }
}