using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TooltipParagraph
{
    public string Title => title;
    public string Text => text;

    [SerializeField] string title;
    [SerializeField] string text;

    public TooltipParagraph()
    {
        title = "";
        text = "";
    }

    public TooltipParagraph(string title, string text)
    {
        this.title = title;
        this.text = text;
    }

    public void SetTitle(string title)
    {
        this.title = title;
    }

    public void SetText(string text)
    {
        this.text = text;
    }
}