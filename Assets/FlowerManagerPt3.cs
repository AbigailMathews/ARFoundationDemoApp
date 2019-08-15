using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class FlowerManagerPt3 : MonoBehaviour
{

    [SerializeField]
    GameObject flowerPrefab;
    [SerializeField]
    ARSessionOrigin arSessionOrigin;

    ARRaycastManager arRaycastManager;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = arSessionOrigin.GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch = Input.GetTouch(0);

        if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            Instantiate(flowerPrefab, hitPose.position, hitPose.rotation);
        }
    }
}
