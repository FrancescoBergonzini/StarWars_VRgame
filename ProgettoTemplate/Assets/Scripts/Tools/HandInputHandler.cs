using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;


/// <summary>
/// Agganciare questo script ai controller vr
/// </summary>

[RequireComponent(typeof(XRBaseController))]
public class HandInputHandler : MonoBehaviour
{
    protected XRBaseController controller;
    //[SerializeField]
    //protected InputActionReference RestartAction;
    [SerializeField]
    protected InputActionReference SetSwordStatus;
 


    void Awake()
    {
        controller = GetComponent<XRBaseController>();
        //RestartAction.action.performed += Restart;
        
    }

    private void Start()
    {
        Debug.Log("Script set Input enable sword");
        SetSwordStatus.action.performed += SetSword;
    }

    private void OnDisable()
    {
        SetSwordStatus.action.performed -= SetSword;
    }

    private void Restart(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetSword(InputAction.CallbackContext obj)
    {
        Debug.Log("Lancio set sword");

        if (SW_VRGame.SW_LightSaber.isBeenGrabbed) //funziona solo se spada grabbata
        {
            SW_VRGame.SW_LightSaber.isPaused = !SW_VRGame.SW_LightSaber.isPaused;
        }
        else 
        { 

            // Move the object upward in world space 1 unit/second.
            //transform.Translate(Vector3.up * Time.deltaTime, Space.World);         
        }
    }

    private void Update()
    {
        // Move our position a step closer to the target.
        
    }

    /// <summary>
    /// Per utilizzare la vibrazione del controller
    /// NOTA: se si utilizza la vibrazione agganciare questo component SEMPRE al controller
    /// </summary>
    /// <param name="amplitude"></param>
    /// <param name="duration"></param>
    public void SendHapticImpulse(float amplitude, float duration)
    {
        controller.SendHapticImpulse(amplitude, duration);
    }

}
