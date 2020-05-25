using UnityEngine;

namespace WoodPecker.Interface {

    //!
    //! @brief レンダリング周りのためのインターフェース
    //!
    public interface IRenderer {

        //!
        //! @brief ラベル
        //!
        void Label( string text );

        //!
        //! @brief Float
        //!
        float FloatField( float previous, string label );

        //!
        //! @brief Int
        //!
        int IntField( int previous, string label );

        //!
        //! @brief bool
        //!
        bool BoolField( bool previous, string label );

        //!
        //! @brief string
        //!
        string TextField( string previous, string label );

        //!
        //! @brief Vector2
        //!
        Vector2 VectorField( Vector2 previous, string label );

        //!
        //! @brief Vector3
        //!
        Vector3 VectorField( Vector3 previous, string label );

        //!
        //! @brief Vector4
        //!
        Vector4 VectorField( Vector4 previous, string label );

        //!
        //! @brief Color
        //!
        Color ColorField( Color previous, string label );

        //!
        //! @brief 折りたたみラベル
        //!
        bool FoldLabel( string text, bool expand );

        //!
        //! @brief スライダー float
        //!
        float Slider( float previous, Attribute.SliderAttribute attribute );

        //!
        //! @brief 入れ子を追加（Push
        //!
        void PushOrientation();
        
        //!
        //! @brief 入れ子を削除（Pop
        //!
        void PopOrientation();
    }
}