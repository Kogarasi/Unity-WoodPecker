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
        //! @brief 折りたたみラベルの表示
        //!
        public bool FoldLabel( string text, bool expand ){
            return EditorGUILayout.Foldout( expand, text );
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