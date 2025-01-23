using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandle;
    [SerializeField] Color bgActiveColor;
    [SerializeField] Color handleActiveColor;

    Image bgImage, handleImage;

    Color bgDefaultColor, handleDefaultColor;

    Toggle toggle;
    Vector2 handlePosition;

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        handlePosition = uiHandle.anchoredPosition;
        bgImage = uiHandle.parent.GetComponent<Image>();
        handleImage = uiHandle.GetComponent<Image>();

        bgDefaultColor = bgImage.color;
        handleDefaultColor = handleImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
            OnSwitch(true);
    }

    void OnSwitch(bool on)
    {
        uiHandle.anchoredPosition = on ? handlePosition * -1 : handlePosition;
        bgImage.color = on ? bgActiveColor : bgDefaultColor;
        handleImage.color = on ? handleActiveColor : handleDefaultColor;
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }

}
