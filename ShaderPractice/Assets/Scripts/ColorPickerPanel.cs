using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorPickerPanel : MonoBehaviour, IPointerClickHandler
{
    public RawImage colorWheelImage; // カラーサークル画像
    public Button confirmButton; // 決定ボタン
    public Image selectedColorPreview; // 選択色のプレビュー
    public RectTransform selectedColorMarker; // 選択した色の位置を示す○

    private Texture2D colorWheelTexture; // カラーサークルのテクスチャ
    private Color selectedColor; // 現在の選択色

    void Start()
    {
        colorWheelTexture = (Texture2D)colorWheelImage.texture;

        // 確認ボタンのイベント設定
        confirmButton.onClick.AddListener(ConfirmColor);

        // 初期状態で選択マーカーを非表示
        selectedColorMarker.gameObject.SetActive(false);
    }

    private int colorIndex;
    public void SetColorIndex(int colorIndex)
    {
        this.colorIndex = colorIndex;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // クリックした位置の色を取得
        Vector2 localPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(colorWheelImage.rectTransform, eventData.position, eventData.pressEventCamera, out localPosition))
        {
            Vector2 normalizedPosition = new Vector2(
                (localPosition.x / colorWheelImage.rectTransform.rect.width) + 0.5f,
                (localPosition.y / colorWheelImage.rectTransform.rect.height) + 0.5f
            );

            // マウス位置に基づいて色を取得
            int x = Mathf.FloorToInt(normalizedPosition.x * colorWheelTexture.width);
            int y = Mathf.FloorToInt(normalizedPosition.y * colorWheelTexture.height);

            selectedColor = colorWheelTexture.GetPixel(x, y);
            SetSelectedColor(selectedColor, localPosition);
        }
    }

    private void SetSelectedColor(Color color, Vector2 localPosition)
    {
        selectedColorPreview.color = color;

        // マーカーを表示して位置を設定
        selectedColorMarker.gameObject.SetActive(true);
        selectedColorMarker.anchoredPosition = localPosition; // パネル内の相対位置
    }

    private void ConfirmColor()
    {
        Debug.Log("選択されたRGB: " + selectedColor.r + ", " + selectedColor.g + ", " + selectedColor.b);
        gameObject.SetActive(false); // パネルを閉じる
        AvatarMakeSceneManager.instance.OnAfterCloseColorSelectPanel(selectedColor, this.colorIndex);
    }
}