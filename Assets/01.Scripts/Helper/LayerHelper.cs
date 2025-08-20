using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class LayerHelper
{

    public static int OriginLayerValue(string _LayerName)
    {
        return LayerMask.NameToLayer(_LayerName);
    }

    public static int GetLayer(string _LayerName)
    {
        return 1 << LayerMask.NameToLayer(_LayerName);
    }
}
