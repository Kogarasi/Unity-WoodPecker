using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoLayout.Attribute {
    public class AxisAttribute : System.Attribute {

        public Entity.Axis enabledAxis;

        public AxisAttribute( Entity.Axis enabledAxis ){
            this.enabledAxis = enabledAxis;
        }

    }
}