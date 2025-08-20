using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class LayerHelper
{
    public const string GroundLayer = "Ground";
    public const string PlayerLayer = "Player";
    public const string PlayerRendererLayer = "PlayerRenderer";
    public const string Wall = "Wall";
    public const string InteractionLayer = "Interaction";
    public const string MonsterLayer = "Monster";
    public const string AStarNode = "AStarNode";

    public static int OriginLayerValue(string _LayerName)
    {
        return LayerMask.NameToLayer(_LayerName);
    }

    public static int GetLayer(string _LayerName)
    {
        return 1 << LayerMask.NameToLayer(_LayerName);
    }
}
