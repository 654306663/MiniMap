using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MiniMapView : MonoBehaviour
{
    private static MiniMapView instance;
    public static MiniMapView Instance
    {
        get
        {
            return instance;
        }
    }

    public MiniMapController controller;

    private MiniMapPoolManager poolManager;

    [HideInInspector]
    public Transform elementIconParent;

    public Dictionary<int, Transform> elementIconDict;

    public Vector3 mapSize = new Vector3( 30f, 1, 30f );

    public Vector2 miniMapSize;

    void Awake()
    {
        instance = this;

        controller = new MiniMapController();

        poolManager = GetComponent<MiniMapPoolManager>();

        miniMapSize = transform.GetComponent<RectTransform>().sizeDelta;

        elementIconDict = new Dictionary<int, Transform>();

        elementIconParent = transform.FindChild( "Mask/ElementParent" ).transform;

        controller.OnCreate();

    }

    public void CreateElementIcon( int id, MiniMapElementIconType iconType )
    {
        GameObject elementIcon = null;
        elementIcon = poolManager.TakeObject( iconType );
        elementIcon.SetActive( true );
        elementIconDict.Add( id, elementIcon.transform );
    }

    public void UpdateElementIcon( int id, MiniMapElementIconType iconType )
    {
        if ( !elementIconDict.ContainsKey( id ) )
        {
            return;
        }

        GameObject newIcon = null;
        newIcon = poolManager.TakeObject( iconType );
        newIcon.transform.localPosition = elementIconDict[id].localPosition;
        newIcon.SetActive( true );

        poolManager.ReleaseObject( controller.elementDataDict[id], elementIconDict[id].gameObject );
        controller.elementDataDict[id] = iconType;
        elementIconDict[id] = newIcon.transform;
    }

    public void DestroyElementIcon( int id )
    {
        if ( elementIconDict.ContainsKey( id ) )
        {
            poolManager.ReleaseObject( controller.elementDataDict[id], elementIconDict[id].gameObject );
            elementIconDict.Remove( id );
        }
    }

    public void MoveElementIcon( int id, Vector2 vec )
    {
        if ( elementIconDict.ContainsKey( id ) )
        {
            SetElementIconPosition( elementIconDict[id], vec );
        }
    }

    private void SetElementIconPosition( Transform elementIcon, Vector2 vec )
    {
        elementIcon.localPosition = new Vector2( miniMapSize.x * vec.x / mapSize.x, miniMapSize.y * vec.y / mapSize.z );
    }


    void Destroy()
    {
        controller.OnDestroy();
    }
}
