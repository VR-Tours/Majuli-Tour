// using System.Reflection.PortableExecutable;
using System.Runtime.Versioning;
// using System.Reflection.Metadata;
using System.Transactions;
using System.Linq;
// using System.Numerics;
// using System.Runtime.Intrinsics;
// using System.Transactions;
// using System.Numerics;
// using System.Reflection.PortableExecutable;
using System.Linq.Expressions;
using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEditor;



public class followView : MonoBehaviour {
	bool mouseDown = false;
	float mouseX;
	float mouseY;
	List<string> linesAudio;
	List<string> lines;
	List<string> VidLines;
	List<string> NotifLines;
	public InputActionProperty nextImageAction;
	public InputActionProperty togglePanel;

	Camera mainCamera;
	public Material skyboxMaterial;
	// public Transform trans;
	public Material skyboxMaterialVideo;
	public float locationChangeDuration=1.2f;
	public float StartLocationChangeDuration=1.2f;
	public float waitDaPls=0.0f;
	public VideoPlayer UniversalVideoPlayer;
    public string locName = "StartScreen";
    private string textureName = "";
    private int textureX = 0;
    private int textureY = 0;
	private float rot=0;
	private bool joyStickDiretionState;
	private bool panelState;
	private bool panelButtonState;
	private Vector3 oldCameraDirection;
	Material previousSkyboxMaterial;
	Material newSkyboxMaterial;
	public GameObject audio;
	public GameObject arrowhead;
	public GameObject panel;
	public GameObject info_box;
	public GameObject locText;
	public RawImage on;
    public RawImage off;
	// private get
	// Texture pranshu;


    public float AngleBetweenVectors(Vector3 v1, Vector3 v2)
    {
        // Calculate the projection of v1 onto the x-z plane
        Vector3 v1Projected = new Vector3(v1.x, 0f, v1.z);

        // Calculate the projection of v2 onto the x-z plane
        Vector3 v2Projected = new Vector3(v2.x, 0f, v2.z);

        // Calculate the dot product of v1Projected and v2Projected
        float dotProduct = Vector3.Dot(v1Projected, v2Projected);

        // Calculate the magnitudes of v1Projected and v2Projected
        float v1Mag = v1Projected.magnitude;
        float v2Mag = v2Projected.magnitude;

        // Calculate the angle between the two vectors in degrees
        float angle = Mathf.Acos(dotProduct / (v1Mag * v2Mag)) * Mathf.Rad2Deg;

        return angle;
    }


