using System;

namespace WoodPecker.Attribute {
    public class SliderAttribute: LayoutElementAttribute {
        public string label { get; set; }
        public float min { get; set; }
        public float max { get; set; }

        public override object InvokeIfConformed( Interface.IRenderer renderer, Type type, object value ){
            if( type == typeof( float ) ){
                return renderer.Slider( (float)value, this );
            } else {
                return value;
            }
        }
    }
}