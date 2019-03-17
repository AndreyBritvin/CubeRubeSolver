using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePieceScript : MonoBehaviour {

    //public GameObject UpPlane,DownPlane,
    //					RightPlane,LeftPlane,
    //					FrontPlane,BackPlane;
    int SizeCube = CubeManager.SizeSideCube;
    public List<GameObject> Planes = new List<GameObject>();
	// Use this for initialization
	public void SetColor(int x, int y, int z)
	{
        //Debug.Log(x+" "+ y+" " +z+" " + SizeCube);
		if(y == 0)
				Planes[0].SetActive(true);
		else if(y == (-SizeCube)+1)
				Planes[1].SetActive(true);
		if(z == 0)
				Planes[2].SetActive(true);
		else if(z == SizeCube-1)
				Planes[3].SetActive(true);
		if(x == 0)
				Planes[4].SetActive(true);
		else if(x == (-SizeCube)+1)
				Planes[5].SetActive(true);
	}
}
