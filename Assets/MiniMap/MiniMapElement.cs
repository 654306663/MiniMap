using UnityEngine;
using System.Collections;

public class MiniMapElement : MonoBehaviour {

    [HideInInspector]
    public int id;

    MiniMapElementIconType iconType = MiniMapElementIconType.Empty;

    Vector3 lastPostion = Vector3.zero;

    public void Init( MiniMapElementIconType iconType)
    {
        id = MiniMapController.MakeId();
        this.iconType = iconType;
        MiniMapMessageDispatcher.PostElementMessage( MiniMapElementStateType.Create, id, iconType );
    }

    void OnEnable()
    {
        if ( iconType != MiniMapElementIconType.Empty )
        {
            MiniMapMessageDispatcher.PostElementMessage( MiniMapElementStateType.Create, id, iconType );
        }
    }

    public void UpdateIcon( MiniMapElementIconType iconType )
    {
        this.iconType = iconType;
        MiniMapMessageDispatcher.PostElementMessage( MiniMapElementStateType.Update, id, iconType );
    }

    void LateUpdate()
    {
        if ( MiniMapView.Instance.elementIconDict.ContainsKey(id) && lastPostion != transform.position )
        {
            lastPostion = transform.position;
            MiniMapMessageDispatcher.PostElementMessage( MiniMapElementStateType.Update, id, transform.position );
        }
    }

    void OnDisable()
    {
        lastPostion = Vector2.zero;
        MiniMapMessageDispatcher.PostElementMessage( MiniMapElementStateType.Destroy, id, MiniMapView.Instance.controller.elementDataDict[id] );
    }
}

