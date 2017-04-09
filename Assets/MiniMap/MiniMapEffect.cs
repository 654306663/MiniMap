using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MiniMapEffect : MonoBehaviour {

    Tweener tweener;

    Vector3 targetScale = Vector3.one * 1.5f;
    
	void OnEnable()
    {
        tweener = transform.DOScale( targetScale, 0.2f );
        tweener.SetLoops( -1, LoopType.Yoyo );
    }


    void OnDisable()
    {
        tweener.Kill( false );
        transform.localScale = Vector3.one;
    }
}
