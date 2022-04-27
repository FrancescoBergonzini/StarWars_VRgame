using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using AudioItem = SW_VRGame.AudioManager.AudioType;


/// <summary>
/// Agganciare questo script ai controller vr
/// </summary>

[RequireComponent(typeof(XRBaseController))]
public class HandInputHandler : MonoBehaviour
{
    public XRBaseController[] controllers;
    //[SerializeField]
    //protected InputActionReference RestartAction;
    [SerializeField]
    protected InputActionReference SetSwordStatus;
 
    private void Start()
    {
        SetSwordStatus.action.performed += SetSword;
        SW_VRGame.SW_CutBladeLogic.Instance.Event_ApticInput += SendHapticImpulse;
    }

    private void OnDisable()
    {
        SetSwordStatus.action.performed -= SetSword;
    }



    /// <summary>
    /// Set Sword determina la stato PAUSA, ma si attiva solo se la spada è grabbata
    /// </summary>
    /// <param name="obj"></param>
    private void SetSword(InputAction.CallbackContext obj)
    {
        if (!SW_VRGame.SW_LightSaber.Instance.isBeenGrabbed)
            return;

        SW_VRGame.SW_LightSaber.Instance.isPaused = !SW_VRGame.SW_LightSaber.Instance.isPaused;

        if (SW_VRGame.SW_LightSaber.Instance.isPaused)
        {
            SW_VRGame.SW_GameManager.Instance.ResumeGame();


            //AUDIO
            SW_VRGame.AudioManager.Instance.Play(AudioItem.LaserSwordOff);
        }
        else
        {
            SW_VRGame.SW_GameManager.Instance.PauseGame();


            //AUDIO
            SW_VRGame.AudioManager.Instance.Play(AudioItem.LaserSwordOff);
        }
    }


    /// <summary>
    /// Per utilizzare la vibrazione del controller
    /// NOTA: se si utilizza la vibrazione agganciare questo component SEMPRE al controller
    /// </summary>
    /// <param name="amplitude"></param>
    /// <param name="duration"></param>
    public void SendHapticImpulse(float amplitude, float duration)
    {
        foreach(XRBaseController controller in controllers)
        {
            controller.SendHapticImpulse(amplitude, duration);
        }
        
    }

}
