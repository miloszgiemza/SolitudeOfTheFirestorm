//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/MainInputAsset.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @MainInputAssetWrapper: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainInputAssetWrapper()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainInputAsset"",
    ""maps"": [
        {
            ""name"": ""MobileDevicesMap"",
            ""id"": ""29a707c5-8791-494b-8288-c95c60226c37"",
            ""actions"": [
                {
                    ""name"": ""MainAction"",
                    ""type"": ""Button"",
                    ""id"": ""e168c317-fcb7-4e30-88c4-c1e11e798db3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MainActionPosition"",
                    ""type"": ""Value"",
                    ""id"": ""88bfba62-d48e-4def-9cb1-e9805d6978b4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""EndTurnOrCancelAction"",
                    ""type"": ""Button"",
                    ""id"": ""5cfd7548-7ad8-4f44-a558-c9f1165c7ff1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""ed6db86f-8fcd-4fa1-ac47-e9cfe03989c1"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraMovement"",
                    ""type"": ""Value"",
                    ""id"": ""1fe9387f-a79b-470c-a6f3-45cdb5d6e5d5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ac89d615-007f-4dfc-88dd-a7225152f49f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MainAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dcb0ba0a-c12b-4b72-aa9d-ac98c2511734"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MainAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""569f80f6-9a64-4ca1-b527-f732c5dd8ae1"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MainActionPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d47d622d-c575-43b2-b683-de479a1e87f2"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MainActionPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d6e9746d-1612-4e28-87d2-f94191f820c2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EndTurnOrCancelAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fa881f3-5f37-4744-86b3-f08eb06cade0"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""7c8bf3bc-5970-4730-aaa8-66fdfd195c33"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""dc7fa237-854f-4a2d-9c19-5517631e0a81"",
                    ""path"": ""<Keyboard>/#(W)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""695607c4-f655-484c-8406-eef6c75d3645"",
                    ""path"": ""<Keyboard>/#(S)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""78f223f1-822d-4c0a-8820-280122578ef3"",
                    ""path"": ""<Keyboard>/#(A)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""317d4487-6230-4009-816f-44c075b7c859"",
                    ""path"": ""<Keyboard>/#(D)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""6db181f5-6a2d-416d-bbdb-bef7189bd2c1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""9487ba18-d236-401a-a7ce-69fc13dacc2b"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""bb83e994-36db-4bf5-b1e4-97033bac81e4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""f818d349-04a7-4429-97ba-b09979b0349a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""c3d5d0cd-bc42-47e7-8489-75863066ebd5"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MobileDevicesMap
        m_MobileDevicesMap = asset.FindActionMap("MobileDevicesMap", throwIfNotFound: true);
        m_MobileDevicesMap_MainAction = m_MobileDevicesMap.FindAction("MainAction", throwIfNotFound: true);
        m_MobileDevicesMap_MainActionPosition = m_MobileDevicesMap.FindAction("MainActionPosition", throwIfNotFound: true);
        m_MobileDevicesMap_EndTurnOrCancelAction = m_MobileDevicesMap.FindAction("EndTurnOrCancelAction", throwIfNotFound: true);
        m_MobileDevicesMap_Zoom = m_MobileDevicesMap.FindAction("Zoom", throwIfNotFound: true);
        m_MobileDevicesMap_CameraMovement = m_MobileDevicesMap.FindAction("CameraMovement", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // MobileDevicesMap
    private readonly InputActionMap m_MobileDevicesMap;
    private List<IMobileDevicesMapActions> m_MobileDevicesMapActionsCallbackInterfaces = new List<IMobileDevicesMapActions>();
    private readonly InputAction m_MobileDevicesMap_MainAction;
    private readonly InputAction m_MobileDevicesMap_MainActionPosition;
    private readonly InputAction m_MobileDevicesMap_EndTurnOrCancelAction;
    private readonly InputAction m_MobileDevicesMap_Zoom;
    private readonly InputAction m_MobileDevicesMap_CameraMovement;
    public struct MobileDevicesMapActions
    {
        private @MainInputAssetWrapper m_Wrapper;
        public MobileDevicesMapActions(@MainInputAssetWrapper wrapper) { m_Wrapper = wrapper; }
        public InputAction @MainAction => m_Wrapper.m_MobileDevicesMap_MainAction;
        public InputAction @MainActionPosition => m_Wrapper.m_MobileDevicesMap_MainActionPosition;
        public InputAction @EndTurnOrCancelAction => m_Wrapper.m_MobileDevicesMap_EndTurnOrCancelAction;
        public InputAction @Zoom => m_Wrapper.m_MobileDevicesMap_Zoom;
        public InputAction @CameraMovement => m_Wrapper.m_MobileDevicesMap_CameraMovement;
        public InputActionMap Get() { return m_Wrapper.m_MobileDevicesMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MobileDevicesMapActions set) { return set.Get(); }
        public void AddCallbacks(IMobileDevicesMapActions instance)
        {
            if (instance == null || m_Wrapper.m_MobileDevicesMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MobileDevicesMapActionsCallbackInterfaces.Add(instance);
            @MainAction.started += instance.OnMainAction;
            @MainAction.performed += instance.OnMainAction;
            @MainAction.canceled += instance.OnMainAction;
            @MainActionPosition.started += instance.OnMainActionPosition;
            @MainActionPosition.performed += instance.OnMainActionPosition;
            @MainActionPosition.canceled += instance.OnMainActionPosition;
            @EndTurnOrCancelAction.started += instance.OnEndTurnOrCancelAction;
            @EndTurnOrCancelAction.performed += instance.OnEndTurnOrCancelAction;
            @EndTurnOrCancelAction.canceled += instance.OnEndTurnOrCancelAction;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
            @CameraMovement.started += instance.OnCameraMovement;
            @CameraMovement.performed += instance.OnCameraMovement;
            @CameraMovement.canceled += instance.OnCameraMovement;
        }

        private void UnregisterCallbacks(IMobileDevicesMapActions instance)
        {
            @MainAction.started -= instance.OnMainAction;
            @MainAction.performed -= instance.OnMainAction;
            @MainAction.canceled -= instance.OnMainAction;
            @MainActionPosition.started -= instance.OnMainActionPosition;
            @MainActionPosition.performed -= instance.OnMainActionPosition;
            @MainActionPosition.canceled -= instance.OnMainActionPosition;
            @EndTurnOrCancelAction.started -= instance.OnEndTurnOrCancelAction;
            @EndTurnOrCancelAction.performed -= instance.OnEndTurnOrCancelAction;
            @EndTurnOrCancelAction.canceled -= instance.OnEndTurnOrCancelAction;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
            @CameraMovement.started -= instance.OnCameraMovement;
            @CameraMovement.performed -= instance.OnCameraMovement;
            @CameraMovement.canceled -= instance.OnCameraMovement;
        }

        public void RemoveCallbacks(IMobileDevicesMapActions instance)
        {
            if (m_Wrapper.m_MobileDevicesMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMobileDevicesMapActions instance)
        {
            foreach (var item in m_Wrapper.m_MobileDevicesMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MobileDevicesMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MobileDevicesMapActions @MobileDevicesMap => new MobileDevicesMapActions(this);
    public interface IMobileDevicesMapActions
    {
        void OnMainAction(InputAction.CallbackContext context);
        void OnMainActionPosition(InputAction.CallbackContext context);
        void OnEndTurnOrCancelAction(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnCameraMovement(InputAction.CallbackContext context);
    }
}