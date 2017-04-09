using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum MiniMapPoolStateType
{
    Used,
    Unused,
}

public class MiniMapPoolManager : MonoBehaviour
{ 

    private Dictionary<MiniMapElementIconType, string> prefabPathDict;

    public Dictionary<MiniMapElementIconType, List<GameObject>> unusedPoolDict;

    private int memoryCount = 3;


    void Awake()
    {
        Init();
    }

    void Init()
    {
        prefabPathDict = new Dictionary<MiniMapElementIconType, string>()
        {
            { MiniMapElementIconType.Owner_Soldier, "OwnerSoldier" },
            { MiniMapElementIconType.Owner_Tower, "OwnerTower" },
            { MiniMapElementIconType.Enemy_Soldier, "EnemySoldier" },
            { MiniMapElementIconType.Enemy_Tower, "EnemyTower" },
            { MiniMapElementIconType.Neutral_Tower, "" },
            { MiniMapElementIconType.Effect1, "Effect1" },
            { MiniMapElementIconType.Effect2, "Effect2" },
            { MiniMapElementIconType.Effect3, "" },

        };

        unusedPoolDict = new Dictionary<MiniMapElementIconType, List<GameObject>>() {
            { MiniMapElementIconType.Owner_Soldier, new List<GameObject>() },
            { MiniMapElementIconType.Owner_Tower, new List<GameObject>() },
            { MiniMapElementIconType.Enemy_Soldier, new List<GameObject>() },
            { MiniMapElementIconType.Enemy_Tower, new List<GameObject>() },
            { MiniMapElementIconType.Neutral_Tower, new List<GameObject>() },
            { MiniMapElementIconType.Effect1, new List<GameObject>() },
            { MiniMapElementIconType.Effect2, new List<GameObject>() },
            { MiniMapElementIconType.Effect3, new List<GameObject>() },
        };
    }
    
    public GameObject TakeObject( MiniMapElementIconType type )
    {
        GameObject go = null;

        if ( unusedPoolDict[type].Count > 0 )
        {
            go = unusedPoolDict[type][0];
            unusedPoolDict[type].RemoveAt( 0 );
            ResetOrder( go );
        }
        else
        {
            go = GameObject.Instantiate( Resources.Load( prefabPathDict[type] ) ) as GameObject;
            go.SetActive( false );
            go.transform.SetParent( MiniMapView.Instance.elementIconParent, false );
        }

        return go;
    }

    public void ReleaseObject( MiniMapElementIconType type, GameObject go )
    {
        go.SetActive( false );

        if ( unusedPoolDict[type].Count >= memoryCount )
        {
            GameObject.Destroy( go );
        }
        else
        {
            unusedPoolDict[type].Add( go );
        }
    }

    private void ResetOrder( GameObject go )
    {
        go.transform.SetParent( null, false );
        go.transform.SetParent( MiniMapView.Instance.elementIconParent, false );
    }
}
