using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty PinchAnimatinAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;
    public InputActionProperty mapShow;
    public GameObject gameObject;
    private bool mapShowState;
    // Start is called before the first frame update
    void Start()
    {
        mapShowState = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mapShowValue = mapShow.action.ReadValue<float>();
        if(mapShowValue==0){
            mapShowState = false;
        }
        if(mapShowValue == 1 && !mapShowState){
            mapShowState = true;
            gameObject.GetComponent<button_map>().ButtonPressed();
        }    

        float triggerValue = PinchAnimatinAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripvalue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripvalue);
    }
}