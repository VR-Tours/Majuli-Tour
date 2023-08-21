using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

 public class infobox : MonoBehaviour {
    public RawImage image;
    public RawImage close;

    public RawImage map;
    public Image map_close;
    public GameObject gameObject;    

    button_map dp;
    public void ButtonPressed(){
        Debug.Log("Button clicked"); 
        image.enabled = !image.enabled;
        close.enabled = !close.enabled;

        
        dp = gameObject.GetComponent<button_map>();
        dp.Start();
        

    }
     public void Start() { 
        image.enabled=false;
        close.enabled=false;
     }
     
     // Update is called once per frame
     public void Update () {
        
     }
 }