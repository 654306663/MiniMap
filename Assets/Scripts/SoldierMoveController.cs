using UnityEngine;
using System.Collections;

public class SoldierMoveController : MonoBehaviour {
    
    Rigidbody r;

    MiniMapElement miniMapElement;

    void Awake()
    {
        miniMapElement = gameObject.AddComponent<MiniMapElement>();

        if ( Random.Range( 1, 10 ) < 5 )
        {
            miniMapElement.Init( MiniMapElementIconType.Owner_Soldier );
        }
        else
        {
            miniMapElement.Init( MiniMapElementIconType.Enemy_Soldier );
        }
    }

    // Use this for initialization
    void Start () {
        r = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {

        if ( miniMapElement.id > 1 )
        {
            float horizontal = Input.GetAxis( "Horizontal" );
            float vertical = Input.GetAxis( "Vertical" );

            transform.Translate( new Vector3( horizontal, 0, vertical ) * Time.deltaTime * 10 );
        }

        
	}
}
