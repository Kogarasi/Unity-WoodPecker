namespace WoodPecker.Attribute {

    //!
    //! @brief レイアウトクラスの表示オプション
    //!
    public class LayoutOptionAttribute: System.Attribute {
        public Entity.LayoutOptionType type { get; set; }
    }
}