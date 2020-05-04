using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoLayout.ScriptableObject {

    [CreateAssetMenu]
    public class ColorPreset : UnityEngine.ScriptableObject {
        public List<Color> colors;

        public static ColorPreset Load( string assetName ){
            return Resources.Load<ColorPreset>( "Color/" + assetName );
        }
    }
}