using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AutoLayout {

    //!
    //! @brief 自動でGUIレイアウトを組む仕組み
    //!
    public class AutoLayout {

        private UiRenderer renderer = new UiRenderer();
        private Element[] elements;

        //!
        //! @brief コンストラクタ
        //!
        public AutoLayout( params Element[] elements ){
            this.elements = elements;
        }

        //!
        //! @brief UI描画
        //!
        public void Render(){
            elements.ToList().ForEach( elem => {

                var type = elem.GetType();

                // クラスに設定されているアイテムを処理
                RenderItemWithType<Attribute.StackAttribute>( type, renderer.Orientation );
                RenderItemWithType<Attribute.LabelAttribute>( type, renderer.Label );

                if( isVisibleItems( type ) ){
                    // フィールドに設定されているアイテムを処理
                    type.GetFields( BindingFlags.Public | BindingFlags.Instance )
                        .ToList().ForEach( info => RenderField( elem, info ) );

                    // メソッドに設定されているアイテムを処理
                    type.GetMethods( BindingFlags.Public | BindingFlags.Instance )
                        .ToList().ForEach( info => RenderMethod( elem, info ) );
                }

                renderer.PopStackOrientation();
            });
        }

        //!
        //! @brief 指定したUIを描画する with Type parameter.
        //! @brief Typeから指定したAttrを取得して、次の関数に渡します
        //!
        void RenderItemWithType<Attr>( Type type, Action<Attr> handler ) where Attr: System.Attribute {
            var attrType = typeof( Attr );
            var attr = type.GetCustomAttributes( attrType, false ).FirstOrDefault();

            if( attr != null ){
                handler( attr as Attr );
            }
        }

        //!
        //! @brief FieldInfoに基づいてUIを描画
        //!
        void RenderField( Element elem, FieldInfo info ){

            var type = info.FieldType;
            var value = info.GetValue( elem );

            // Listを持ってるか。もってなければ次
            var listAttr = info.GetCustomAttribute<Attribute.ListAttribute>();
            if( listAttr != null ){

                // ListElementインターフェースを持っている場合にはそちらを優先させる
                if( typeof( Interface.ListElement ).IsAssignableFrom( type ) ){
                    renderer.List( value as Interface.ListElement, listAttr );
                } else {
                    renderer.List( value, listAttr );
                }
            }

            // Sliderを持ってるか。持ってなければ次
            var sliderAttr = info.GetCustomAttribute<Attribute.SliderAttribute>();
            if( sliderAttr != null ){

                if( type == typeof( float ) ){
                    info.SetValue( elem, renderer.Slider( (float)value, sliderAttr ) );
                } else if( type == typeof( Vector3 ) ){
                    info.SetValue( elem, renderer.Slider( (Vector3)value, sliderAttr ) );
                }
            }

            // Transformを持っているか。持ってなければ次
            var transAttr = info.GetCustomAttribute<Attribute.TransformAttribute>();
            if( transAttr != null ){

                var axisAttr = info.GetCustomAttribute<Attribute.AxisAttribute>();

                info.SetValue( elem, renderer.Transform( value as Vector3[], transAttr, axisAttr ) );
            }

            // Checkboxを持っているか、持ってなければ次
            var checkboxAttr = info.GetCustomAttribute<Attribute.CheckboxAttribute>();
            if( checkboxAttr != null ){

                if( type.IsEnum ){
                    info.SetValue( elem, renderer.Enum( value as Enum, checkboxAttr ) );
                }
            }

            // Labelを持ってるか。もってないならもうなにもしない
            var labelAttr = info.GetCustomAttribute<Attribute.LabelAttribute>();

            if( labelAttr != null ){
                object rValue = null;

                if( type == typeof( string ) ){
                    rValue = renderer.Text( value as string, labelAttr );
                } else if( type == typeof( Vector2 ) ){
                    rValue = renderer.Vector( (Vector2)value, labelAttr );
                } else if( type == typeof( Vector3 ) ){
                    rValue = renderer.Vector( (Vector3)value, labelAttr );
                } else if( type == typeof( Vector4 ) ){
                    rValue = renderer.Vector( (Vector4)value, labelAttr );
                } else if( type == typeof( Color ) ){
                    var presetAttr = info.GetCustomAttribute<Attribute.PresetAttribute>();
                    rValue = renderer.Color( (Color)value, labelAttr, presetAttr );
                } else if( type == typeof( float ) ){
                    rValue = renderer.Float( (float)value, labelAttr );
                } else if( type == typeof( int ) ){
                    rValue = renderer.Integer( (int)value, labelAttr );
                } else if( type == typeof( bool ) ){
                    rValue = renderer.Bool( (bool)value, labelAttr );
                }

                info.SetValue( elem, rValue );
            }
        }

        //!
        //! @brief ボタンを描画
        //!
        void RenderMethod( Element elem, MethodInfo info ){
            // button
            var buttonAttr = info.GetCustomAttribute<Attribute.ButtonAttribute>();

            if( buttonAttr != null ){
                renderer.Button( ()=>{
                    info.Invoke( elem, null );
                }, buttonAttr );
            }

            // Custom Rendering
            var customRenderAttr = info.GetCustomAttribute<Attribute.CustomRenderAttribute>();

            if( customRenderAttr != null ){
                info.Invoke( elem, new object[]{} );
            }
        }

        //!
        //! @brief 要素を表示するか
        //!
        bool isVisibleItems( Type type ){
            var attr = type.GetCustomAttribute<Attribute.LabelAttribute>();
            if( attr == null ){
                return true;
            }

            return renderer.getFoldStatus( attr );
        }
    }

}