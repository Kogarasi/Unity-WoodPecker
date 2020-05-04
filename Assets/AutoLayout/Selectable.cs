using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

namespace AutoLayout {
    public class Selectable<T> {

        private int _selected;

        public int selected {
            get {
                return _selected;
            }

            set {
                var old = _selected;
                _selected = value;
                if( onChangedValue != null ){
                    onChangedValue( old, value );
                }
            }
        }

        private Expression<Func<List<T>>> expression;
        private Expression<Func<List<Texture2D>>> previewExpression;

        private MemberExpression memberExp {
            get {
                return (MemberExpression)expression.Body;
            }
        }

        public List<T> resources {
            get {
                return expression.Compile()();
            }
        }

        public List<Texture2D> previews {
            get {

                if( previewExpression == null ){
                    return new List<Texture2D>();
                } else {
                    return previewExpression.Compile()();
                }
            }
        }

        public Action<int,int> onChangedValue;

        public Selectable( Expression<Func<List<T>>> exp, Expression<Func<List<Texture2D>>> previewExp = null ){
            expression = exp;
            previewExpression = previewExp;
        }

        public T Load(){
            return resources[ selected ];
        }

        public Texture2D LoadPreview(){
            if( previews == null ){
                return default;
            } else {
                return previews[ selected ];
            }
        }

        public void Unselect(){
            selected = -1;
        }
    }
}