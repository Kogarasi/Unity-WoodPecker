using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WoodPecker {

    //!
    //! @brief ILayout拡張
    //!
    public static class ILayoutExtension {

        //!
        //! @brief UI要素のレンダリング
        //!
        public static void RenderContent( this Interface.ILayout layout, Interface.IRenderer renderer ){
            var type = layout.GetType();

            var fields = type.GetFields( BindingFlags.Public | BindingFlags.Instance );

            var options = new Dictionary<Entity.LayoutOptionType, FieldInfo>();
            fields.Select( f => new { field = f, attr = f.GetCustomAttribute<Attribute.LayoutOptionAttribute>() } )
            .Where( p => p.attr != null )
            .ToList()
            .ForEach( p => {
                options[ p.attr.type ] = p.field;
            });

            var completion = layout.RenderBox( renderer, options );

            fields.Select( f => new { field = f, attr = f.GetCustomAttribute<Attribute.LayoutElementAttribute>() } )
            .Where( p => p.attr != null )
            .ToList()
            .ForEach( pair => layout.RenderInnerContent( renderer, pair.field, pair.attr ) );

            completion();
        }

        //!
        //! @brief 囲いのレンダリング
        //!
        private static Action RenderBox( this Interface.ILayout layout, Interface.IRenderer renderer, Dictionary<Entity.LayoutOptionType, FieldInfo> options ){
            var foldable = false;
            var caption = "";

            renderer.PushOrientation();

            // Foldable
            if( options.ContainsKey( Entity.LayoutOptionType.Foldable ) ){
                var fi = options[ Entity.LayoutOptionType.Foldable ];

                foldable = (bool)fi.GetValue( layout );
            }

            // Caption
            if( options.ContainsKey( Entity.LayoutOptionType.Caption ) ){
                var fi = options[ Entity.LayoutOptionType.Caption ];

                caption = fi.GetValue( layout ) as string;
            }

            if( foldable ){
                var status = renderer.FoldLabel( caption, true );
            } else if( caption.Any() ){
                renderer.Label( caption );
            }

            return ()=>{
                renderer.PopOrientation();
            };
        }

        //!
        //! @brief ボックスの中のコンテンツをレンダリング
        //!
        public static void RenderInnerContent( this Interface.ILayout layout, Interface.IRenderer renderer, FieldInfo field, Attribute.LayoutElementAttribute element ){
            var prevValue = field.GetValue( layout );
            var newValue = element.InvokeIfConformed( renderer, field.FieldType, prevValue );
            field.SetValue( layout, newValue );
        }
    }
}