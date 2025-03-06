using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class AvatarMakeSceneManager : MonoBehaviour
{
    public static AvatarMakeSceneManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            Debug.Log(this.gameObject.name + " has made");
        }
        else
        {
            Destroy(this.gameObject);
            Debug.Log(this.gameObject.name + " is already exist");
        }
    }

    [SerializeField] private GameObject Avatar;
    [SerializeField] private MeshRenderer AvatarMeshRenderer;

    [SerializeField] private Button colorChangeButton1;
    [SerializeField] private Button colorChangeButton2;
    [SerializeField] private ColorPickerPanel colorPickerPanel;

    void Start()
    {
        colorChangeButton1.OnClickAsObservable()
        .ThrottleFirst(TimeSpan.FromSeconds(1))
        .Subscribe(_ => { PressColorChange(1); });
        colorChangeButton2.OnClickAsObservable()
        .ThrottleFirst(TimeSpan.FromSeconds(1))
        .Subscribe(_ => { PressColorChange(2); });
    }
    private void PressColorChange(int colorIndex)
    {
        // Avatar.GetComponent<Renderer>().material.color = new Color32(248, 168, 133, 1);
        colorPickerPanel.gameObject.SetActive(true);
        colorPickerPanel.SetColorIndex(colorIndex);
    }

    public void OnAfterCloseColorSelectPanel(Color selectedColor, int colorIndex)
    {
        // Avatar.GetComponent<Renderer>().material.color = selectedColor;
        string propertyName = colorIndex == 1 ? "_Color1" : "_Color2";
        AvatarMeshRenderer.material.SetColor(propertyName, selectedColor);
    }
}
