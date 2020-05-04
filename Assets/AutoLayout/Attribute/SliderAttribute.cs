using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AutoLayout.Attribute {

    //!
    //! @brief スライダー
    //!
    public class SliderAttribute : System.Attribute {

        public string label;
        public float min;
        public float max;

        public SliderAttribute( string label, float min, float max ){
            this.label = label;
            this.min = min;
            this.max = max;
        }
    }
}