using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
using UnityEngine.Events;
using System;

public class WaveInputCustom : MonoBehaviour
{
    [Tooltip("添加VivePointers预制体")]
    public GameObject var_vivePointers;
    [Tooltip("添加ViveCurvePointers预制体")]
    public GameObject var_viveTeleports;
    [Tooltip("右手柄Trigger点击事件")]
    public EventOnTrigger OnRightHandTriggerClick;
    [Tooltip("左手柄Trigger点击事件")]
    public EventOnTrigger OnLeftHandTriggerClick;
    [Tooltip("右手柄Menu点击事件")]
    public EventOnTrigger OnRightHandMenuClick;
    [Tooltip("左手柄Menu点击事件")]
    public EventOnTrigger OnLeftHandMenuClick;
    [HideInInspector]
    public Vector2 left_touchPadAxis;
    [HideInInspector]
    public Vector2 right_touchPadAxis;

    public static WaveInputCustom instance;
    private void Awake()
    {
        instance = this;
        ViveInput.AddListenerEx(HandRole.RightHand, ControllerButton.Trigger, ButtonEventType.Click, OnRightHandTrigger);
        ViveInput.AddListenerEx(HandRole.LeftHand, ControllerButton.Trigger, ButtonEventType.Click, OnLeftHandTrigger);

        ViveInput.AddListenerEx(HandRole.RightHand, ControllerButton.Menu, ButtonEventType.Click, OnRightHandMenuTrigger);
        ViveInput.AddListenerEx(HandRole.LeftHand, ControllerButton.Menu, ButtonEventType.Click, OnLeftHandMenuTrigger);
        //if (CheckUIPointerActive())
        //    SetPointerToolOff();
        //if (CheckTeleportActive())
        //    SetTeleportToolOff();

    }
    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(HandRole.RightHand, ControllerButton.Trigger, ButtonEventType.Click, OnRightHandTrigger);
        ViveInput.RemoveListenerEx(HandRole.LeftHand, ControllerButton.Trigger, ButtonEventType.Click, OnLeftHandTrigger);
        ViveInput.RemoveListenerEx(HandRole.RightHand, ControllerButton.Menu, ButtonEventType.Click, OnRightHandMenuTrigger);
        ViveInput.RemoveListenerEx(HandRole.LeftHand, ControllerButton.Menu, ButtonEventType.Click, OnLeftHandMenuTrigger);
    }
    public bool CheckUIPointerActive()
    {
        if (var_vivePointers == null)
        {
            var_vivePointers = GameObject.Find("VivePointers");
            if (var_vivePointers == null)
            {
                Debug.LogWarning("这个场景里没有VivePointers！不能对发出射线对UI进行操作,如果想使用该功能，请将HTC.UnityPlugin/ViveInputUtility/Prefabs/VivePointers.prefab" +
                    "拖入置场景");
                return false;
            }
            else
                return true;
        }
        return true;
    }
    public bool CheckTeleportActive()
    {
        if (var_viveTeleports == null)
        {
            var_viveTeleports = GameObject.Find("ViveCurvePointers");
            if (var_viveTeleports == null)
            {
                Debug.LogWarning("这个场景里没有ViveCurvePointers！不能闪现,如果想使用该功能，请将HTC.UnityPlugin/ViveInputUtility/Prefabs/ViveCurvePointers.prefab" +
                    "拖入置场景");
                return false;
            }
            else
                return true;
        }
        return true;
    }
    private void OnRightHandMenuTrigger()
    {
        OnRightHandMenuClick.Invoke();
    }
    private void OnLeftHandMenuTrigger()
    {
        OnLeftHandMenuClick.Invoke();
    }
    private void OnRightHandTrigger()
    {
        OnRightHandTriggerClick.Invoke();
    }
    private void OnLeftHandTrigger()
    {
        OnLeftHandTriggerClick.Invoke();
    }
    private void Update()
    {
        right_touchPadAxis = ViveInput.GetPadTouchAxis(HandRole.RightHand);
        left_touchPadAxis = ViveInput.GetPadTouchAxis(HandRole.LeftHand);
        DebugKey();
    }

    private void DebugKey()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetTeleportToolOn();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SetPointerToolOn();
        }
    }

    public void SetTeleportToolOn()
    {
        if (CheckTeleportActive())
            var_viveTeleports.SetActive(true);
    }
    public void SetTeleportToolOff()
    {
        if (CheckTeleportActive())
            var_viveTeleports.SetActive(false);
    }
    public void SetPointerToolOn()
    {
        if (CheckUIPointerActive())
            var_vivePointers.SetActive(true);
    }
    public void SetPointerToolOff()
    {
        if (CheckUIPointerActive())
            var_vivePointers.SetActive(false);
    }
}
[System.Serializable]
public class EventOnTrigger : UnityEvent
{

}
