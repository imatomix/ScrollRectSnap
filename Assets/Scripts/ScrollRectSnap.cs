using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollRectSnap : ScrollRect {
	public int horizontalPages = 3; // horizontalの分割数
	public int verticalPages = 3;  // verticalの分割数

	[SerializeField] private float smooth = 10f;  // スナップ係数
	private Vector2 targetPosition; // スナップ先座標
	private bool isDrag = false; // フラグ

	protected override void Start(){
		base.Start ();
		normalizedPosition = content.GetComponent<RectTransform> ().pivot;
		targetPosition = FindSnapPosition ();
	}

	void Update(){
		if (!isDrag && normalizedPosition != targetPosition) {
			normalizedPosition = Vector2.Lerp (normalizedPosition, targetPosition, smooth * Time.deltaTime);
		}
	}

	public override void OnBeginDrag(PointerEventData eventData){
		base.OnBeginDrag (eventData);
		isDrag = true;
	}

	public override void OnEndDrag(PointerEventData eventData){
		base.OnEndDrag (eventData);
		targetPosition = FindSnapPosition ();
		isDrag = false;
	}
		
	// スナップ先座標を取得する
	Vector2 FindSnapPosition(){
		float x = 0, y = 0;
		Vector2 center;

		if(horizontal){
			if(horizontalPages > 1){
				for (int page = 0; page < horizontalPages; page ++) {
					center.x = (2f * page - 1f) / ((horizontalPages - 1f) * 2f);
					if(horizontalNormalizedPosition >= center.x){
						x = page / (horizontalPages - 1f);
					}
				}
			}
		}else{
			x = horizontalNormalizedPosition;
		}

		if(vertical){
			if(verticalPages > 1){
				for (int page = 0; page < verticalPages; page ++) {
					center.y = (2f * page - 1f) / ((verticalPages - 1f) * 2f);
					if(verticalNormalizedPosition >= center.y){
						y = page / (verticalPages - 1f) ;
					}
				}
			}
		}else{
			y = verticalNormalizedPosition;
		}
		return new Vector2 (x, y);
	}
}