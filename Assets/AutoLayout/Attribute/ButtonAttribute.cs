using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AutoLayout.Attribute {
    public class ButtonAttribute : System.Attribute {
        public string label;
        public bool confirm;

        public ButtonAttribute( string label, bool confirm = false ){
            this.label = label;
            this.confirm = confirm;
        }
    }
}