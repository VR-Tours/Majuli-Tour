using System.Collections.Specialized;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.Data;
using Microsoft.VisualBasic;
using System.Web;
// using System.Threading.Tasks.Dataflow;
using System.Runtime.Serialization;
using System.Net.Http;
using System.Net.WebSockets;
using System.Net;
using UnityEngine.XR;
// using System.Reflection.PortableExecutable;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MyVideoPlayer : MonoBehaviour
{

    public GameObject cinemaPlane;
    public GameObject btnPlay;
    public GameObject btnPause;
    public GameObject knob;
    public GameObject progressBar;
    public GameObject progressBarBG;
    public GameObject mainCamera_obj;
    public GameObject interactor_ctrl;
    public GameObject interactor_ctrl2;
    // public 
    public InputActionProperty vidPlayPause;
    public InputActionProperty vidPlayPause2;

    Camera mainCamera;

    

    private float maxKnobValue;
    private float newKnobX;
    private float maxKnobX;
    private float minKnobX;
    private float knobPosY;
    private float simpleKnobValue;
    private float knobValue;
    private float progressBarWidth;
    private bool knobIsDragging;
    private bool videoIsJumping = false;
    private bool videoIsPlaying = false;
    private VideoPlayer videoPlayer;
    private Ray ray;
    private RaycastHit hit;
    private RaycastHit hitNew;

    private bool VideoPlayPauseState =false;

    private void Start  ()
    {
        mainCamera = mainCamera_obj.GetComponent<Camera>();
        knobPosY = knob.transform.localPosition.y;
        videoPlayer = GetComponent<VideoPlayer>();
        btnPause.SetActive(true);
        btnPlay.SetActive(false);
        UnityEngine.Debug.Log("Frame count:"+videoPlayer.frameCount);
        videoPlayer.frame = 0;
        // progressBarWidth = progressBarBG.GetComponent<SpriteRenderer>().bounds.size.x;
        progressBarWidth = 9.3f-0.581f;
        UnityEngine.Debug.Log("ProgressBarWidth:"+progressBarWidth);
        // UnityEngine.Debug.Log("")

        UnityEngine.Debug.Log("Frame count:"+videoPlayer.frameCount);

    }

    private void Update()
    {
        if(videoPlayer.frame==(long)videoPlayer.frameCount-1 && videoPlayer.CompareTag("VideoPlayerImg")){
            GameObject.FindGameObjectsWithTag("StartScreen")[0].GetComponent<Image_change>().showDB();
        }
        if (!knobIsDragging && !videoIsJumping)
        {
            if (videoPlayer.frameCount > 0)
            {
                float progress = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
                progressBar.transform.localScale = new Vector3(progressBarWidth * progress, progressBar.transform.localScale.y, 0);
                knob.transform.localPosition = new Vector2(progressBar.transform.localPosition.x + (progressBarWidth * progress), knob.transform.localPosition.y);
            }
        }
        // if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
        // {
        //     UnityEngine.Debug.Log("Here1");
        //     ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        //     if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //     {
        //         UnityEngine.Debug.Log("Here");
        //     }
        // }
        // if (Input.GetAxis("LeftTrigger") > 0.5f)
        // Vector2 joyStickDiretion = nextImageAction.action.ReadValue<Vector2>();
        float triggerPressedOrNot2 = vidPlayPause2.action.ReadValue<float>(); 
        float triggerPressedOrNot = vidPlayPause.action.ReadValue<float>();
        if(triggerPressedOrNot < 0.5f&&triggerPressedOrNot2<0.5f){
            VideoPlayPauseState =false;
        }

        if((triggerPressedOrNot>0.5f||triggerPressedOrNot2>0.5f)&&VideoPlayPauseState==false)
        {
            VideoPlayPauseState =true;
            UnityEngine.Debug.Log("Just pressed video play button");

            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 cameraDirection = mainCamera.transform.forward;
            RaycastHit hit;
            UnityEngine.XR.InputDevice device =  UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.LeftHand); // get a reference to the left hand controller
            Vector3 controllerPosition = interactor_ctrl.transform.position;
            Vector3 controllerDirection =interactor_ctrl.transform.rotation*Vector3.forward;

            // if (device.TryGetFeatureValue(new InputFeatureUsage<Vector3>("position"), out controllerPosition)) // get the position of the controller
            // {
            //     if (device.TryGetFeatureValue(new InputFeatureUsage<Quaternion>("rotation"), out Quaternion controllerRotation)) // get the rotation of the controller
            //     {
            //         controllerDirection = controllerRotation * Vector3.forward; // calculate the forward direction of the controller
            //     }
            // }
            Physics.Raycast(controllerPosition,controllerDirection,out hit);
            
            // || (hitCollider2.CompareTag(cinemaPlane.tag)&& videoIsPlaying)
            if (hit.collider != null && (hit.collider.CompareTag(btnPause.tag)) )
            {
            Collider collider = hit.collider;
            UnityEngine.Debug.Log("Collider hit: " + collider.name);

            // Print the dimensions of the collider
            Bounds bounds = collider.bounds;
            UnityEngine.Debug.Log("Collider dimensions: " + bounds.size);

            // Print the position of the collider
            Vector3 position = collider.transform.position;
            UnityEngine.Debug.Log("Collider position: " + position);
                print("pauseBtn");
                BtnPlayVideo();
            }
            if (hit.collider != null && (hit.collider.CompareTag(btnPlay.tag)))
            {
                print("playBtn");
                BtnPlayVideo();
            }



            RaycastHit hit2;
            // UnityEngine.XR.InputDevice device =  UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.LeftHand); // get a reference to the left hand controller
            Vector3 controllerPosition2 = interactor_ctrl2.transform.position;
            Vector3 controllerDirection2 =interactor_ctrl2.transform.rotation*Vector3.forward;
            // if (device.TryGetFeatureValue(new InputFeatureUsage<Vector3>("position"), out controllerPosition)) // get the position of the controller
            // {
            //     if (device.TryGetFeatureValue(new InputFeatureUsage<Quaternion>("rotation"), out Quaternion controllerRotation)) // get the rotation of the controller
            //     {
            //         controllerDirection = controllerRotation * Vector3.forward; // calculate the forward direction of the controller
            //     }
            // }
            Physics.Raycast(controllerPosition2,controllerDirection2,out hit2);
            
            // || (hitCollider2.CompareTag(cinemaPlane.tag)&& videoIsPlaying)
            if (hit2.collider != null && (hit2.collider.CompareTag(btnPause.tag)) )
            {
            UnityEngine.Debug.Log("hitting!!!!!!");
            Collider collider2 = hit2.collider;
            UnityEngine.Debug.Log("Collider hit: " + collider2.name);

            // Print the dimensions of the collider
            Bounds bounds = collider2.bounds;
            UnityEngine.Debug.Log("Collider dimensions: " + bounds.size);

            // Print the position of the collider
            Vector3 position2 = collider2.transform.position;
            UnityEngine.Debug.Log("Collider position: " + position2);
                print("pauseBtn");
                BtnPlayVideo();
            }
            if (hit2.collider != null && (hit2.collider.CompareTag(btnPlay.tag)))
            {
                print("playBtn");
                BtnPlayVideo();
            }
        }
    }

    public void KnobOnPressDown()
    {
        VideoStop();
        minKnobX = progressBar.transform.localPosition.x;
        maxKnobX = minKnobX + progressBarWidth;
    }

    public void KnobOnRelease()
    {
        knobIsDragging = false;
        CalcKnobSimpleValue();
        VideoPlay();
        VideoJump();
        StartCoroutine(DelayedSetVideoIsJumpingToFalse());
    }

    IEnumerator DelayedSetVideoIsJumpingToFalse()
    {
        yield return new WaitForSeconds(2);
        SetVideoIsJumpingToFalse();
    }

    public void KnobOnDrag()
    {
        knobIsDragging = true;
        videoIsJumping = true;
        // Vector3 curScreenPoint = new Vector2(interactor_ctrl.transform.position.x, interactor_ctrl.transform.position.y);
        // Vector3 curPosition = mainCamera.ScreenToWorldPoint(curScreenPoint);
        // knob.transform.position = new Vector2(curPosition.x, curPosition.y);
        // Ray ray = new Ray(interactor_ctrl.transform.position, interactor_ctrl.transform.rotation*Vector3.forward);
        // UnityEngine.Debug.Log(progressBarBG.tag);
        // Vector3 controllerPosition = interactor_ctrl.transform.position;
        // Vector3 controllerDirection =interactor_ctrl.transform.rotation*Vector3.forward;
        // Physics.Raycast(controllerPosition, controllerDirection,out hitNew);
        // if (hitNew.collider != null && hitNew.collider.CompareTag(progressBarBG.tag)) {
        // // Collision detected with sprite
        // UnityEngine.Debug.Log("Collision detected with sprite");
        // Vector3 collisionPoint = hitNew.point;
        // Do something with the collision point
        // var d=transform.position.z;
        // Vector3 hitPoint = hitNew.point; // Get the world position of the hit point
        // Vector3 localHitPoint = transform.InverseTransformPoint(hitPoint); // Convert the world position to local position
        newKnobX = knob.transform.localPosition.x + progressBarWidth/400; 
        // UnityEngine.Debug.Log(progressBarWidth);
        if (newKnobX > maxKnobX) { newKnobX = maxKnobX; }
        if (newKnobX < minKnobX) { newKnobX = minKnobX; }
        knob.transform.localPosition = new Vector2(newKnobX, knobPosY);
        CalcKnobSimpleValue();
        progressBar.transform.localScale = new Vector3(simpleKnobValue * progressBarWidth, progressBar.transform.localScale.y, 0);
        // }
    }

    public void KnobOnDrag1()
    {
        knobIsDragging = true;
        videoIsJumping = true;
        // Vector3 curScreenPoint = new Vector2(interactor_ctrl.transform.position.x, interactor_ctrl.transform.position.y);
        // Vector3 curPosition = mainCamera.ScreenToWorldPoint(curScreenPoint);
        // knob.transform.position = new Vector2(curPosition.x, curPosition.y);
        // Ray ray = new Ray(interactor_ctrl.transform.position, interactor_ctrl.transform.rotation*Vector3.forward);
        // UnityEngine.Debug.Log(progressBarBG.tag);
        // Vector3 controllerPosition = interactor_ctrl.transform.position;
        // Vector3 controllerDirection =interactor_ctrl.transform.rotation*Vector3.forward;
        // Physics.Raycast(controllerPosition, controllerDirection,out hitNew);
        // if (hitNew.collider != null && hitNew.collider.CompareTag(progressBarBG.tag)) {
        // // Collision detected with sprite
        // UnityEngine.Debug.Log("Collision detected with sprite");
        // Vector3 collisionPoint = hitNew.point;
        // Do something with the collision point
        // var d=transform.position.z;
        // Vector3 hitPoint = hitNew.point; // Get the world position of the hit point
        // Vector3 localHitPoint = transform.InverseTransformPoint(hitPoint); // Convert the world position to local position
        newKnobX = knob.transform.localPosition.x - progressBarWidth/400; 
        UnityEngine.Debug.Log(progressBarWidth);
        if (newKnobX > maxKnobX) { newKnobX = maxKnobX; }
        if (newKnobX < minKnobX) { newKnobX = minKnobX; }
        knob.transform.localPosition = new Vector2(newKnobX, knobPosY);
        CalcKnobSimpleValue();
        progressBar.transform.localScale = new Vector3(simpleKnobValue * progressBarWidth, progressBar.transform.localScale.y, 0);
        // }
    }

    private void SetVideoIsJumpingToFalse()
    {
        videoIsJumping = false;
    }

    private void CalcKnobSimpleValue()
    {
        maxKnobValue = maxKnobX - minKnobX;
        knobValue = knob.transform.localPosition.x - minKnobX;
        simpleKnobValue = knobValue / maxKnobValue;
    }

    private void VideoJump()
    {
        var frame = videoPlayer.frameCount * simpleKnobValue;
        videoPlayer.frame = (long)frame;
    }
    public void QuadPress()
    {
       UnityEngine.Debug.Log("QuadClicked");
    }
    public void BtnPlayVideo()
    {
        if (videoIsPlaying)
        {
            VideoStop();
        }
        else
        {
            VideoPlay();
        }
    }

    private void VideoStop()
    {
        videoIsPlaying = false;
        videoPlayer.Pause();
        btnPause.SetActive(false);
        btnPlay.SetActive(true);
    }

    private void VideoPlay()
    {
        videoIsPlaying = true;
        videoPlayer.Play();
        btnPause.SetActive(true);
        btnPlay.SetActive(false);
    }
}