	IEnumerator ChangePostWait() {
		// var gameDevices = new List<UnityEngine.XR.InputDevice>();
		// UnityEngine.XR.InputDevices.GetDevices(gameDevices);
		// foreach (var device in gameDevices)
		// {
		// 	// UnityEngine.Debug.Log(device.name + device.characteristics);
		// }
		mainCamera = GetComponent<Camera>();
		textureX = 0;
    	textureY = 0;
		joyStickDiretionState =false;
		rot=0;
		UnityEngine.Debug.Log("forward!!!!");
		// mainCamera.transform.forward=Vector3.forward;
		// UnityEngine.Debug.Log(GetComponent<Transform>().rotation);
		// GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
		// UnityEngine.Debug.Log(GetComponent<Transform>().rotation);
		// transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		skyboxMaterialVideo.SetFloat("_Rotation",(83+AngleBetweenVectors(mainCamera.transform.forward,Vector3.forward))%360);

		skyboxMaterial.SetFloat("_Rotation",90);
		// Get the rendering settings
		// var renderingSettings;

		// Set the skybox material
		// UnityEngine.RenderSettings.skybox = skyboxMaterialVideo;

		// Set the lighting settings
		// renderingSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
		// renderingSettings.ambientLight = Color.white;
		// renderingSettings.ambientIntensity = 1.0f;



		// skyboxMaterial.SetTexture("_Tex", Resources.Load<Texture>());

		// pranshu = Resources.Load<Texture>(locName + "/" + "0,0");
		//  Texture2D kandoi = (Texture2D)pranshu;
		// pranshu = (Texture)kandoi;

		// skyboxMaterial.SetTexture("_Tex", pranshu);
		yield return new WaitForSeconds(waitDaPls);
		if(locName != "StartScreen"){
			UnityEngine.Debug.Log("Not at start");
			on.enabled=true;
			off.enabled=false;
		}
		else{
			UnityEngine.Debug.Log(" at start");
		}
		// StreamReader audio_reader = new StreamReader("Assets/Resources/audio_swee/"+locName+".txt");
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
				
				
				if (textureX==0 && textureY==0)
				{
					
					AudioSource ad_src = GetComponent<AudioSource>();
					AudioClip new_clip  = Resources.Load<AudioClip>("audio_swee" + "/"+audio_file);	
					if(new_clip!=null)		
						ad_src.clip = new_clip;

					if(on.enabled == true)
						ad_src.Play();
					
					//Debug.Log("music set to "+audio_file);
					break; // Exit the loop since we found a match
				}
			}
		}
		else{
				AudioSource ad_src = GetComponent<AudioSource>();
				// AudioClip new_clip  = Resources.Load<AudioClip>("audio_swee" + "/"+audio_file);	
				// if(new_clip!=null)		
				ad_src.clip = null;
				if(on.enabled == true)
					ad_src.Play();
		}

		UnityEngine.Debug.Log("first call");
		arrowhead.SetActive(true);

		waitDaPls=0f;
		UniversalVideoPlayer.Pause();
		UniversalVideoPlayer.time = 0;


		
		if(locName.Split(".").Length>1)
		{
			locText.GetComponent<Location_text>().EnableText();
			locText.GetComponent<Location_text>().SetText(locName.Split(".")[1]);
		}	
		// If the texture dimensions is not "Cube", we convert it
        // TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath("Assets/Resources/"+locName + "/" + "0,0.jpg");
        // if (tex.dimension != UnityEngine.Rendering.TextureDimension.Cube)
        // {
		// importer.textureShape = TextureImporterShape.TextureCube;
		// importer.SaveAndReimport();
        // }
		if(Resources.Load(locName + "/" +"0,0")){
			UnityEngine.Debug.Log("image accessed at start:"+locName);
		}
		skyboxMaterial.SetTexture("_MainTex", Resources.Load<Texture2D>(locName + "/" + "0,0"));
		// skyboxMaterial.SetTexture("_Tex", Resources.Load<Texture>(locName + "/360rt"));
		oldCameraDirection=mainCamera.transform.forward;
		//do nothing
		UnityEngine.RenderSettings.skybox = skyboxMaterial;
		// return;
	}
	public int getX(){
		return textureX;
	}
	public int getY(){
		return textureY;
	}

	public void Start () {
		on.enabled = false;
		off.enabled = true;
		UnityEngine.Debug.Log("set as needed");

		panelButtonState=false;
		panelState=false;
		panel.SetActive(panelState);

		
		// transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        // transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		StartCoroutine(ChangePostWait());
	}

	// Material previousSkyboxMaterial;
	// Material newSkyboxMaterial;

	void UpdateSkyboxTexture(Texture closestTexture)
	{
		previousSkyboxMaterial = skyboxMaterial;
		newSkyboxMaterial = new Material(skyboxMaterial);
		newSkyboxMaterial.SetTexture("_Tex", closestTexture);
		// Texture currentTexture = RenderSettings.skybox.GetTexture("_Tex");
		UnityEngine.Debug.Log("name of closest texture:"+closestTexture.name);
		StartCoroutine(CrossFadeSkybox(previousSkyboxMaterial, newSkyboxMaterial, 1f));
		skyboxMaterial = newSkyboxMaterial;
	}

	IEnumerator CrossFadeSkybox(Material from, Material to, float duration)
	{
		float t = 0f;
		while (t < duration)
		{
			t += Time.deltaTime;
			float fadeOutBlend = Mathf.Clamp01(t / duration);
			float fadeInBlend = 1f - fadeOutBlend;
			from.SetFloat("_Exposure", fadeOutBlend);
			to.SetFloat("_Exposure", fadeInBlend);
			yield return null;
		}
		from.SetFloat("_Exposure", 1f);
		to.SetFloat("_Exposure", 0f);
	}


	void Update () {
		float rightJoyPress = togglePanel.action.ReadValue<float>();
		if(rightJoyPress==0){
			panelButtonState=false;
		}
		if(rightJoyPress==1&&panelButtonState==false){
			panelButtonState=true;
			panelState = !panelState;
			panel.SetActive(panelState);
		}

		
		// if()
		
		// {
		// 	Arrows.SetActiveRecursively(false);
		// }
		Vector2 joyStickDiretion = nextImageAction.action.ReadValue<Vector2>();
		// UnityEngine.Debug.Log(joyStickDiretion);
		float angleBwForwardAndController = Vector2.Dot(joyStickDiretion, Vector2.up);

		// bool triggerValue;
		// if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
		// {
		// 	UnityEngine.Debug.Log("Trigger button is pressed");
		// }
		if(Input.GetMouseButtonDown(0) && !mouseDown )
		{                
			mouseDown = true;
			mouseX = Input.mousePosition.x;
			mouseY = Input.mousePosition.y;
		}
		else if(Input.GetMouseButtonUp(0) && mouseDown)
		{                			
			mouseDown = false;
		}
		if(!(angleBwForwardAndController>0.85)){
			joyStickDiretionState = false;
		}
		// if (Input.GetKeyDown(KeyCode.UpArrow)) {
		if((angleBwForwardAndController>0.85) && !joyStickDiretionState){
		joyStickDiretionState = true;
		Vector3 cameraDirectionAbs = mainCamera.transform.forward;
		Texture currentTexture=skyboxMaterial.GetTexture("_MainTex");
		float rot = skyboxMaterial.GetFloat("_Rotation");
		// rot+=900;
		UnityEngine.Debug.Log("rotation is:"+rot);
		rot=0;
		string frontTextureName = textureName + textureX + "," + (textureY + 1);
		//UnityEngine.Debug.Log(locName + "/" + frontTextureName);
        string backTextureName = textureName + textureX + "," + (textureY - 1);
        string leftTextureName = textureName + (textureX - 1) + "," + textureY;
        string rightTextureName = textureName + (textureX + 1) + "," + textureY;

        Texture frontTexture = Resources.Load<Texture>(locName + "/" + frontTextureName);
        // if(Resources.Load<Texture>(locName + "/" + frontTextureName)){
		// 	Debug.Log("found forward texture");
		// }
		Texture backTexture = Resources.Load<Texture>(locName + "/" + backTextureName);
        Texture leftTexture = Resources.Load<Texture>(locName + "/" + leftTextureName);
        Texture rightTexture = Resources.Load<Texture>(locName + "/" + rightTextureName);

		// Quaternion rotation = Quaternion.FromToRotation(oldCameraDirection, Vector3.forward); // Create a rotation that aligns with the old camera direction
		Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.forward); // Create a rotation that aligns with the old camera direction

		Vector3 forward = rotation * Vector3.forward; // Define the forward direction with respect to the old camera direction
		Vector3 backward = rotation * Vector3.back; // Define the backward direction with respect to the old camera direction
		Vector3 left = rotation * Vector3.left; // Define the left direction with respect to the old camera direction
		Vector3 right = rotation * Vector3.right; // Define the right direction with respect to the old camera direction
		Vector3 up = rotation * Vector3.up; // Define the up direction with respect to the old camera direction
		Vector3 down = rotation * Vector3.down; // Define the down direction with respect to the old camera direction

		float dotForward = Vector3.Dot(cameraDirectionAbs, forward);
		float dotBackward = Vector3.Dot(cameraDirectionAbs, backward);
		float dotLeft = Vector3.Dot(cameraDirectionAbs, left);
		float dotRight = Vector3.Dot(cameraDirectionAbs, right);
		float dotUp = Vector3.Dot(cameraDirectionAbs, up);
		float dotDown = Vector3.Dot(cameraDirectionAbs, down);

		float maxDot = Mathf.Max(dotForward, dotBackward, dotLeft, dotRight, dotUp, dotDown);

        Vector3 cameraDirection = cameraDirectionAbs;
        float closestDot = -1f;
        Texture closestTexture = currentTexture;

		if (maxDot==dotForward) {
            closestDot = Vector3.Dot(cameraDirection, Vector3.up);
			UnityEngine.Debug.Log("up");
            closestTexture = currentTexture;
		}
        if (maxDot==dotDown) {
            closestDot = Vector3.Dot(cameraDirection, Vector3.down);
           UnityEngine.Debug.Log("down");
			closestTexture = currentTexture;
        }
        if (maxDot==dotLeft) {
            closestDot = Vector3.Dot(cameraDirection, Vector3.left);
           UnityEngine.Debug.Log("L");
			closestTexture = leftTexture;
        }
        if (maxDot==dotRight) {
            closestDot = Vector3.Dot(cameraDirection, Vector3.right);
           UnityEngine.Debug.Log("R");
			closestTexture = rightTexture;
        }
        if (maxDot==dotForward) {
            closestDot = Vector3.Dot(cameraDirection, Vector3.forward);
           UnityEngine.Debug.Log("F");
			closestTexture = frontTexture;
        }
        if (maxDot==dotBackward) {
            closestDot = Vector3.Dot(cameraDirection, Vector3.back);
           UnityEngine.Debug.Log("B");
			closestTexture = backTexture;
        }
		UnityEngine.Debug.Log("Texture x and texture y " + textureX + textureY);
		UnityEngine.Debug.Log(skyboxMaterial);
		if(closestTexture && closestTexture!=currentTexture){
			UnityEngine.Debug.Log("Currrrrrrr");
			oldCameraDirection=mainCamera.transform.forward;
			// UpdateSkyboxTexture(closestTexture);

        	skyboxMaterial.SetTexture("_MainTex", closestTexture);
			int textureX_old = int.Parse(currentTexture.name.Split(',')[0]);
			int textureY_old = int.Parse(currentTexture.name.Split(',')[1]);
			textureX = int.Parse(closestTexture.name.Split(',')[0]);
			textureY = int.Parse(closestTexture.name.Split(',')[1]);

			// StreamReader audio_reader = new StreamReader("Assets/Resources/audio_swee/"+locName+".txt");
			// print(audio_reader);
			// TextAsset mytxtData=(TextAsset)Resources.Load("/audio_swee/"+locName+".txt");
			// List<string> lines = new List<string>(mytxtData.text.Split('\n'));
		
			
			string old_audio = "";
			// Read each line of the file
			
			
			
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
				
				if(x1 == textureX_old && y1 == textureY_old){
					old_audio=audio_file;
					//Debug.Log("music set to "+audio_file);
					break; // Exit the loop since we found a match
				}
			}
			// StreamReader audio_reader2 = new StreamReader("Assets/Resources/audio_swee/"+locName+".txt");
			// TextAsset mytxtData=(TextAsset)Resources.Load("/audio_swee/"+locName+".txt");
			// List<string> lines = new List<string>(mytxtData.text.Split('\n'));
		
			foreach (string line in linesAudio)
			{
				// string line = audio_reader2.ReadLine();

				// Split the line into its components
				string[] components = line.Split(',');

				// Parse the integers and rotation value
				int x1 = int.Parse(components[0]);
				int y1 = int.Parse(components[1]);
				// int x2 = int.Parse(components[2]);
				// int y2 = int.Parse(components[3]);
				string audio_file = components[2];
				
				
				if (x1 == textureX && y1 == textureY)
				{
				UnityEngine.Debug.Log(old_audio+" "+audio_file);
					if(old_audio!=audio_file){
						AudioSource ad_src = GetComponent<AudioSource>();
						AudioClip new_clip  = Resources.Load<AudioClip>("audio_swee" + "/"+audio_file);			
						ad_src.clip = new_clip;
						if(on.enabled == true)
							ad_src.Play();
					}
					
				
					break; // Exit the loop since we found a match
				}
			}



			// Open the file for reading
			// StreamReader reader = new StreamReader("Assets/Resources/"+locName+"/textures_rotations_"+locName+".txt");
			// Stream reader;
			TextAsset mytxtData=(TextAsset)Resources.Load(locName+"/textures_rotations_"+locName);
			if(mytxtData)
				lines = new List<string>(mytxtData.text.Split('\n'));
			// Read each line of the file
			foreach (string line in lines)
			{
				// string line = reader.ReadLine();

				// Split the line into its components
				string[] components = line.Split(',');

				// Parse the integers and rotation value
				int x1 = int.Parse(components[0]);
				int y1 = int.Parse(components[1]);
				// int x2 = int.Parse(components[2]);
				// int y2 = int.Parse(components[3]);
				float rota = float.Parse(components[2]);
				
				// Check if the texture coordinates match
				if (x1 == textureX && y1 == textureY)
				{
					// Set the rotation value
					rot = (rota)%360;
					UnityEngine.Debug.Log("rotation set to "+rot);
					break; // Exit the loop since we found a match
				}
			}
			
			TextAsset NotifInfo=(TextAsset)Resources.Load(locName+"/textures_notifs_"+locName);
			NotifLines = new List<string>(NotifInfo.text.Split('\n'));
			// UnityEngine.Debug.log()
			// Read each line of the file
			int no_infobox=1;
			foreach (string line in NotifLines)
			{
				// string line = reader.ReadLine();
				
				// Split the line into its components
				string[] components = line.Split(',');
				if(components.Length<=2)
				{
					break;
				}

				// Parse the integers and rotation value
				int x1 = int.Parse(components[0]);
				int y1 = int.Parse(components[1]);
				// int x2 = int.Parse(components[2]);
				// int y2 = int.Parse(components[3]);
				string type = components[2];

				// Check if the texture coordinates match
				if (x1 == textureX && y1 == textureY)
				{
					UnityEngine.Debug.Log("Type is "+type);
					GameObject.FindWithTag("DynamicCanvas").GetComponent<aadu_infobox>().GetNotif(type);
					no_infobox=0;
					panelState = true;
					panel.SetActive(panelState);
					break;
				}
			}
			if(no_infobox==1)
			{
				GameObject.FindWithTag("DynamicCanvas").GetComponent<aadu_infobox>().GetNotif("DefaultInfobox");
				GameObject.FindWithTag("DynamicCanvas").GetComponent<aadu_infobox>().closeNotif();
				panelState = false;
				panel.SetActive(false);
			}

			TextAsset VidInfo=(TextAsset)Resources.Load(locName+"/textures_video_"+locName);
			if(VidInfo!=null)
				VidLines = new List<string>(VidInfo.text.Split('\n'));
			else
			{
				VidLines=null;
			}
			// UnityEngine.Debug.log()
			// Read each line of the file
			int no_video=1;
			foreach (string line in VidLines)
			{
				// string line = reader.ReadLine();
				
				// Split the line into its components
				string[] components = line.Split(',');
				if(components.Length<=2)
				{
					break;
				}

				// Parse the integers and rotation value
				int x1 = int.Parse(components[0]);
				int y1 = int.Parse(components[1]);
				// int x2 = int.Parse(components[2]);
				// int y2 = int.Parse(components[3]);
				string type = components[2];

				// Check if the texture coordinates match
				if (x1 == textureX && y1 == textureY)
				{
					UnityEngine.Debug.Log("Type is "+type);
					GameObject.FindWithTag("DynamicCanvas").GetComponent<aadu_infobox>().getVideo(type);
					no_video=0;
					panelState = true;
					panel.SetActive(panelState);
					break;
				}
			}
			if(no_video==1 && no_infobox==1)
			{
				GameObject.FindWithTag("DynamicCanvas").GetComponent<aadu_infobox>().getVideo("INTRO");
				GameObject.FindWithTag("DynamicCanvas").GetComponent<aadu_infobox>().closeNotif();
				panelState = false;
				panel.SetActive(false);
			}

			GameObject.FindWithTag("DynamicCanvas").GetComponent<aadu_infobox>().Reset();
			// Close the file
			// reader.Close();
			//UnityEngine.Debug.Log(textureX+"   "+textureY);
			UnityEngine.Debug.Log("Skybox texture changed to " + closestTexture.name);
			// rot=; file read mapping depending on old_x old_y and textureX textureY

			skyboxMaterial.SetFloat("_Rotation",(rot+90)%360);
		}
		}
    }
	

	void LateUpdate()
	{
		if(mouseDown)
		{
			float mouseXStop = Input.mousePosition.x;
			float mouseYStop = Input.mousePosition.y;
			float deltaX = mouseXStop - mouseX;
			float deltaY = mouseYStop - mouseY;
			float centerXNew = Screen.width / 2 + deltaX;
			float centerYNew = Screen.height / 2 + deltaY;

			Vector3 Gaze = mainCamera.ScreenToWorldPoint(new Vector3(centerXNew, centerYNew, mainCamera.nearClipPlane));
			transform.LookAt(Gaze);
			mouseX = mouseXStop;
			mouseY = mouseYStop;
		}
	}
}
