using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoLayout.Attribute {

    public class LabelAttribute: System.Attribute {
        public string label;
        public bool isFoldable;

        public LabelAttribute( string label, bool isFoldable = false ){
            this.label = label;
            this.isFoldable = isFoldable;
        }
    }
}