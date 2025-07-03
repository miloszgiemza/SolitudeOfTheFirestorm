using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ActionsCounter : MonoBehaviour
{
    public static ActionsCounter Instance => instance;
    private static ActionsCounter instance;

    [SerializeField] private TextMeshProUGUI remainingMainActionsCountText;
    [SerializeField] private TextMeshProUGUI remainingSecondaryActionsCounterText;

    private void Awake()
    {
        if(ReferenceEquals(ActionsCounter.Instance, null))
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void Refresh(string remainingMaionActions, string remainingSecondaryActions)
    {
        remainingMainActionsCountText.text = remainingMaionActions;
        remainingSecondaryActionsCounterText.text = remainingSecondaryActions;
    }
}
