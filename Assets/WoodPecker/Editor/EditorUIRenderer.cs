using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WoodPecker {

    //!
    //! @brief EditorGUIを利用したレンダラー
    //!
    public class EditorUIRenderer: Interface.IRenderer {
        
        //!
        //! @brief ラベル
        //!
        public void Label( string text ){
            EditorGUILayout.LabelField( text );
        }
        
        //!
        //! @brief Float
        //!
        public float FloatField( float previous , string label ){
            return EditorGUILayout.FloatField( label, previous );
        }

        //!
        //! @brief Int
        //!
        public int IntField( int previous, string label ){
            return EditorGUILayout.IntField( label, previous );
        }

        //!
        //! @brief Bool
        //!
        public bool BoolField( bool previous, string label ){
            return EditorGUILayout.Toggle( label, previous );
        }

        //!
        //! @brief String
        //!
        public string TextField( string previous, string label ){
            return EditorGUILayout.TextField( label: label, text: previous );
        }

        //!
        //! @brief Vector2
        //!
        public Vector2 VectorField( Vector2 previous, string label ){
            return EditorGUILayout.Vector2Field( label, previous );
        }

        //!
        //! @brief Vector3
        //!
        public Vector3 VectorField( Vector3 previous, string label ){
            return EditorGUILayout.Vector3Field( label, previous );
        }

        //!
        //! @brief Vector4
        //!
        public Vector4 VectorField( Vector4 previous, string label ){
            return EditorGUILayout.Vector4Field( label, previous );
        }

        //!
        //! @breif Color
        //!
        public Color ColorField( Color previous, string label ){
            return EditorGUILayout.ColorField( label, previous );
        }

        //!
        //! @brief 折りたたみラベルの表示
        //!
        public bool FoldLabel( string text, bool expand ){
            return EditorGUILayout.Foldout( expand, text );
        }

        //!
        //! @brief スライダー float
        //!
        public float Slider( float previous, Attribute.SliderAttribute attribute ){
            return EditorGUILayout.Slider( value: previous, label: attribute.label, leftValue: attribute.min, rightValue: attribute.max );
        }

        //!
        //! @brief 入れ子を追加（Push
        //!
        public void PushOrientation(){
            EditorGUILayout.BeginVertical( GUI.skin.box );
        }

        //!
        //! @brief 入れ子を削除（Pop
        //!
        public void PopOrientation(){
            EditorGUILayout.EndVertical();
        }
    }
}