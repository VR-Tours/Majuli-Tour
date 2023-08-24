using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
public class Knob : MonoBehaviour
{
    public GameObject videoPlayer;
    private MyVideoPlayer videoPlayerScript;

    // public XRGrabInteractable grabInteractable;
    public InputActionProperty knobDrag;
    public InputActionProperty videoforwardbackwardaction;
    private bool isDraggingKnob = false;


    // private void Start()
    // {
    //     // Get the XRGrabInteractable component attached to this object
    //     grabInteractable = GetComponent<XRGrabInteractable>();
    // }
    void Start()
    {
        // grabInteractable = GetComponent<XRGrabInteractable>();
        videoPlayerScript = videoPlayer.GetComponent<MyVideoPlayer>();
    }
    /*
    private void OnEnable()
    {
        // Subscribe to the OnSelectEntered and OnSelectExited events
        grabInteractable.onSelectEntered.AddListener(OnSelectEntered);
        grabInteractable.onSelectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnSelectEntered and OnSelectExited events
        grabInteractable.onSelectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.onSelectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(XRBaseInteractor interactor)
    {
        // Create a ray from the interactor's position and direction
        Ray ray = new Ray(interactor.transform.position, interactor.transform.forward);

        // Create a RaycastHit variable to store information about the hit
        RaycastHit hit;

        // Check if the ray hits a collider on this object
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the collider hit is on this object
            if (hit.collider.gameObject == gameObject)
            {
                // Set the isDragging flag to true
                isDragging = true;

                videoPlayerScript.KnobOnPressDown();

                // Do something when the object is clicked
                Debug.Log("Object clicked!");
            }
        }
    }

    private void OnSelectExited(XRBaseInteractor interactor)
    {
        // Set the isDragging flag to false
        isDragging = false;
        videoPlayerScript.KnobOnRelease();

        // Do something when the object is released
        Debug.Log("Object released!");
    }

    private void Update()
    {
        // Check if the object is being dragged
        if (isDragging)
        {
            // Get the position of the interactor
            Vector3 interactorPosition = grabInteractable.selectingInteractor.transform.position;

            // Set the position of the object to the position of the interactor
            transform.position = interactorPosition;
            videoPlayerScript.KnobOnDrag();

            // Do something while the object is being dragged
            Debug.Log("Object dragged!");
        }
    }
    */


    void Update(){
            // videoPlayerScript.KnobOnDrag();

            RaycastHit hit;
            // UnityEngine.XR.InputDevice device =  UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.LeftHand); // get a reference to the left hand controller
            Vector3 controllerPosition = videoPlayerScript.interactor_ctrl.transform.position;
            Vector3 controllerDirection =videoPlayerScript.interactor_ctrl.transform.rotation*Vector3.forward;
            Physics.Raycast(controllerPosition,controllerDirection,out hit);
            
            float knobSelect = knobDrag.action.ReadValue<float>();

            // || (hitCollider2.CompareTag(cinemaPlane.tag)&& videoIsPlaying)

            // Collider collider = hit.collider;
            // UnityEngine.Debug.Log("Collider hit: " + collider.name);

            // // Print the dimensions of the collider
            // Bounds bounds = collider.bounds;
            // UnityEngine.Debug.Log("Collider dimensions: " + bounds.size);

            // // Print the position of the collider
            // Vector3 position = collider.transform.position;
            // UnityEngine.Debug.Log("Collider position: " + position);
            // print("pauseBtn");
            // BtnPlayVideo();
        Vector2 joyStickDiretionleft =  videoforwardbackwardaction.action.ReadValue<Vector2>(); 

        float angleBwRightAndController = Vector2.Dot(joyStickDiretionleft, Vector2.left);
        float angleBwLeftAndController = Vector2.Dot(joyStickDiretionleft, Vector2.right);
        int flag = 0;
        if (angleBwRightAndController>0.65){
            flag = 1;
        }else if(angleBwLeftAndController > 0.65){
            flag = 1;
        }
        // UnityEngine.Debug.log(flag);

            if(flag == 1&&isDraggingKnob==false){
                UnityEngine.Debug.Log("first call");
                // if (hit.collider != null && (hit.collider.CompareTag(videoPlayerScript.knob.tag)) )
                // {
                    videoPlayerScript.KnobOnPressDown();
                // }
                isDraggingKnob=true;
            }
            if(flag == 0&&isDraggingKnob==true){
                UnityEngine.Debug.Log("released call");
                // if (hit.collider != null && (hit.collider.CompareTag(videoPlayerScript.knob.tag)) )
                // {
                    
                    videoPlayerScript.KnobOnRelease();
                // }
                isDraggingKnob=false;
            }
            if(flag == 1){
            if (angleBwRightAndController>0.65){
                videoPlayerScript.KnobOnDrag1();
            }else if(angleBwLeftAndController > 0.65){
                videoPlayerScript.KnobOnDrag();
            }
                UnityEngine.Debug.Log("continous call");
                // videoPlayerScript.KnobOnDrag();
            }
        
    }
}
