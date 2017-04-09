using UnityEngine;
using System.Collections;

public class TowerMoveController : MonoBehaviour {

    void Awake()
    {
        MiniMapElement miniMapElement = gameObject.AddComponent<MiniMapElement>();
        if ( Random.Range( 1, 10 ) < 5 )
        {
            miniMapElement.Init( MiniMapElementIconType.Owner_Tower );
        }
        else
        {
            miniMapElement.Init( MiniMapElementIconType.Enemy_Tower );
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
