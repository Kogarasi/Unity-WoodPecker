using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace AutoLayout {
    public class UiRenderer {
        
        private Stack<Attribute.StackAttribute.Orientation> stack = new Stack<Attribute.StackAttribute.Orientation>();
        private Dictionary<int, Vector2> scrollPositions = new Dictionary<int, Vector2>();
        private Dictionary<int, bool> foldStatus = new Dictionary<int, bool>();

        //!
        //! @brief UIの整列方向の設定
        //!
        public void Orientation( Attribute.StackAttribute attr ){
            var skin = attr.isBoxed ? GUI.skin.box : GUIStyle.none;

            PushStackOrientation( attr.orientation, skin );
        }

        //!
        //! @brief ラベルの表示
        //!
        public void Label( Attribute.LabelAttribute attr ){

            if( attr.isFoldable ){
                var status = getFoldStatus( attr );

                status = EditorGUILayout.Foldout( status, attr.label );
                setFoldStatus( attr, status );
            } else {
                EditorGUILayout.LabelField( attr.label );
            }

        }

        //!
        //! @brief テキスト入力の表示
        //!
        public string Text( string previous, Attribute.LabelAttribute attr = null ){
            if( attr != null ){
                return EditorGUILayout.TextField( label: attr.label, text: previous );
            } else {
                return EditorGUILayout.TextField( text: previous );
            }
        }

        //!
        //! @brief 列挙型
        //!
        public Enum @Enum( Enum bit, Attribute.CheckboxAttribute checkboxAttr ){

            if( checkboxAttr.isFlaggable ){
                return EditorGUILayout.EnumFlagsField( checkboxAttr.label, bit );
            } else {
                return EditorGUILayout.EnumPopup( checkboxAttr.label, bit );
            }

        }

        //!
        //! @brief カラー
        //! @brief "@"マークで予約語回避してるよ！
        //!
        public Color @Color( Color previous, Attribute.LabelAttribute labelAttr = null, Attribute.PresetAttribute presetAttr = null ){
            Color tmp = default;

            // Color Field表示
            if( labelAttr != null ){
                tmp = EditorGUILayout.ColorField( label: labelAttr.label, value: previous );
            } else {
                tmp = EditorGUILayout.ColorField( previous );
            }

            if( presetAttr != null ){
                var preset = ScriptableObject.ColorPreset.Load( presetAttr.assetName );
                var rect = EditorGUILayout.GetControlRect();

                var width = 40;
                var height = 20;
                var offset = 150;

                preset.colors.Select( (c,i)=> new { color = c, index = i } ).ToList().ForEach( (pair) => {
                    EditorGUI.DrawRect( new Rect( rect.x + offset + width * pair.index, rect.y, width, height ), pair.color );
                    var selected = GUI.Button( new Rect( rect.x + offset + width * pair.index, rect.y + height, width, height ), "選択" );

                    if( selected ){
                        tmp = pair.color;
                    }
                });                                
                GUILayout.Space( 40 );
            }

            return tmp;
        }

        //!
        //! @brief ベクトル系
        //!
        public Vector2 Vector( Vector2 previous, Attribute.LabelAttribute attr ){
            return EditorGUILayout.Vector2Field( label: attr.label, value: previous );
        }

        public Vector3 Vector( Vector3 previous, Attribute.LabelAttribute attr ){
            return EditorGUILayout.Vector3Field( label: attr.label, value: previous );
        }

        public Vector4 Vector( Vector4 previous, Attribute.LabelAttribute attr ){
            return EditorGUILayout.Vector4Field( label:attr.label, value: previous );
        }

        //!
        //! @brief int
        //!
        public int Integer( int previous, Attribute.LabelAttribute attr ){
            return EditorGUILayout.IntField( label: attr.label, value: previous );
        }

        //!
        //! @brief float
        //!
        public float Float( float previous, Attribute.LabelAttribute attr ){
            return EditorGUILayout.FloatField( label: attr.label, value: previous );
        }

        //!
        //! @brief bool
        //!
        public bool Bool( bool previous, Attribute.LabelAttribute attr ){
            return EditorGUILayout.Toggle( label: attr.label, value: previous );
        }

        //!
        //! @brief スライダー
        //!
        public float Slider( float previous, Attribute.SliderAttribute attr ){
            return EditorGUILayout.Slider( label: attr.label, value: previous, leftValue: attr.min, rightValue: attr.max );
        }

        public Vector3 Slider( Vector3 previous, Attribute.SliderAttribute attr ){

            PushStackOrientation( Attribute.StackAttribute.Orientation.Vertical, GUI.skin.box );
            var x = EditorGUILayout.Slider( label: attr.label + "(X)", value: previous.x, leftValue: attr.min, rightValue: attr.max );
            var y = EditorGUILayout.Slider( label: attr.label + "(Y)", value: previous.y, leftValue: attr.min, rightValue: attr.max );
            var z = EditorGUILayout.Slider( label: attr.label + "(Z)", value: previous.z, leftValue: attr.min, rightValue: attr.max );

            PopStackOrientation();

            return new Vector3( x, y, z );
        }

        //!
        //! @brief Transform
        //!
        public Vector3[] Transform( Vector3[] previous, Attribute.TransformAttribute transAttr, Attribute.AxisAttribute axisAttr = null ){
            PushStackOrientation( Attribute.StackAttribute.Orientation.Vertical, GUI.skin.box );

            if( axisAttr == null ){
                return previous;
            }

            var trans = previous[0];
            if( axisAttr.enabledAxis.HasFlag( Entity.Axis.TranslateX ) ){
                trans.x = EditorGUILayout.Slider( label: transAttr.label + "(Trans)(X)", value: previous[0].x, leftValue: transAttr.transMin, rightValue: transAttr.transMax );
            }

            if( axisAttr.enabledAxis.HasFlag( Entity.Axis.TranslateY ) ){
                trans.y = EditorGUILayout.Slider( label: transAttr.label + "(Trans)(Y)", value: previous[0].y, leftValue: transAttr.transMin, rightValue: transAttr.transMax );
            }

            if( axisAttr.enabledAxis.HasFlag( Entity.Axis.TranslateZ ) ){
                trans.z = EditorGUILayout.Slider( label: transAttr.label + "(Trans)(Z)", value: previous[0].z, leftValue: transAttr.transMin, rightValue: transAttr.transMax );
            }

            var rotate = previous[1];

            if( axisAttr.enabledAxis.HasFlag( Entity.Axis.RotationX ) ){
               rotate.x = EditorGUILayout.Slider( label: transAttr.label + "(Rotate)(X)", value: previous[1].x, leftValue: transAttr.rotateMin, rightValue: transAttr.rotateMax );
            }

            if( axisAttr.enabledAxis.HasFlag( Entity.Axis.RotationY ) ){
                rotate.y = EditorGUILayout.Slider( label: transAttr.label + "(Rotate)(Y)", value: previous[1].y, leftValue: transAttr.rotateMin, rightValue: transAttr.rotateMax );
            }

            if( axisAttr.enabledAxis.HasFlag( Entity.Axis.RotationZ ) ){
                rotate.z = EditorGUILayout.Slider( label: transAttr.label + "(Rotate)(Z)", value: previous[1].z, leftValue: transAttr.rotateMin, rightValue: transAttr.rotateMax );
            }

            var scale = previous[2];

            if( axisAttr.enabledAxis.HasFlag( Entity.Axis.ScaleX ) ){
                scale.x = EditorGUILayout.Slider( label: transAttr.label + "(Scale)(X)", value: previous[2].x, leftValue: transAttr.scaleMin, rightValue: transAttr.scaleMax );
            }

            if( axisAttr.enabledAxis.HasFlag( Entity.Axis.ScaleY ) ){
                scale.y = EditorGUILayout.Slider( label: transAttr.label + "(Scale)(Y)", value: previous[2].y, leftValue: transAttr.scaleMin, rightValue: transAttr.scaleMax );
            }

            if( axisAttr.enabledAxis.HasFlag( Entity.Axis.ScaleZ ) ){
                scale.z = EditorGUILayout.Slider( label: transAttr.label + "(Scale)(Z)", value: previous[2].z, leftValue: transAttr.scaleMin, rightValue: transAttr.scaleMax );
            }

            var modified = new Vector3[]{
                trans,
                rotate,
                scale
            };

            PopStackOrientation();

            return modified;
        }

        //!
        //! @brief リスト
        //!
        public void List( object selectable, Attribute.ListAttribute attr ){

            EditorGUILayout.LabelField( attr.label );

            Func<object, Type, object> getListObjectFromLazy = ( sourceList, type )=>{
                return type.GetProperty( "Value", BindingFlags.Public | BindingFlags.Instance ).GetValue( sourceList );
            };

            var scrollKey = selectable.GetHashCode();
            if( !scrollPositions.ContainsKey( scrollKey ) ){
                scrollPositions[ scrollKey ] = Vector2.zero;
            }

            var height = attr.columnSize == -1 ? 60 : attr.columnSize * 60;
            scrollPositions[ scrollKey ] = GUILayout.BeginScrollView( scrollPositions[ scrollKey ], GUILayout.Height( height ) );
            PushStackOrientation( Attribute.StackAttribute.Orientation.Horizontal, GUIStyle.none );

            if( selectable is Selectable<Texture2D> ){
                var _selectable = selectable as Selectable<Texture2D>;

                var previews = _selectable.previews.Any() ? _selectable.previews : _selectable.resources;

                var selected = attr.columnSize == -1 ? RenderLayoutButton( previews, attr.useUnselect ) : RenderButton( previews, attr.useUnselect );
                if( selected >= 0 ){
                    _selectable.selected = selected;
                } else if( selected == -1 ){
                    _selectable.Unselect();
                }
            }

            if( selectable is Selectable<GameObject> ){
                var _selectable = selectable as Selectable<GameObject>;

                var usePreview = _selectable.previews.Any();
                var selected = -1;
                if( usePreview ){
                    selected = attr.columnSize == -1 ? RenderLayoutButton( _selectable.previews, attr.useUnselect ) : RenderButton( _selectable.previews, attr.useUnselect );
                } else {
                    selected = attr.columnSize == -1 ? RenderLayoutButton( _selectable.resources.Select( r => r.name ), attr.useUnselect ) : RenderButton( _selectable.resources.Select( r => r.name ), attr.useUnselect );
                }

                if( selected >= 0 ){
                    _selectable.selected = selected;
                } else if( selected == -1 ){
                    _selectable.Unselect();
                }
            }

            if( selectable is Selectable<string> ){
                var _selectable = selectable as Selectable<string>;

                var usePreview = _selectable.previews.Any();
                var selected = -1;
                if( usePreview ){
                    selected = attr.columnSize == -1 ? RenderLayoutButton( _selectable.previews, attr.useUnselect ) : RenderButton( _selectable.previews, attr.useUnselect );
                } else {
                    selected = attr.columnSize == -1 ? RenderLayoutButton( _selectable.resources, attr.useUnselect ) : RenderButton( _selectable.resources, attr.useUnselect );
                }

                if( selected >= 0 ){
                    _selectable.selected = selected;
                } else if( selected == -1 ){
                    _selectable.Unselect();
                }
            }

            PopStackOrientation();

            GUILayout.EndScrollView();

        }

        //!
        //! @brief FoldOut
        //!
        public bool FoldOut( string label, bool state ){
            return EditorGUILayout.Foldout( state, label );
        }

        //!
        //! @brief リスト
        //!
        public void List( Interface.ListElement list, Attribute.ListAttribute attr ){
            
        }

        //!
        //! @brief ボタン
        //!
        public void Button( Action handler, Attribute.ButtonAttribute attr ){
            if( GUILayout.Button( attr.label ) ){

                var confirm = true;
                if( attr.confirm ){
                    confirm = EditorUtility.DisplayDialog( "確認表示", "操作を続行します。よろしいですか？", "OK", "キャンセル" );
                }

                if( confirm ){
                    handler();
                }
            }
        }

        //!
        //! @brief StackView的なのを設定
        //!
        public void PushStackOrientation( Attribute.StackAttribute.Orientation orientation, GUIStyle skin ){
            stack.Push( orientation );
            if( orientation == Attribute.StackAttribute.Orientation.Vertical ){
                EditorGUILayout.BeginVertical( skin );
            } else {
                EditorGUILayout.BeginHorizontal( skin );
            }
        }

        //!
        //! @brief StackView的なのから1段抜ける
        public void PopStackOrientation(){
            if( stack.Pop() == Attribute.StackAttribute.Orientation.Vertical ){
                EditorGUILayout.EndVertical();
             } else {
                EditorGUILayout.EndHorizontal();
             }
        }

        //!
        //! @brief 消すだけ。StackViewの段数を処理しない
        //!
        public void ClearStack(){
            stack.Clear();
        }

        //!
        //! @brief FoldStatusを返す
        //!
        public bool getFoldStatus( Attribute.LabelAttribute attr ){
            if( !attr.isFoldable ){
                return true;
            }

            var defaultValue = false;
            var key = attr.GetHashCode();
            var status = defaultValue;

            if( foldStatus.ContainsKey( key ) ){
                status = foldStatus[ key ];
            } else {
                foldStatus[ key ] = status;
            }

            return status;
        }

        //!
        //! @brief FoldStatusを設定
        //!
        public void setFoldStatus( Attribute.LabelAttribute attr, bool status ){
            var key = attr.GetHashCode();
            foldStatus[ key ] = status;
        }

        //!
        //! @brief ボタンの表示
        //!
        public int RenderLayoutButton<T>( IEnumerable<T> items, bool useUnselect ) {
            var options = new GUILayoutOption[]{ GUILayout.Width( 60 ), GUILayout.Height( 40 ) };

            var unselected = false;
            if( useUnselect ){
                unselected = GUILayout.Button( "選択解除", options );
            }
            
            var query = items.Select( (item, index)=> (item, index) );

            if( typeof( Texture2D ).IsAssignableFrom( typeof( T ) ) ){
                var pressedItem = query.FirstOrDefault( elem => GUILayout.Button( elem.item as Texture2D, options ) );

                if( pressedItem.item != null ){
                    return pressedItem.index;
                }
            }

            if( typeof( string ).IsAssignableFrom( typeof( T ) ) ){
                var pressedItem = query.FirstOrDefault( elem => GUILayout.Button( elem.item as string, options ) );

                if( pressedItem.item != null ){
                    return pressedItem.index;
                }
            }

            if( unselected ){
                return -1;
            } else {
                return -2;
            }
        }

        public int RenderButton<T>( IEnumerable<T> items, bool useUnselect ) {
            var width = 60;
            var height = 40;
            var space = 10;
            var column = 8;

            var unselected = false;
            if( useUnselect ){
                unselected = GUI.Button( CreateRectForButton( 0, width, height, space, column ), "選択解除" );
            }
            
            var query = items.Select( (item, index)=> (item, index) );

            if( typeof( Texture2D ).IsAssignableFrom( typeof( T ) ) ){
                var offset = useUnselect ? 1 : 0;
                var pressedItem = query.FirstOrDefault( elem => GUI.Button( CreateRectForButton( elem.index+offset, width, height, space, column ), elem.item as Texture2D ) );

                if( pressedItem.item != null ){
                    return pressedItem.index;
                }
            }

            if( typeof( string ).IsAssignableFrom( typeof( T ) ) ){
                var offset = useUnselect ? 1 : 0;
                var pressedItem = query.FirstOrDefault( elem => GUI.Button( CreateRectForButton( elem.index+offset, width, height, space, column ), elem.item as string ) );

                if( pressedItem.item != null ){
                    return pressedItem.index;
                }
            }

            if( unselected ){
                return -1;
            } else {
                return -2;
            }
        }

        private Rect CreateRectForButton( int index, int width, int height, int space, int column ){
            var w = space + (index%8) * (width+space);
            var h = space + (index/8) * (height+space);

            return new Rect( w, h, width, height );
        }
    }
}