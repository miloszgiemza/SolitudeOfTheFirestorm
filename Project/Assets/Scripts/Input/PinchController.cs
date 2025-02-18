using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchController : MonoBehaviour
{
    public static PinchController Instance => instance;
    private static PinchController instance;

    public bool Pinching => pinching;
    public double PinchValueTwoFrames => pinchValueTwoFrames;
    public float MinimalPinchDistance => minimalPinchDistance;

    private BaseState currentState;

    public StateNotPinching stateNotPinching = new StateNotPinching();
    public StatePinching statePinching = new StatePinching();

    private bool pinching = false;
    private double pinchValueTwoFrames = 0;

    private Vector2 previousCursor1Pos;
    private Vector2 currentCursor1Pos;
    private Vector2 previousCursor2Pos;
    private Vector2 currentCursor2Pos;

    private double previousFrameTouchesDistance;
    private double currentFrameTouchesDistance;

    private float minimalPinchDistance = 1f;

    private void Awake()
    {
        if(!ReferenceEquals(PinchController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;

            currentState = stateNotPinching;
        }
    }

    private void Update()
    {
        currentState.RunUpdate(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.IsPressed(), InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.SecondaryTouch.IsPressed(),
            InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainActionPosition.ReadValue<Vector2>(), 
            InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.SecondaryTouchPosition.ReadValue<Vector2>(), ref previousCursor1Pos, ref currentCursor1Pos, ref previousCursor2Pos, ref currentCursor2Pos, 
            ref previousFrameTouchesDistance, ref currentFrameTouchesDistance, ref pinching, ref pinchValueTwoFrames, this);
    }

    #region StateMachine
    public abstract class BaseState
    {
        public abstract void RunUpdate(bool primaryTouchIsPressed, bool secondaryTouchIsPressed, Vector2 primaryTouchPosition, Vector2 secondaryTouchPosition, ref Vector2 previousCursor1Pos, 
            ref Vector2 currentCursor1Pos, ref Vector2 previousCursor2Pos, ref Vector2 currentCursor2Pos, ref double previousFrameTouchesDistance, ref double currentFrameTouchesDistance, ref bool pinching, 
            ref double pinchValueTwoFrames, PinchController stateMachine);
    }

    public class StateNotPinching : BaseState
    {
        public override void RunUpdate(bool primaryTouchIsPressed, bool secondaryTouchIsPressed, Vector2 primaryTouchPosition, Vector2 secondaryTouchPosition, ref Vector2 previousCursor1Pos, 
            ref Vector2 currentCursor1Pos, ref Vector2 previousCursor2Pos, ref Vector2 currentCursor2Pos, ref double previousFrameTouchesDistance, ref double currentFrameTouchesDistance, ref bool pinching, 
            ref double pinchValueTwoFrames, PinchController stateMachine)
        {
            if (primaryTouchIsPressed && secondaryTouchIsPressed)
            {
                previousCursor1Pos = primaryTouchPosition;
                previousCursor2Pos = secondaryTouchPosition;

                previousFrameTouchesDistance = stateMachine.CalculateDistance(previousCursor1Pos, previousCursor2Pos);

                stateMachine.SwitchState(stateMachine.statePinching);
            }
        }
    }

    public class StatePinching : BaseState
    {
        public override void RunUpdate(bool primaryTouchIsPressed, bool secondaryTouchIsPressed, Vector2 primaryTouchPosition, Vector2 secondaryTouchPosition, ref Vector2 previousCursor1Pos, 
            ref Vector2 currentCursor1Pos, ref Vector2 previousCursor2Pos, ref Vector2 currentCursor2Pos, ref double previousFrameTouchesDistance, ref double currentFrameTouchesDistance, ref bool pinching, 
            ref double pinchValueTwoFrames, PinchController stateMachine)
        {
            currentCursor1Pos = primaryTouchPosition;
            currentCursor2Pos = secondaryTouchPosition;

            currentFrameTouchesDistance = stateMachine.CalculateDistance(currentCursor1Pos, currentCursor2Pos);

            pinchValueTwoFrames = Math.Abs(currentFrameTouchesDistance - previousFrameTouchesDistance);

            if (previousFrameTouchesDistance > currentFrameTouchesDistance) pinchValueTwoFrames *= -1;

            previousCursor1Pos = currentCursor1Pos;
            previousCursor2Pos = currentCursor2Pos;

            Debug.Log("pinch value: " + pinchValueTwoFrames);

            if (Math.Abs(pinchValueTwoFrames) < stateMachine.MinimalPinchDistance) pinching = false;
            else pinching = true;

            previousFrameTouchesDistance = stateMachine.CalculateDistance(previousCursor1Pos, previousCursor2Pos);

            if (primaryTouchIsPressed == false || secondaryTouchIsPressed == false)
            {
                pinching = false;
                pinchValueTwoFrames = 0f;

                stateMachine.SwitchState(stateMachine.stateNotPinching);
            }
        }
    }

    public void SwitchState(BaseState newState)
    {
        currentState = newState;
    }
    #endregion

    private double CalculateDistance(Vector2 point1Position, Vector2 point2Position)
    {
        return Math.Abs(Math.Sqrt(Math.Pow((point2Position.x - point1Position.x), 2) + Math.Pow((point2Position.y - point1Position.y), 2))) ;
    }
}
