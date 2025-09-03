using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public static SwipeController Instance => instance;
    private static SwipeController instance;

    public Vector2 SwipeLastFrameValue => twoFramesSwipeValue;
    public Vector2 SwipeCurrentFullValue => valueOfCurrentSwipe;
    public bool Swiping => swiping;

    private BaseState currentState;

    public StateNotSwping stateNotSwping = new StateNotSwping();
    public StateSwiping stateSwiping = new StateSwiping();

    private Vector2 twoFramesSwipeValue;
    private Vector2 valueOfCurrentSwipe = new Vector2(0f, 0f);

    private Vector2 previousCursorPos;
    private Vector2 currentCursorPos;

    private bool swiping;

    private void Awake()
    {
        currentState = stateNotSwping;

        if(!ReferenceEquals(SwipeController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        currentState.RunUpdate(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.IsPressed(), 
            InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(), ref previousCursorPos, ref currentCursorPos, ref twoFramesSwipeValue, ref valueOfCurrentSwipe, 
            ref swiping, this);
    }

    #region StateMachine
    public abstract class BaseState
    {
        public abstract void RunUpdate(bool inputIsPressed, Vector2 inputPos, ref Vector2 previousCursorPos, ref Vector2 currentCursorPos, ref Vector2 twoFramesSwipeValue, ref Vector2 valueOfCurrentSwipe, 
            ref bool swiping, SwipeController stateMachine); 
    }

    public class StateNotSwping : BaseState
    {
        public override void RunUpdate(bool inputIsPressed, Vector2 inputPos, ref Vector2 previousCursorPos, ref Vector2 currentCursorPos, ref Vector2 twoFramesSwipeValue, ref Vector2 valueOfCurrentSwipe, 
            ref bool swiping, SwipeController stateMachine)
        {
            if(inputIsPressed)
            {
                swiping = true;
                previousCursorPos = inputPos;
                stateMachine.SwitchState(stateMachine.stateSwiping);
            }
        }
    }

    public class StateSwiping : BaseState
    {
        public override void RunUpdate(bool inputIsPressed, Vector2 inputPos, ref Vector2 previousCursorPos, ref Vector2 currentCursorPos, ref Vector2 twoFramesSwipeValue, ref Vector2 valueOfCurrentSwipe, 
            ref bool swiping, SwipeController stateMachine)
        {
            #region Swiping
            currentCursorPos = inputPos;

            twoFramesSwipeValue = new Vector2(Mathf.Abs(currentCursorPos.x - previousCursorPos.x), Mathf.Abs(currentCursorPos.y - previousCursorPos.y));
            if (currentCursorPos.x > previousCursorPos.x) twoFramesSwipeValue = new Vector2(-twoFramesSwipeValue.x, twoFramesSwipeValue.y);
            if (currentCursorPos.y > previousCursorPos.y) twoFramesSwipeValue = new Vector2(twoFramesSwipeValue.x, -twoFramesSwipeValue.y);

            valueOfCurrentSwipe = new Vector2(valueOfCurrentSwipe.x + twoFramesSwipeValue.x, valueOfCurrentSwipe.y + twoFramesSwipeValue.y);

            previousCursorPos = currentCursorPos;
            #endregion

            #region BreakSwipe
            if (!inputIsPressed)
            {
                swiping = false;
                valueOfCurrentSwipe = new Vector2(0f, 0f);

                stateMachine.SwitchState(stateMachine.stateNotSwping);
            }
            #endregion
        }
    }

    public void SwitchState(BaseState newState)
    {
        currentState = newState;
    }
    #endregion
}

