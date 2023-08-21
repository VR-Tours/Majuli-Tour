using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;
using System.Globalization;
// using System.Threading.Tasks.Dataflow;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Video;

 public class aadu_infobox : MonoBehaviour {
    // public RawImage image;
    // public RawImage close;
    public GameObject info_box;
    public GameObject setting;
    public GameObject panel;


    // public RawImage map;
    // public Image map_close;
    public GameObject gameObject;    
    public GameObject notif;
    public GameObject calendar;
    public GameObject map;
    public GameObject videoPlayer;
    button_map dp;
    Vector3 finalPos;
    float timer = 0;
    int ctr = 0;
    int active = 0;
    int deactive = 0;
    
    public void SetAllFalse_i(){
        if(GameObject.FindGameObjectsWithTag("MapImg").Length != 0){
            map = GameObject.FindGameObjectsWithTag("MapImg")[0];
            map.SetActive(false);
        }
        if(GameObject.FindGameObjectsWithTag("calendar").Length != 0){
            calendar = GameObject.FindGameObjectsWithTag("calendar")[0];
            calendar.SetActive(false);
        }
        if(GameObject.FindGameObjectsWithTag("SettingImg").Length != 0){
            setting = GameObject.FindGameObjectsWithTag("SettingImg")[0];
            setting.SetActive(false);
        }
        if(GameObject.FindGameObjectsWithTag("Infobox").Length != 0){
            info_box = GameObject.FindGameObjectsWithTag("Infobox")[0];
            info_box.SetActive(false);
        }


    }
    public void ButtonPressed(){
        UnityEngine.Debug.Log("Button clicked"); 
        // image.enabled = !image.enabled;
        // close.enabled = !close.enabled;
        // info_box = GameObject.FindGameObjectsWithTag("Infobox")[0];
        if(GameObject.FindGameObjectsWithTag("MapImg").Length != 0){
            map = GameObject.FindGameObjectsWithTag("MapImg")[0];
            map.SetActive(false);
        }
        if(GameObject.FindGameObjectsWithTag("calendar").Length != 0){
            calendar = GameObject.FindGameObjectsWithTag("calendar")[0];
            calendar.SetActive(false);
        }
        if(GameObject.FindGameObjectsWithTag("SettingImg").Length != 0){
            setting = GameObject.FindGameObjectsWithTag("SettingImg")[0];
            setting.SetActive(false);
        }
        videoPlayer.SetActive(false);

        info_box.SetActive(!info_box.activeInHierarchy);
        // if(setting !=null)
        //     setting.SetActive(false);
        // dp = gameObject.GetComponent<button_map>();
        // dp.Start();

        closeNotif();
        panel.SetActive(false);

    }

    public void getVideo(string type)
    {
        if(type!="INTRO")
        {
            active=1;
            deactive=0;
            notif.SetActive(true);
            notif.GetComponent<RawImage>().texture = Resources.Load<Texture2D>("Video/VideoNotif");
        }
       
        VideoClip new_video = Resources.Load<VideoClip>("Video/"+type.Trim());
        UnityEngine.Debug.Log("Loaded Video "+type);
        videoPlayer.GetComponent<VideoPlayer>().clip = new_video;
    }
    public void GetNotif(string type)
    {
    	if(type!="DefaultInfobox")
        {
            active=1;
            deactive=0;
            notif.SetActive(true);
            notif.GetComponent<RawImage>().texture = Resources.Load<Texture2D>("Infobox/InfoNotif");
        }
        
        Texture2D new_texture = Resources.Load<Texture2D>("Infobox/"+type.Trim());
        info_box.GetComponent<RawImage>().texture = new_texture;
					
    }
    public void closeNotif()
    {
    	active=0;
    	deactive=1;
    }

     public void Start() { 
        // image.enabled=false;
        // close.enabled=false;
        // info_box = GameObject.FindGameObjectsWithTag("Infobox")[0];
        info_box.SetActive(false);
        if(GameObject.FindGameObjectsWithTag("SettingImg").Length != 0)
            setting = GameObject.FindGameObjectsWithTag("SettingImg")[0];

     }

     public void Reset() { 
        // image.enabled=false;
        // close.enabled=false;
        // info_box = GameObject.FindGameObjectsWithTag("Infobox")[0];
        info_box.SetActive(false);
        if(GameObject.FindGameObjectsWithTag("SettingImg").Length != 0)
            setting = GameObject.FindGameObjectsWithTag("SettingImg")[0];

     }
     
     // Update is called once per frame
     public void Update () {
        if(active==1)
        {
            if(notif.transform.localPosition[1]>=126)
            {
            	notif.transform.Translate(new Vector3(0,-1,0) * 3 * Time.deltaTime);
            	ctr+=1;
            }
        }
        if(deactive==1)
        {
        	if(notif.transform.localPosition[1]<=723)
            {
            	notif.transform.Translate(new Vector3(0,1,0) * 3 * Time.deltaTime);
            	ctr-=1;
            }
            else
            {
            	deactive = 0;
            	notif.SetActive(false);
            	
            }
        }
     }
 }