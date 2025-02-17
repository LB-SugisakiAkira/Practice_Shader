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

    [SerializeField] private Button colorChangeButton;
    [SerializeField] private ColorPickerPanel colorPickerPanel;

    void Start()
    {
        colorChangeButton.OnClickAsObservable()
        .ThrottleFirst(TimeSpan.FromSeconds(1))
        .Subscribe(_ => { PressColorChange(); });
    }
    private void PressColorChange()
    {
        // Avatar.GetComponent<Renderer>().material.color = new Color32(248, 168, 133, 1);
        colorPickerPanel.gameObject.SetActive(true);
    }

    public void OnAfterCloseColorSelectPanel(Color selectedColor)
    {
        Debug.Log("選択された色 (RGB): " + selectedColor.r + ", " + selectedColor.g + ", " + selectedColor.b);
        Avatar.GetComponent<Renderer>().material.color = selectedColor;
    }
}
