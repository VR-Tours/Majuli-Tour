using System.Linq;
using System.Net.Mime;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;
 
 public class button_map : MonoBehaviour {
    public InputActionProperty moveMapAction;
    public GameObject gameObject_static;
    public GameObject gameObject_dynamic;
    public GameObject camComp;
    public GameObject panel;
    aadu_infobox dp;
    bool isActive = false;
    public RawImage image;
    // public GameObject;
    public Image close;
    public Image loc1;
    public Image loc2;
    public Image loc3;
    public Image loc4;
    public Image loc5;
    public Image loc6;
    public Image loc7;
    public Image loc8;
    public Image loc9;
    public Image loc10;
    public Image loc11;
    public Image loc12;
    public Image loc13;
    public Image loc14;
    public Image loc15;
    public Image loc16;
    public Image loc17;
    public Image loc18;
    public Image loc19;
    public Image loc20;
    public Image loc21;
    public Image loc22;
    public Image loc23;
    public Image loc24;
    public Image loc25;
    private Vector3 vectorUpwards=new Vector3(0,(float)1.2,0);

    public bool isEnabled(){
        return image.enabled;
    }
    


    public void ButtonPressed(){
        gameObject_static.SetActiveRecursively(!isActive);
        isActive=!isActive;
        if(isActive)
        {
            Vector3 vec=new Vector3(0,0,(float)2.2);
            Quaternion rotation = Quaternion.FromToRotation(vec, new Vector3(camComp.transform.forward.x,0,camComp.transform.forward.z));
            gameObject_static.GetComponent<RectTransform>().localPosition = rotation*vec+vectorUpwards;
            gameObject_static.GetComponent<RectTransform>().localRotation = rotation;
        }
       

        Debug.Log("Button clicked"); 
        // image.enabled = !image.enabled;
        // close.enabled = !close.enabled;
        // loc1.enabled = !loc1.enabled;
        // loc2.enabled = !loc2.enabled;
        // loc3.enabled = !loc3.enabled;
        // loc4.enabled = !loc4.enabled;
        // loc5.enabled = !loc5.enabled;
        // loc6.enabled = !loc6.enabled;
        // loc7.enabled = !loc7.enabled;
        // loc8.enabled = !loc8.enabled;
        // loc9.enabled = !loc9.enabled;
        // loc10.enabled = !loc10.enabled;
        // loc11.enabled = !loc11.enabled;
        // loc12.enabled = !loc12.enabled;
        // loc13.enabled = !loc13.enabled;
        // loc14.enabled = !loc14.enabled;
        // loc15.enabled = !loc15.enabled;
        // loc16.enabled = !loc16.enabled;
        // loc17.enabled = !loc17.enabled;
        // loc18.enabled = !loc18.enabled;
        // loc19.enabled = !loc19.enabled;
        // loc20.enabled = !loc20.enabled;
        // loc21.enabled = !loc21.enabled;
        // loc22.enabled = !loc22.enabled;
        // loc23.enabled = !loc23.enabled;
        // loc24.enabled = !loc24.enabled;
        // loc25.enabled = !loc25.enabled;
        panel.SetActive(false);
        dp = gameObject_dynamic.GetComponent<aadu_infobox>();
        dp.Reset();

    }
     public void Start() { 
        gameObject_static.SetActiveRecursively(false);
        // image.enabled=false;
        // close.enabled=false;
        // loc1.enabled=false;
        // loc2.enabled=false;
        // loc3.enabled=false;
        // loc4.enabled=false;
        // loc5.enabled = false;
        // loc6.enabled = false;
        // loc7.enabled = false;
        // loc8.enabled = false;
        // loc9.enabled = false;
        // loc10.enabled = false;
        // loc11.enabled = false;
        // loc12.enabled = false;
        // loc13.enabled = false;
        // loc14.enabled = false;
        // loc15.enabled = false;
        // loc16.enabled = false;
        // loc17.enabled = false;
        // loc18.enabled = false;
        // loc19.enabled = false;
        // loc20.enabled = false;
        // loc21.enabled = false;
        // loc22.enabled = false;
        // loc23.enabled = false;
        // loc24.enabled = false;
        // loc25.enabled = false;
        // mapShowState = false;
     }
     
     // Update is called once per frame
     public void Update () { 
        Vector2 joyStickDiretionleft =  moveMapAction.action.ReadValue<Vector2>(); 

        float angleBwForwardAndController = Vector2.Dot(joyStickDiretionleft, Vector2.up);
        float angleBwBackAndController = Vector2.Dot(joyStickDiretionleft, Vector2.down);

        if (angleBwForwardAndController>0.85){
                gameObject_static.GetComponent<RectTransform>().localPosition = (float)1.05*(gameObject_static.GetComponent<RectTransform>().localPosition-vectorUpwards)+vectorUpwards;
                
        }else if(angleBwBackAndController > 0.85){
                gameObject_static.GetComponent<RectTransform>().localPosition = (float)0.96*(gameObject_static.GetComponent<RectTransform>().localPosition-vectorUpwards)+vectorUpwards;
        }

     }
 }