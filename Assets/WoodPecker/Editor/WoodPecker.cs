using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WoodPecker {
    public class WoodPecker {
        private readonly Interface.IRenderer renderer;

        public WoodPecker( Interface.IRenderer renderer ){
            this.renderer = renderer;
        }

        public void Render( params Interface.ILayout[] layouts ){
            layouts.ToList().ForEach( layout => {
                layout.RenderContent( renderer );
            });
        }
    }
}