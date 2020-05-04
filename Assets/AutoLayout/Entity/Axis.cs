using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoLayout.Entity {
    
    [System.Flags]
    public enum Axis: int {
        TranslateX = 1,
        TranslateY = 2,
        TranslateZ = 4,

        TranslateAll = TranslateX | TranslateY | TranslateZ,

        RotationX = 8,
        RotationY = 16,
        RotationZ = 32,

        RotationAll = RotationX | RotationY | RotationZ,

        ScaleX = 64,
        ScaleY = 128,
        ScaleZ = 256,

        ScaleAll = ScaleX | ScaleY | ScaleZ,

        All = TranslateAll | RotationAll | ScaleAll
    }
}