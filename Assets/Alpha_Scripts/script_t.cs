using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class script_t : MonoBehaviour
{

    public static float x, y, z;
    public static bool flip;
    private ImageTargetBehaviour mImageTargetBehaviour = null;

    // Use this for initialization
    void Start()
    {
        // We retrieve the ImageTargetBehaviour component
        // Note: This only works if this script is attached to an ImageTarget
        mImageTargetBehaviour = GetComponent<ImageTargetBehaviour>();

        if (mImageTargetBehaviour == null)
        {
            Debug.Log("ImageTargetBehaviour not found ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cornerInLocalRef = new Vector3(0.5f, 0, 0.5f);
        // Convert from local ref to world ref
        Vector3 cornerInWorldRef = this.transform.TransformPoint(cornerInLocalRef);
        // Convert from world ref to camera ref
        Vector3 cornerInCameraRef = Camera.main.transform.InverseTransformPoint(cornerInWorldRef);
        //		Debug.Log ("For A = " + cornerInCameraRef.y);
        if (cornerInCameraRef.y > 0)
            flip = false;
        else
            flip = true;
        StateManager sm = TrackerManager.Instance.GetStateManager();
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();
        int count = 0;
        foreach (TrackableBehaviour tb in activeTrackables)
        {

            //               TRACKABLE NAME NEEDS TO BE CHANGED

            if (tb.TrackableName == "t")
                count = 1;
        }
        if (count == 1)
        {
            mImageTargetBehaviour = GetComponent<ImageTargetBehaviour>();

            if (mImageTargetBehaviour == null)
            {
                Debug.Log("ImageTargetBehaviour not found ");
                return;
            }
            Vector2 targetSize = mImageTargetBehaviour.GetSize();
            float targetAspect = targetSize.x / targetSize.y;

            // We define a point in the target local reference 
            // we take the bottom-left corner of the target, 
            // just as an example
            // Note: the target reference plane in Unity is X-Z, 
            // while Y is the normal direction to the target plane
            Vector3 pointOnTarget = new Vector3(-0.5f, 0, -0.5f / targetAspect);

            // We convert the local point to world coordinates
            Vector3 targetPointInWorldRef = transform.TransformPoint(pointOnTarget);

            // We project the world coordinates to screen coords (pixels)
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(targetPointInWorldRef);

         //   			Debug.Log ("FOR LETTER A : " + screenPoint.x + ", " + screenPoint.y + ", " + screenPoint.z);
            x = screenPoint.x;
            y = screenPoint.y;
            z = screenPoint.z;
        }
        else
            x = y = z = 0;
    }
}