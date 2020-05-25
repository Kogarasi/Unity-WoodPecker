using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace WoodPecker.Attribute {

    //!
    //! @brief UI要素であることの属性
    //!
    public abstract class LayoutElementAttribute: System.Attribute {

        //!
        //! @brief UIの表示処理（引数のType typeを元に表示等処理をするか継承したクラス側で実装する）
        //!
        public abstract object InvokeIfConformed( Interface.IRenderer renderer, Type type, object value );
    }
}