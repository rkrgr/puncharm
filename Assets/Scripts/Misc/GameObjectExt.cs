using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper.GameObjectExt
{
    public static class GameObjectExt{

    
        public static bool IsInLayer(this GameObject gameObject, string layer)
        {
            return gameObject.layer == LayerMask.NameToLayer(layer);
        }
    }
}

