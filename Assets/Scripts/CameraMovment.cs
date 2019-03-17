using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour {
    Vector3 localRotation = new Vector3(-40,40,0);
    bool isDisabled = false,
    canRotateSide = false;


    public CubeManager CubeMan;


    public List<GameObject> pieces = new List<GameObject>(),
                     planes = new List<GameObject>();

    // Use this for initialization

	
	// Update is called once per frame
	void LateUpdate () {
        if (Input.GetMouseButton(0))
        {   if(!canRotateSide)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, 100))
                {
                    isDisabled = true;

                    if(pieces.Count<2 &&
                        !pieces.Exists(x=>x == hit.collider.transform.parent.gameObject) &&
                       hit.transform.parent.gameObject != CubeMan.gameObject)
                    {
                        pieces.Add(hit.collider.transform.parent.gameObject);
                        planes.Add(hit.collider.gameObject);
                    }
                    else if (pieces.Count == 2)
                    {
                        CubeMan.DetectRotate(pieces, planes);
                        canRotateSide = true;
                    }
                }
            }
            if (!isDisabled)
            {
                isDisabled = false;
                localRotation.x += Input.GetAxis("Mouse X") * 15;
                localRotation.y += Input.GetAxis("Mouse Y") * -15;
                //нет перевращения кубика(нельзя развернууть его на 360 градусов)
                localRotation.y = Mathf.Clamp(localRotation.y, -90, 90); 
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            pieces.Clear();
            planes.Clear();
            isDisabled =canRotateSide= false;
        }
        Quaternion qt = Quaternion.Euler(localRotation.y, localRotation.x, 0);
        transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, qt, Time.deltaTime * 15);
	}
}
