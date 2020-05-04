using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoLayout.Attribute {

    //!
    //! アイテムの並べる方向を設定
    //!
    public class StackAttribute: System.Attribute {
        public enum Orientation {
            Vertical,
            Horizontal
        }

        public Orientation orientation;
        public bool isBoxed;

        //!
        //! @brief コンストラクタ
        //!
        public StackAttribute( Orientation orientation = Orientation.Vertical, bool isBoxed = false ){
            this.orientation = orientation;
            this.isBoxed = isBoxed;
        }
    }
}