using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    public float range = 2;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitRaycast = new RaycastHit();
        bool rayState = Physics.Raycast(playerCam.ScreenPointToRay(Input.mousePosition), out hitRaycast, range);
        if (rayState)
        {
            GameObject objInRange = hitRaycast.transform.gameObject;
            if (Input.GetMouseButtonDown(0))
            {
                objInRange.GetComponent<InterrupteurObj>().Interact();
            }
        }
    }
}
