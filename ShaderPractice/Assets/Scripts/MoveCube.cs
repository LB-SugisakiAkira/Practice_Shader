using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCube : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    // ↓これだと左クリック以外拾えない
    // void OnMouseDrag()
    // {
    //     Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);
    //     Vector3 mousePos = new(Input.mousePosition.x, Input.mousePosition.y, objPos.z);
    //     transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    // }

    // ↓これだと左クリック以外拾えない
    // void OnMouseDown()
    // {
    //     // 左クリックの判定
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Debug.Log("左クリックされました");
    //     }
    //     // 右クリックの判定
    //     else if (Input.GetMouseButtonDown(1))
    //     {
    //         Debug.Log("右クリックされました");
    //     }
    //     // 中央ボタン（ホイールクリック）の判定
    //     else if (Input.GetMouseButtonDown(2))
    //     {
    //         Debug.Log("ホイールボタン（中央ボタン）がクリックされました");
    //     }
    // }

    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("クリック");
    }

    public void OnDrag(PointerEventData data)
    {
        switch (data.button)
        {
            case PointerEventData.InputButton.Left:
                transform.Rotate(new(0, data.delta.x / 10, 0));
                break;
            case PointerEventData.InputButton.Middle:
                // ↓これだと消える（おそらくZ座標がだめ)
                // Vector3 targetPos = Camera.main.ScreenToWorldPoint(data.position);
                // transform.position = targetPos;

                // ↓これだと消える（おそらくZ座標がだめ)
                // Vector3 targetPos = Camera.main.ScreenToWorldPoint(data.position);
                // transform.position = Camera.main.ScreenToWorldPoint(targetPos);

                Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 targetPos = new(data.position.x, data.position.y, objPos.z);
                transform.position = Camera.main.ScreenToWorldPoint(targetPos);
                break;
            case PointerEventData.InputButton.Right:
                Debug.Log("Right");
                break;
        }
    }
}
