using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Image_change : MonoBehaviour
{
    GameObject first;
    GameObject second;
    GameObject third;
    GameObject prompt;
    GameObject e1;
    GameObject e2;
    GameObject videoPlayer;
    GameObject dbox;
    public GameObject gobj;

    public String StartLocName="8. Kamalabari Satra";
    // GameObject 
    // Start is called before the first frame update
    public void Start()
    {
        first = GameObject.FindGameObjectsWithTag("FirstImg")[0];
        second = GameObject.FindGameObjectsWithTag("SecondImg")[0];
        third = GameObject.FindGameObjectsWithTag("ThirdImg")[0];
        prompt = GameObject.FindGameObjectsWithTag("PromptImg")[0];
        e1 = GameObject.FindGameObjectsWithTag("E1Img")[0];
        e2 = GameObject.FindGameObjectsWithTag("E2Img")[0];
        videoPlayer = GameObject.FindGameObjectsWithTag("VideoPlayerImg")[0];
        dbox = GameObject.FindGameObjectsWithTag("DialogBox")[0]; // Reference to the dialog box object

        dbox.SetActive(false);
        first.SetActive(true);
        second.SetActive(false);
        third.SetActive(false);
        prompt.SetActive(false);
        e1.SetActive(false);
        e2.SetActive(false);
        videoPlayer.SetActive(false);


    }

    // Update is called once per frame
    public void nextImage12()
    {
        first.SetActive(false);
        second.SetActive(true);
    }

    public void prevImage21()
    {
        first.SetActive(true);
        second.SetActive(false);
    }
    public void nextImage23()
    {
        second.SetActive(false);
        third.SetActive(true);
    }

    public void prevImage32()
    {
        second.SetActive(true);
        third.SetActive(false);
    }
    public void experience()
    {
        first.SetActive(false);
        second.SetActive(false);
        third.SetActive(false);
        prompt.SetActive(true);

    }
    public void promptE1()
    {
        prompt.SetActive(false);
        e1.SetActive(true);
    }
    public void E1prompt()
    {
        prompt.SetActive(true);
        e1.SetActive(false);
    }
    public void E1E2()
    {
        e1.SetActive(false);
        e2.SetActive(true);
    }
    public void E2E1()
    {
        e1.SetActive(true);
        e2.SetActive(false);
    }
    public void E2Game()
    {
        e2.SetActive(false);
        videoPlayer.SetActive(true);
    }
    public void EscapeVideo(){
        videoPlayer.SetActive(false);
        dbox.SetActive(false);
        DisplayObjectIfFileExists dof;
        dof= gobj.GetComponent<DisplayObjectIfFileExists>();
        dof.locName=StartLocName;
        followView folView;
        folView= gobj.GetComponent<followView>();
        // folView.mainCamera.transform.forward=Vector3.forward;
        // UnityEngine.RenderSettings.skybox = folView.skyboxMaterialVideo;
        // folView.UniversalVideoPlayer.Play();
        // folView.waitDaPls=0;
        // folView.locName=StartLocName;
        // folView.Start();
        VideoClip videoClip = Resources.Load<VideoClip>("ferry");
        folView.UniversalVideoPlayer.clip = videoClip;
        folView.UniversalVideoPlayer.Prepare();
        // folView.aud().clip = null;
        

        UnityEngine.RenderSettings.skybox = folView.skyboxMaterialVideo;
        folView.GetComponent<AudioSource>().clip = null;
        folView.arrowhead.SetActive(false);
        folView.UniversalVideoPlayer.Play();
        folView.waitDaPls=folView.StartLocationChangeDuration;
        folView.locName=StartLocName;
    // videoPlayer.Prepare();
    // videoPlayer.Play();
        folView.Start();
    }

    public void ReStartVideo(){
        videoPlayer.SetActive(false);
        videoPlayer.SetActive(true);
        dbox.SetActive(false);
    }

    public void showDB(){
        videoPlayer.SetActive(false);
        dbox.SetActive(true);
    }


}
