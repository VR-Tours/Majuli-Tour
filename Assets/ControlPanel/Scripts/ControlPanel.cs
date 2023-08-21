using System.Diagnostics;
using System.Resources;
using System.Data;
using System.Transactions;
using System.Linq;
using System.Runtime.Versioning;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Mime;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class ControlPanel : MonoBehaviour
{
    List<string> linesAudio;
    public GameObject setting;
    public GameObject infobox;
    public GameObject gameObject;
    public RawImage on;
    public RawImage off;
    public GameObject VideoPlayer; 
    public GameObject calendar;
    public GameObject C1;
    public GameObject map;
    public GameObject C2;
    public GameObject paneloo;
    public GameObject minimap;
    public GameObject mainCamera;
    public GameObject audio;
    public GameObject tutorial;
    public void Start() { 

        // if()
        if(GameObject.FindGameObjectsWithTag("SettingImg").Length != 0)
            setting = GameObject.FindGameObjectsWithTag("SettingImg")[0];
            // print(GameObject.FindGameObjectsWithTag("Infobox"));
        if(GameObject.FindGameObjectsWithTag("Infobox").Length != 0)
            infobox = GameObject.FindGameObjectsWithTag("Infobox")[0];

        setting.SetActive(false);
        // on.enabled = true;
        // off.enabled = false;
        VideoPlayer.SetActive(false);
        tutorial.SetActive(false);


        calendar = GameObject.FindGameObjectsWithTag("calendar")[0];
        calendar.SetActive(false);

        if(GameObject.FindGameObjectsWithTag("C1Img").Length > 0)
        C1 = GameObject.FindGameObjectsWithTag("C1Img")[0];
        if(GameObject.FindGameObjectsWithTag("C2Img").Length > 0)
        C2 = GameObject.FindGameObjectsWithTag("C2Img")[0];

        minimap.SetActiveRecursively(false);
    }

    public void SetAllFalse(){
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
            infobox = GameObject.FindGameObjectsWithTag("Infobox")[0];
            infobox.SetActive(false);
        }


    }

    public void SettingPressed(){
        // Debug.Log("Button clicked"); 
        setting.SetActive (!setting.activeInHierarchy);
        if(infobox != null)
            infobox.SetActive(false);
        if(GameObject.FindGameObjectsWithTag("MapImg").Length != 0){
            map = GameObject.FindGameObjectsWithTag("MapImg")[0];
            map.SetActive(false);
        }
        if(GameObject.FindGameObjectsWithTag("calendar").Length != 0){
            calendar = GameObject.FindGameObjectsWithTag("calendar")[0];
            calendar.SetActive(false);
        }
        VideoPlayer.SetActive(false);

        UnityEngine.Debug.Log("Button clicked"); 
        paneloo.SetActive(false);
    }

    public void TutPressed(){
        SettingPressed();
        tutorial.SetActive (!tutorial.activeInHierarchy);

    }

    public void CalendarPressed(){
        // Debug.Log("Button clicked"); 
        calendar.SetActive (!calendar.activeInHierarchy);
        if(infobox != null)
            infobox.SetActive(false);
        if(GameObject.FindGameObjectsWithTag("MapImg").Length != 0){
            map = GameObject.FindGameObjectsWithTag("MapImg")[0];
            map.SetActive(false);
        }
        if(GameObject.FindGameObjectsWithTag("SettingImg").Length != 0){
            setting = GameObject.FindGameObjectsWithTag("SettingImg")[0];
            setting.SetActive(false);
        }
        VideoPlayer.SetActive(false);
        UnityEngine.Debug.Log("CalendarPressed clicked"); 
        C2.SetActive(false);
        C1.SetActive(true);
        paneloo.SetActive(false);
    }

    public void C1C2(){
        // Debug.Log("Button clicked"); 
        C1.SetActive(false);
        C2.SetActive(true);

    }
    public void C2C1(){
        // Debug.Log("Button clicked");
        C1.SetActive(true);
        C2.SetActive(false); 

    
    }

    public void ExitMap(){
        UnityEngine.Debug.Log("exit setting clicked"); 
        setting.SetActive(false);
        // setting.SetActive (!setting.activeInHierarchy);

    }

    public void videoPressed()
    {
        if(infobox != null)
            infobox.SetActive(false);
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
        VideoPlayer.SetActive(!VideoPlayer.activeInHierarchy);
        GameObject.FindWithTag("DynamicCanvas").GetComponent<aadu_infobox>().closeNotif();
        paneloo.SetActive(false);
        // VideoPlayer.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/Asteroids Gameplay");
    }
    public void AudioToggle(){
        UnityEngine.Debug.Log("Audio Toggle"); 
        on.enabled = !on.enabled;
        off.enabled = !off.enabled;

        if(on.enabled==true){
            followView fv;
            fv = mainCamera.GetComponent<followView>();
            string locName = fv.locName;
            int presentX=fv.getX();
            int presentY=fv.getY();
            TextAsset mytxtDataAudio=(TextAsset)Resources.Load("audio_swee/"+locName);
		// UnityEngine.Debug.Log(mytxtDataAudio);
            if(mytxtDataAudio!=null)
            {
                linesAudio = new List<string>(mytxtDataAudio.text.Split('\n'));
                foreach (string line in linesAudio)
                {
                    // string line = audio_reader.ReadLine();

                    // Split the line into its components
                    string[] components = line.Split(',');

                    // Parse the integers and rotation value
                    int x1 = int.Parse(components[0]);
                    int y1 = int.Parse(components[1]);
                    // int x2 = int.Parse(components[2]);
                    // int y2 = int.Parse(components[3]);
                    string audio_file = components[2];
                    
                    
                    if (x1==presentX && y1==presentY)
                    {
                        
                        AudioSource ad_src = mainCamera.GetComponent<AudioSource>();
                        AudioClip new_clip  = Resources.Load<AudioClip>("audio_swee" + "/"+audio_file);	
                        if(new_clip!=null)		
                            ad_src.clip = new_clip;
                        ad_src.Play();
                        
                        //Debug.Log("music set to "+audio_file);
                        break; // Exit the loop since we found a match
                    }
                }
            }
            else{
                    AudioSource ad_src = mainCamera.GetComponent<AudioSource>();
                    // AudioClip new_clip  = Resources.Load<AudioClip>("audio_swee" + "/"+audio_file);	
                    // if(new_clip!=null)		
                    ad_src.clip = null;
                    ad_src.Play();
            }

        }
        else{
            AudioSource ad_src = mainCamera.GetComponent<AudioSource>();
            ad_src.Pause();
        }

    }

        public void AudioTurnOffWhenOn(){
        UnityEngine.Debug.Log("Audio Turning Off");      
        if(on.enabled==true){
            on.enabled = false;
            off.enabled = true;
            AudioSource ad_src = mainCamera.GetComponent<AudioSource>();
            ad_src.Pause();
        }

    }

    public void Exitgame(){
        Application.Quit();
    }

    public void MiniMapPressed()
    {
        minimap.SetActiveRecursively(!minimap.activeSelf);
        paneloo.SetActive(!paneloo.activeSelf);
        if(minimap.activeSelf)
        {
            followView fv;
            fv = mainCamera.GetComponent<followView>();
            string locName = fv.locName;
            minimap.GetComponent<RawImage>().texture = Resources.Load<Texture2D>(locName+"/MiniMap");
        }
    }
     // Update is called once per frame
     public void Update () {
        
     }
}
