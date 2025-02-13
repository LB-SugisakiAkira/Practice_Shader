using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class AvatarMakeSceneManager : MonoBehaviour
{
    [SerializeField] private Button colorChangeButton;
    [SerializeField] private GameObject Avatar;
    // Start is called before the first frame update
    void Start()
    {
        colorChangeButton.OnClickAsObservable()
    .ThrottleFirst(TimeSpan.FromSeconds(1))
    .Subscribe(_ => { PressColorChange(); });
    }
    private void PressColorChange()
    {
        Avatar.GetComponent<Renderer>().material.color = Color.red;
    }



}
