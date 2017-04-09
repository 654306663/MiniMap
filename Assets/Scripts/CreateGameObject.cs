using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateGameObject : MonoBehaviour
{

    public Transform goParent;

    public GameObject soldierGo;

    public GameObject towerGo;


    int effectId = 0;

    bool b = false;

    // Use this for initialization
    void Start()
    {

    }

    GameObject lastGo;
    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown( KeyCode.Alpha1 ) )
        {
            GameObject go = Instantiate( soldierGo ) as GameObject;
            go.transform.parent = goParent;
            go.transform.localPosition = new Vector3( Random.Range( -15, 15 ), 0, Random.Range( -15, 15 ) );
            lastGo = go;
        }

        if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
        {
            GameObject go = Instantiate( towerGo ) as GameObject;
            go.transform.parent = goParent;
            go.transform.localPosition = new Vector3( Random.Range( -15, 15 ), 0, Random.Range( -15, 15 ) );
            lastGo = go;
        }

        if ( Input.GetKeyDown( KeyCode.Z ) )
        {
            effectId = MiniMapController.MakeId();
            MiniMapMessageDispatcher.PostElementMessage( MiniMapElementStateType.Create, effectId, MiniMapElementIconType.Effect1 );
            MiniMapMessageDispatcher.PostElementMessage( MiniMapElementStateType.Update, effectId, lastGo.transform.position );
            b = true;
        }

        if ( Input.GetKeyDown( KeyCode.X ) )
        {
            MiniMapMessageDispatcher.PostElementMessage( MiniMapElementStateType.Destroy, effectId );
            effectId = 0;
        }

        if ( Input.GetKeyDown( KeyCode.C ) )
        {
            MiniMapMessageDispatcher.PostElementMessage( MiniMapElementStateType.Update, effectId, MiniMapElementIconType.Effect2 );
        }

        if ( Input.GetKeyDown( KeyCode.U ) )
        {
            lastGo.GetComponent<MiniMapElement>().enabled = !lastGo.GetComponent<MiniMapElement>().enabled;
        }
    }
}
