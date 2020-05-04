namespace WoodPecker.Interface {

    //!
    //! @brief レンダリング周りのためのインターフェース
    //!
    public interface IRenderer {

        //!
        //! @brief 折りたたみラベル
        //!
        bool FoldLabel( string text, bool expand );

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