using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MiniMapCameraManager : MonoBehaviour, IDragHandler, IPointerDownHandler{
    
    private Transform point;

    private Vector3 cameraOffset;

    private Vector2 pointOffset;

    private Vector3 mapSize;

    private Vector2 miniMapSize;

    private PolygonCollider2D polygonCollider2D;

    void Start()
    {
        point = transform.FindChild( "Mask/Bg/Point" );
        polygonCollider2D = transform.FindChild( "Mask/Bg" ).GetComponent<PolygonCollider2D>();

        mapSize = MiniMapView.Instance.mapSize;
        miniMapSize = MiniMapView.Instance.miniMapSize;
        
        cameraOffset = Camera.main.transform.position;

        InitPointPosition( cameraOffset );
    }

    public void OnPointerDown( PointerEventData eventData )
    {
        if ( !polygonCollider2D.OverlapPoint( eventData.position ) )
        {
            return;
        }

        point.position = eventData.position;

        SetCameraPosition( point.transform.localPosition );
    }

    public void OnDrag( PointerEventData eventData )
    {
        if ( !polygonCollider2D.OverlapPoint( eventData.position ) )
        {
            return;
        }

        if (Vector2.Distance(point.position, eventData.position) < 10)
        {
            return;
        }

        point.position = eventData.position;

        SetCameraPosition( point.transform.localPosition );
    }

    Tweener tweener;
    void SetCameraPosition(Vector2 vec)
    {
        vec = vec - pointOffset;
        Vector3 targetPosition = new Vector3( mapSize.x * vec.x / miniMapSize.x, 0, mapSize.z * vec.y / miniMapSize.y ) + cameraOffset;
        if ( tweener != null && tweener.IsPlaying() )
        {
            tweener.Kill( false );
        }

        tweener = DOTween.To( () => Camera.main.transform.position, value => Camera.main.transform.position = value, targetPosition, 0.3f ).SetEase(Ease.OutQuad);

    }

    void InitPointPosition( Vector3 vec )
    {
        Vector3 position = Vector3.zero;

        RaycastHit hit;
        Vector2 v = new Vector2( Screen.width / 2, Screen.height / 2 );
        if ( Physics.Raycast( Camera.main.ScreenPointToRay( v ), out hit ) )
        {
            position = hit.point;
        }

        Vector2 targetPosition = new Vector2( position.x * miniMapSize.x / mapSize.x, position.z * miniMapSize.y / mapSize.z );
        point.localPosition = targetPosition;
        pointOffset = targetPosition;
    }

}
