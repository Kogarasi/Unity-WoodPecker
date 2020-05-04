using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoLayout.Attribute {
    public class CheckboxAttribute : System.Attribute {
        public string label;
        public bool isFlaggable;

        public CheckboxAttribute( string label, bool isFlaggable = true ){
            this.label = label;
            this.isFlaggable = isFlaggable;
        }
    }
}