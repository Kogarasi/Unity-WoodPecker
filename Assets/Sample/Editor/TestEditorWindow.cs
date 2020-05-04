using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestEditorWindow : EditorWindow {
    private static string windowTitle = "テストウィンドウ";
    private static bool isUtility = true;

    private WoodPecker.WoodPecker woodPecker = new WoodPecker.WoodPecker( new WoodPecker.EditorUIRenderer() );

    class TestLayout: WoodPecker.Interface.ILayout {

        [WoodPecker.Attribute.LayoutOption( type = WoodPecker.Entity.LayoutOptionType.Caption )]
        public string title = "テスト";

        [WoodPecker.Attribute.LayoutOption( type = WoodPecker.Entity.LayoutOptionType.Foldable )]
        public bool isFolding = true;
    }

    private TestLayout test = new TestLayout();

    void OnGUI(){
        EditorGUILayout.LabelField( "Hello World" );

        woodPecker.Render( test );
    }

    [MenuItem( "Window/Wood Pecker テスト表示" )]
    public static void CreateWindow() => EditorWindow.GetWindow<TestEditorWindow>( utility: isUtility, title: windowTitle );
}
