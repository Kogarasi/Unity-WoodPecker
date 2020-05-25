using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using WoodPecker.Attribute;

//
// サンプルコード
//
public class TestEditorWindow : EditorWindow {
    private static string windowTitle = "テストウィンドウ";
    private static bool isUtility = true;

    // WoodPeckerの設定
    private WoodPecker.WoodPecker woodPecker = new WoodPecker.WoodPecker( new WoodPecker.EditorUIRenderer() );

    // レイアウトデータ。WoodPecker.Interface.ILayoutを継承する
    class TestLayout: WoodPecker.Interface.ILayout {

        [LayoutOption( type = WoodPecker.Entity.LayoutOptionType.Caption )]
        public string title = "テスト";

        [LayoutOption( type = WoodPecker.Entity.LayoutOptionType.Foldable )]
        public bool isFolding = false;

        [Slider( label = "テスト", min = 0f, max = 1f )]
        public float sliderValue = 0;

        [Label( label = "あいうえお" )]
        public float floatData = 0;

        [Label( label = "Vector2" )]
        public Vector2 vec2Data = Vector2.zero;

        [Label( label = "Vector3" )]
        public Vector3 vec3Data = Vector3.zero;

        [Label( label = "Vector4" )]
        public Vector4 vec4Data = Vector4.zero;

        [Label( label = "Color" )]
        public Color colorData = Color.white;
    }

    private TestLayout test = new TestLayout();

    void OnGUI(){
        // WoodPeckerでレンダリング
        woodPecker.Render( test );
    }

    [MenuItem( "Window/Wood Pecker テスト表示" )]
    public static void CreateWindow() => EditorWindow.GetWindow<TestEditorWindow>( utility: isUtility, title: windowTitle );
}
