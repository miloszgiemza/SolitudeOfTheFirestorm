using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    protected Button button;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
    }

    protected void OnEnable()
    {
        button.onClick.AddListener(DoThisOnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(DoThisOnClick);
    }

    protected abstract void DoThisOnClick();
}
