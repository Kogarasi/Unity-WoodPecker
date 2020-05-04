using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoLayout.Attribute {
    public class TransformAttribute: System.Attribute {

        public string label;

        public float transMin;
        public float transMax;

        public float scaleMin;
        public float scaleMax;

        public float rotateMin;
        public float rotateMax;

        public TransformAttribute( string label, float transMin = -5f, float transMax = 5f, float scaleMin = 0f, float scaleMax = 2f, float rotateMin = -360f, float rotateMax = 360f ){
            this.label = label;

            this.transMin = transMin;
            this.transMax = transMax;

            this.scaleMin = scaleMin;
            this.scaleMax = scaleMax;

            this.rotateMin = rotateMin;
            this.rotateMax = rotateMax;
        }
    }
}