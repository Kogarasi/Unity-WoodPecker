using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoLayout.Attribute {

    public class PresetAttribute: System.Attribute {
        public string assetName;
        
        public PresetAttribute( string assetName ){
            this.assetName = assetName;
        }
    }
}