using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReturnObjectDataForTooltip
{
    public TooltipParagraph[] ReturnTooltipText(GameLanguage gameLanguage);
}
