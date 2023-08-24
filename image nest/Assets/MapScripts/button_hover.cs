using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.Video;

 
 public class button_hover : MonoBehaviour {
    public Image image;
    // public RawImage map;
    // public Image close;
    public Sprite from;
    public Sprite newSprite;
    public String locName= "hello";
    public GameObject gobj;
    public GameObject mapObj;
    public TextMeshProUGUI text;
    // public GameObject locText;
    public int clusterfound(String a){
        TextAsset clusAss=(TextAsset)Resources.Load("clustersBike");
        List<string> lines;
        if(clusAss)
            lines = new List<string>(clusAss.text.Split('\n'));
        else
        {
            UnityEngine.Debug.Log("Not found cluster assignments.");
            return -1;
        }
        // Read each line of the file
        foreach (string line in lines)
        {
            // string line = reader.ReadLine();

            // Split the line into its components
            string[] components = line.Split(',');

            // Parse the integers and rotation value
            String x1 = (components[0]);
            int y1 = int.Parse(components[1]);
            
            
            // Check if the texture coordinates match
            if (a == x1)
            {
                return y1;
            }
        }
        return -1;
    }
    public void ButtonHovered(){
        if(mapObj.GetComponent<button_map>().isEnabled())
        {
		    UnityEngine.Debug.Log("Button Hovered"); 
		    image.sprite = newSprite;
		    text.text = locName.Split(".")[1];
		}
    }
    public void ButtonLeave(){
        // Debug.Log("Button Left"); 
        if(mapObj.GetComponent<button_map>().isEnabled())
        {
		    image.sprite = from;
		    text.text = "";
            DisplayObjectIfFileExists dof;
		    dof= gobj.GetComponent<DisplayObjectIfFileExists>();
		    GameObject.FindWithTag(dof.locName.Split(".")[0]).GetComponent<Image>().sprite = newSprite;
            
            
	    }
    }
    public void ButtonClick(){
        if(mapObj.GetComponent<button_map>().isEnabled())
        {
		    UnityEngine.Debug.Log("clicking!");
		    text.text = "";
		    DisplayObjectIfFileExists dof;
		    dof= gobj.GetComponent<DisplayObjectIfFileExists>();
		    UnityEngine.Debug.Log("***"+dof.locName.Split(".")[0]+"***");
            UnityEngine.Debug.Log(locName.Split(".")[0].Trim());
            GameObject.FindWithTag(dof.locName.Split(".")[0].Trim()).GetComponent<Image>().sprite = from;
            GameObject.FindWithTag(locName.Split(".")[0]).GetComponent<Image>().sprite = newSprite;
            
            dof.locName=locName;
		    
            button_map bmap;
		    bmap=mapObj.GetComponent<button_map>();
		    bmap.ButtonPressed();
		    
            
            followView folView;
		    folView= gobj.GetComponent<followView>();
            // folView.mainCamera.transform.forward=Vector3.forward;
            // if(folView.locName == "8. Kamalabari Satra")
            // {
            //     VideoClip videoClip = Resources.Load<VideoClip>("transport");
            //     folView.UniversalVideoPlayer.clip = videoClip;
            // }
            // else{
            VideoClip videoClip = Resources.Load<VideoClip>("bike");
            folView.UniversalVideoPlayer.clip = videoClip;
            // }
            folView.UniversalVideoPlayer.Prepare();


            UnityEngine.RenderSettings.skybox = folView.skyboxMaterialVideo;
            folView.GetComponent<AudioSource>().clip = null;
            folView.arrowhead.SetActive(false);
            folView.UniversalVideoPlayer.Play();
            if((clusterfound(locName)==clusterfound(folView.locName))&&(clusterfound(locName)!=-1)){
                UnityEngine.Debug.Log("asserted");
                folView.waitDaPls=folView.locationChangeDuration;
            }
            else{
                folView.waitDaPls=0f;
            }
            folView.locName=locName;
		    folView.Start();

	    }
    }
     public void Start() { 
        image.sprite=from;
        text.text = "";
        GameObject.FindWithTag("8").GetComponent<Image>().sprite = newSprite;
     }
     
     // Update is called once per frame
     public void Update () {
        
     }
 }
