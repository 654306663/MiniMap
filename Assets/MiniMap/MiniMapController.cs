using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum MiniMapElementIconType
{
    Empty,
    Owner_Soldier,
    Owner_Tower,
    Enemy_Soldier,
    Enemy_Tower,
    Neutral_Tower,
    Effect1,
    Effect2,
    Effect3,
}

public enum MiniMapElementStateType
{
    Create,
    Update,
    Destroy
}

public class MiniMapController {

    private MiniMapView view;

    private static int elementId = 10000;

    public Dictionary<int, MiniMapElementIconType> elementDataDict;

    public void OnCreate()
    {
        view = MiniMapView.Instance;

        elementDataDict = new Dictionary<int, MiniMapElementIconType>();

        MiniMapMessageDispatcher.AddElementObserver( MiniMapElementStateType.Update, MoveElementGameObjectListener );
        
        MiniMapMessageDispatcher.AddElementObserver( MiniMapElementStateType.Create, AddElementDataListener );
        MiniMapMessageDispatcher.AddElementObserver( MiniMapElementStateType.Update, UpdateElementDataListener );
        MiniMapMessageDispatcher.AddElementObserver( MiniMapElementStateType.Destroy, RemoveElementDataListener );
   }

    private void AddElementDataListener( int id, MiniMapElementIconType iconType )
    {
        if ( !elementDataDict.ContainsKey( id ) )
        {
            elementDataDict.Add( id, iconType );
        }
        view.CreateElementIcon( id, elementDataDict[id] );
    }

    private void UpdateElementDataListener( int id, MiniMapElementIconType iconType )
    {
        if ( !elementDataDict.ContainsKey( id ) )
        {
            elementDataDict.Add( id, iconType );
        }
        else
        {
            view.UpdateElementIcon( id, iconType );
        }
    }

    private void RemoveElementDataListener( int id, MiniMapElementIconType iconType )
    {
        view.DestroyElementIcon( id );
        if ( elementDataDict.ContainsKey( id ) )
        {
            elementDataDict.Remove( id );
        }
    }
    

    private void MoveElementGameObjectListener( int id, Vector3 vec3 )
    {
        Vector2 vec2 = new Vector2( vec3.x, vec3.z );
        view.MoveElementIcon( id, vec2 );
    }

    public static int MakeId()
    {
        return elementId++;
    }

    public void OnDestroy()
    {
        MiniMapMessageDispatcher.RemoveElementObserver( MiniMapElementStateType.Update, MoveElementGameObjectListener );

        MiniMapMessageDispatcher.RemoveElementObserver( MiniMapElementStateType.Create, AddElementDataListener );
        MiniMapMessageDispatcher.RemoveElementObserver( MiniMapElementStateType.Update, UpdateElementDataListener );
        MiniMapMessageDispatcher.RemoveElementObserver( MiniMapElementStateType.Destroy, RemoveElementDataListener );

    }
}
