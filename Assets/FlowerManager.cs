using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class FlowerManager : MonoBehaviour
{
	[SerializeField] GameObject flowerPrefab;
    [SerializeField] GameObject raindropPrefab;
	[SerializeField] ARSessionOrigin arSessionOrigin;

    ARRaycastManager arRaycastManager;
    Camera cameraOrigin;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        arRaycastManager = arSessionOrigin.GetComponent<ARRaycastManager>();
        cameraOrigin = arSessionOrigin.GetComponentInChildren<Camera>();
    }

    bool isTouchOverUI(Vector2 pos)
    {
        if (EventSystem.current == null) return false;

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = new Vector2(pos.x, pos.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void ShootRaindrop()
    {
        GameObject newRaindrop = Instantiate<GameObject>(raindropPrefab);
        newRaindrop.transform.position = cameraOrigin.transform.position; // Shoot the raindrop from the camera position
        Rigidbody rb = newRaindrop.GetComponent<Rigidbody>();
        rb.AddForce(1000 * cameraOrigin.transform.forward); // adjust the force til it feels right.
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch = Input.GetTouch(0);

        if(arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon)
            && !isTouchOverUI(touch.position)) {
            Pose hitPose = hits[0].pose;

            Instantiate(flowerPrefab, hitPose.position, hitPose.rotation);
        }

    }
}
