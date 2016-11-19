using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour {

    private Vector3 [] movements;
    public float moveDuration;

	// Use this for initialization
	void Start () {
        movements = new Vector3[] { new Vector3(0, 0, 0), new Vector3(1, -1, 0), new Vector3(2,-1.2f, 0), new Vector3(3, -1, 0), new Vector3(4, 0, 0)};
        Tweener moveTween = transform.DOLocalPath(movements, moveDuration);
        moveTween.SetLoops(-1, LoopType.Yoyo);

        moveTween.SetRelative(true);

        moveTween.SetEase(Ease.InOutCubic);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
