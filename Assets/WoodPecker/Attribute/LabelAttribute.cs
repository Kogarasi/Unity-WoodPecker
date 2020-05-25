using System;
using UnityEngine;

namespace WoodPecker.Attribute {
    public class LabelAttribute: LayoutElementAttribute {
        public string label { get; set; }

        public override object InvokeIfConformed( Interface.IRenderer renderer, Type type, object value ){

            if( type == typeof( float ) ){
                return renderer.FloatField( (float)value, label );
            } else if( type == typeof( int ) ){
                return renderer.IntField( (int)value, label );
            } else if( type == typeof( bool ) ){
                return renderer.BoolField( (bool)value, label );
            } else if( type == typeof( string ) ){
                return renderer.TextField( value as string, label );
            } else if( type == typeof( Vector2 ) ){
                return renderer.VectorField( (Vector2)value, label );
            } else if( type == typeof( Vector3 ) ){
                return renderer.VectorField( (Vector3)value, label );
            } else if( type == typeof( Vector4 ) ){
                return renderer.VectorField( (Vector4)value, label );
            } else if( type == typeof( Color ) ){
                return renderer.ColorField( (Color)value, label );
            }

            return null;
        }
    }
}