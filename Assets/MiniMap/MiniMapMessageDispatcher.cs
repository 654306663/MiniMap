using System;
using System.Collections.Generic;
using UnityEngine;


public class MiniMapMessageDispatcher
{
    private static Dictionary<MiniMapElementStateType, Action<int, Vector3>> elementGameObjectActionDict = new Dictionary<MiniMapElementStateType, Action<int, Vector3>>();

    private static Dictionary<MiniMapElementStateType, Action<int, MiniMapElementIconType>> elementDataActionDict = new Dictionary<MiniMapElementStateType, Action<int, MiniMapElementIconType>>();


    public static void AddElementObserver( MiniMapElementStateType stateType, Action<int, Vector3> action )
    {
        if ( !elementGameObjectActionDict.ContainsKey( stateType ) )
        {
            elementGameObjectActionDict[stateType] = action;
        }
        else
        {
            if ( elementGameObjectActionDict[stateType] != null )
            {
                Delegate[] dels = elementGameObjectActionDict[stateType].GetInvocationList();
                foreach ( Delegate del in dels )
                {
                    if ( del.Equals( action ) )
                        return;
                }
            }

            elementGameObjectActionDict[stateType] += action;
        }
    }

    public static void AddElementObserver( MiniMapElementStateType stateType, Action<int, MiniMapElementIconType> action )
    {
        if ( !elementDataActionDict.ContainsKey( stateType ) )
        {
            elementDataActionDict[stateType] = action;
        }
        else
        {
            if ( elementDataActionDict[stateType] != null )
            {
                Delegate[] dels = elementDataActionDict[stateType].GetInvocationList();
                foreach ( Delegate del in dels )
                {
                    if ( del.Equals( action ) )
                        return;
                }
            }

            elementDataActionDict[stateType] += action;
        }
    }

    public static void RemoveElementObserver( MiniMapElementStateType stateType, Action<int, Vector3> action )
    {
        if ( elementGameObjectActionDict.ContainsKey( stateType ) )
        {
            elementGameObjectActionDict[stateType] -= action;

            if ( elementGameObjectActionDict[stateType] == null )
            {
                elementGameObjectActionDict.Remove( stateType );
            }
        }
    }

    public static void RemoveElementObserver( MiniMapElementStateType stateType, Action<int, MiniMapElementIconType> action )
    {
        if ( elementDataActionDict.ContainsKey( stateType ) )
        {
            elementDataActionDict[stateType] -= action;

            if ( elementDataActionDict[stateType] == null )
            {
                elementDataActionDict.Remove( stateType );
            }
        }
    }

    public static void PostElementMessage( MiniMapElementStateType stateType, int id, Vector3 pos )
    {
        if ( elementGameObjectActionDict.ContainsKey( stateType ) )
        {
            elementGameObjectActionDict[stateType]( id , pos);
        }
    }

    public static void PostElementMessage( MiniMapElementStateType stateType, int id, MiniMapElementIconType iconType = MiniMapElementIconType.Empty )
    {
        if ( elementDataActionDict.ContainsKey( stateType ) )
        {
            elementDataActionDict[stateType]( id, iconType );
        }
    }
}