using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoLayout.Interface {

    //!
    //! @brief リスト系のレンダリング用
    //!
    public interface ListElement: IEnumerable {
        void OnRender();
    }
}