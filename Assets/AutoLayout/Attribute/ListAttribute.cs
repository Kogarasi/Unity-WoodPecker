using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoLayout.Attribute {

    //!
    //! @brief リスト表示
    //!
    public class ListAttribute : System.Attribute {

        public string label;
        public bool useUnselect;
        public int columnSize;

        public ListAttribute( string label, bool useUnselect = false, int columnSize = -1 ){
            this.label = label;
            this.useUnselect = useUnselect;
            this.columnSize = columnSize;
        }
    }
}