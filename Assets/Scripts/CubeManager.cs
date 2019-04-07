using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeManager : MonoBehaviour {
    public GameObject CubePiecePref;
    public bool IsSolve = false;
    public bool isUnderstand = false;
    Transform CubeTransf;
    public static int SizeSideCube = 3;
    List<GameObject> AllCubePieces = new List<GameObject>();
    List<Vector3> AllCubePieces_copy = new List<Vector3>();
    GameObject CubeCenterPiece;
    int numOfRotations = 0;
    public GameObject infoTextGameObj;
    int indexCenter;
    
    bool canRotate = true,
        canShuffle = true;
    public GameObject cam;
    public string inProcess = " в процессе";
    public string ready = " готово";
    public Text infoText;
    public Text solveText;
    public GameObject YesButton;
    public GameObject ExitButton;
    public int speed = 5;

    Vector3[] RotationVectors ={
        new Vector3(0,1,0),
        new Vector3(0,-1,0),
        new Vector3(0,0,-1),
        new Vector3(0,0,1),
        new Vector3(1,0,0),
        new Vector3(-1,0,0)


    };
    Vector3[] RotationVectorsForCenter ={
        new Vector3(1,0,0),
        new Vector3(0,0,1),
        new Vector3(0,1,0),
        new Vector3(-1,0,0),
        new Vector3(0,0,-1),
        new Vector3(0,-1,0)


    };
    List<GameObject> UpPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == 0);
        }
    }
    List<GameObject> DownPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == -2);
        }
    }


    List<GameObject> LeftPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 0);
        }
    }
    List<GameObject> RightPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 2);
        }
    }
    List<GameObject> BackPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == -2);
        }
    }
    List<GameObject> FrontPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == 0);
        }
    }

    List<GameObject> UpHorizontal
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == -1);
        }
    }
    List<GameObject> UpVertical
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 1);
        }
    }
    List<GameObject> FrontHorizontalPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == -1);
        }
    }
    List<GameObject>[,] centralPieces = new List<GameObject>[3, SizeSideCube]; // [0] = frontHorizontal; [1] = [UpVertical]; [2] = UpHorizontals
    public List<List<GameObject>> sides = new List<List<GameObject>>();
    List<List<GameObject>> centralSides = new List<List<GameObject>>();
    
    void getCentralPieces()
    {
        /*
        List<string>[,] nameval = new List<string>[6,6];
        nameval[0,0] = new List<string>();
        nameval[2, 0] = new List<string>();
        nameval[0, 0].Add("A");
        nameval[2, 0].Add("B");
        nameval[2, 0].Add("C");
        Debug.Log("Name val = "+nameval[2,0]);
        nameval[2, 0].ForEach(System.Console.WriteLine);
        foreach(string val in nameval[2,0])
        {
            Debug.Log(val);
        }*/
        for (int i = 0; i <= 2; i++)
        {

            for (int j = 1; j < SizeSideCube - 1; j++)
            {
                if (i == 0)
                {
                    // centralPieces[i, j] = 
                    // Debug.Log(AllCubePieces[0]);

                    //   Debug.Log(AllCubePieces.FindAll(x => x.transform.localPosition.y == -2));

                    List<GameObject> value = AllCubePieces.FindAll(x => x.transform.localPosition.y == -j);
                    centralPieces[i, j] = new List<GameObject>();
                    centralPieces[i, j] = value;

                }
                else if (i == 1)
                {
                    List<GameObject> val = AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == j);

                    //  Debug.Log("List_COunt = " + val.Count);
                    //    Debug.Log("List_Capacity = " + val.Capacity);


                    centralPieces[i, j] = new List<GameObject>();
                    centralPieces[i, j] = val;




                    
                }

                else if (i == 2)
                {
                    centralPieces[i, j] =
                        AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == -j);
                }

            }

        }
        // Debug.Log(centralPieces);
    }

    
    void Start() {
        CubeTransf = transform;

        CreateCube();
        getCentralPieces();
    }

    void Update() {


        if (canRotate) {
            CheckInput();
        } }
    public void CreateCube()
    {
        foreach (GameObject go in AllCubePieces)
        {
            DestroyImmediate(go);
        }
        AllCubePieces_copy.Clear();
        AllCubePieces.Clear();
        for (int x = 0; x < SizeSideCube; x++)
            for (int y = 0; y < SizeSideCube; y++)
                for (int z = 0; z < SizeSideCube; z++)
                {
                    
                    GameObject go = Instantiate(CubePiecePref, CubeTransf, false);
                    go.transform.localPosition = new Vector3(-x, -y, z);
                    go.GetComponent<CubePieceScript>().SetColor(-x, -y, z);
                    //AllCubePieces_copy.Add(go);
                    AllCubePieces_copy.Add(go.transform.position);
                    AllCubePieces.Add(go);
                }
       
        indexCenter = (int)Mathf.Floor((SizeSideCube * SizeSideCube * SizeSideCube) / 2);
        CubeCenterPiece = AllCubePieces[indexCenter];
    }
    void CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            resetList();

            StartCoroutine(BuildWhiteKrest(sides));
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {

            resetList();
            StartCoroutine(BuildWhiteCorner(sides));
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            resetList();
            StartCoroutine(BuildColourRebra(sides));
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            resetList();
            StartCoroutine(ReturnAndPlaceOnTruePlaceYellowRebra(sides));
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            resetList();
                  StartCoroutine(ReturnAndPlaceOnTruePlaceYellowCorners(sides));
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
             StartCoroutine(buildCube());
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            resetList();
            StartCoroutine(PutCenterPicesOnTheirPlaces(sides));
        }
        



            if (Input.GetKeyDown(KeyCode.W))
            StartCoroutine(Rotate(UpPieces, new Vector3(0, 1, 0)));

        else if (Input.GetKeyDown(KeyCode.S))
            StartCoroutine(Rotate(DownPieces, new Vector3(0, -1, 0)));

        else if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine(Rotate(LeftPieces, new Vector3(0, 0, -1)));

        else if (Input.GetKeyDown(KeyCode.D))
            StartCoroutine(Rotate(RightPieces, new Vector3(0, 0, 1)));

        else if (Input.GetKeyDown(KeyCode.B))
            StartCoroutine(Rotate(FrontPieces, new Vector3(1, 0, 0)));

        else if (Input.GetKeyDown(KeyCode.F))
            StartCoroutine(Rotate(BackPieces, new Vector3(-1, 0, 0)));

        else if (Input.GetKeyDown(KeyCode.R) && canShuffle)
            StartCoroutine(shuffle4());
        else if (Input.GetKeyDown(KeyCode.E) && canShuffle)
            CreateCube();

    }
    
   public IEnumerator buildCube()
   {
        solveText.text = "";
       resetList();
       yield return BuildWhiteKrest(sides);
       resetList();
       yield return BuildWhiteCorner(sides);
       resetList();
       yield return BuildColourRebra(sides);
       resetList();
       yield return ReturnAndPlaceOnTruePlaceYellowRebra(sides);
       resetList();
       yield return ReturnAndPlaceOnTruePlaceYellowCorners(sides);

   }


    IEnumerator morgat(GameObject cube)
    {
        Color baseColor = cube.GetComponent<Renderer>().material.color;
        for (int i = 0; i < 25; i++)
        {

            cube.GetComponent<Renderer>().material.color = new Color(1, 0.5f, 0.5f, 0);
            yield return new WaitForSeconds(.3f);
            cube.GetComponent<Renderer>().material.color = baseColor;
            yield return new WaitForSeconds(.3f);
        }
        cube.GetComponent<Renderer>().material.color = baseColor;
    }


    public IEnumerator shuffle4()
   {
        for (int i = 0; i<Buttons.qualShuffle;i++)
        {
            yield return Shuffle();
        }
   }
    IEnumerator Shuffle()
    {
        List<int> umn = new List<int>();
        umn.Add(-1);
        umn.Add(1);
        Debug.Log("InShuffle");
        canShuffle = false;
        for (int MoveCount = Random.Range(15, 30); MoveCount >= 0; MoveCount -= 1)
        {
            int edge = Random.Range(0, 6);
            List<GameObject> edgePieces = new List<GameObject>();

            switch (edge)
            {
                case 0: edgePieces = UpPieces; break;
                case 1: edgePieces = DownPieces; break;
                case 2: edgePieces = LeftPieces; break;
                case 3: edgePieces = RightPieces; break;
                case 4: edgePieces = FrontPieces; break;
                case 5: edgePieces = BackPieces; break;
            }
            int vec = umn[Random.Range(0, 1)];
            StartCoroutine(Rotate(edgePieces, vec*RotationVectors[edge], 45));
            yield return new WaitForSeconds(.1f);
        }
        canShuffle = true;

    }
    IEnumerator Rotate(List<GameObject> pieces, Vector3 rotationVec, int speed = 5)

    {
        //  print("CanRotate = " + canRotate);
        

        canRotate = false;
        int angle = 0;
        while (angle < 90)
        {
            foreach (GameObject go in pieces)
                go.transform.RotateAround(CubeCenterPiece.transform.position, rotationVec, speed);
            angle += speed;
            yield return null;
        }
        CheckComplete();
        canRotate = true;
        resetList();
    }
    
    public void resetList()
    {
        sides.Clear();
        centralSides.Clear();

        sides.Add(UpPieces);
        sides.Add(DownPieces);



        sides.Add(LeftPieces);

        sides.Add(RightPieces);

        sides.Add(FrontPieces);

        sides.Add(BackPieces);

        centralSides.Add(UpHorizontal);
        centralSides.Add(UpVertical);
        centralSides.Add(FrontHorizontalPieces);
    }
    public IEnumerator showAllPieces(List<GameObject> pieces)
    {
        if (IsSolve)
        {
            Coroutine lastRoutine = null;
            Coroutine lastRoutine1 = null;
            Coroutine lastRoutine2 = null;

            Coroutine lastRoutine3 = null;

            lastRoutine = StartCoroutine(morgat(pieces[0]));
            lastRoutine1 = StartCoroutine(morgat(pieces[1]));
            lastRoutine2 = StartCoroutine(morgat(pieces[2]));
            lastRoutine3 = StartCoroutine(morgat(pieces[3]));

            yield return WaitForClickButton();


            StopCoroutine(lastRoutine);
            StopCoroutine(lastRoutine1);
            StopCoroutine(lastRoutine2);
            StopCoroutine(lastRoutine3);
            for (int i = 0; i < 3; i++)
                pieces[i].GetComponent<Renderer>().material.color = CubeCenterPiece.GetComponent<Renderer>().material.color;
        }
    }
    void CheckComplete()
    {
        int whoStTrue = 0;
        foreach(GameObject Cubik in AllCubePieces)
        {
            if (IsPieceOnPlace(Cubik))
            {
                whoStTrue++;
                continue;
            }
            else
            {
                break;
            }
        }
        if(AllCubePieces.Count == whoStTrue)
        {
            print("Complete");
        }

    }
    void CheckComplete_dontwork()
    {
        if (SideComplete(UpPieces) &&
            SideComplete(DownPieces) &&
            SideComplete(LeftPieces) &&
            SideComplete(RightPieces) &&
            SideComplete(FrontPieces) &&
            SideComplete(BackPieces))
        {
            Debug.Log("Complete");
       //     StopAllCoroutines();
        }
    }
    bool SideComplete(List<GameObject> pieces)
    {
        int mainPlaneIndex = pieces[4].GetComponent<CubePieceScript>().Planes.FindIndex(x => x.activeInHierarchy);
        for (int i = 0; i < pieces.Count; i++)
        {
            if (!pieces[i].GetComponent<CubePieceScript>().Planes[mainPlaneIndex].activeInHierarchy ||
                pieces[i].GetComponent<CubePieceScript>().Planes[mainPlaneIndex].GetComponent<Renderer>().material.color !=
                pieces[4].GetComponent<CubePieceScript>().Planes[mainPlaneIndex].GetComponent<Renderer>().material.color)
                return false;
        }
        return true;
    }


    public void DetectRotate(List<GameObject> pieces, List<GameObject> planes)
    {
        if (!canShuffle || !canRotate)
            return;

        if (UpVertical.Exists(x => x == pieces[0]) &&
            UpVertical.Exists(x => x == pieces[1]))
        {
            StartCoroutine(Rotate(UpVertical, new Vector3(0, 0, 1 * DetectLeftMiddleRightSign(pieces))));
        }
        else if (FrontHorizontalPieces.Exists(x => x == pieces[0]) &&
            FrontHorizontalPieces.Exists(x => x == pieces[1]))
        {
            StartCoroutine(Rotate(FrontHorizontalPieces, new Vector3(0, 1 * DetectUpMiddleDownSign(pieces), 0)));
        }
        else if (UpHorizontal.Exists(x => x == pieces[0]) &&
            UpHorizontal.Exists(x => x == pieces[1]))
        {
            StartCoroutine(Rotate(UpHorizontal, new Vector3(1 * DetectFrontMiddleBackSign(pieces), 0, 0)));
        }



        else if (DetectSide(planes, new Vector3(1, 0, 0), new Vector3(0, 0, 1), UpPieces))
        {
            StartCoroutine(Rotate(UpPieces, new Vector3(0, 1 * DetectUpMiddleDownSign(pieces), 0)));
        }

        else if (DetectSide(planes, new Vector3(1, 0, 0), new Vector3(0, 0, 1), DownPieces))
        {
            StartCoroutine(Rotate(DownPieces, new Vector3(0, 1 * DetectUpMiddleDownSign(pieces), 0)));
        }
        else if (DetectSide(planes, new Vector3(0, 0, 1), new Vector3(0, 1, 0), FrontPieces))
        {
            StartCoroutine(Rotate(FrontPieces, new Vector3(1 * DetectFrontMiddleBackSign(pieces), 0, 0)));
        }
        else if (DetectSide(planes, new Vector3(0, 0, 1), new Vector3(0, 1, 0), BackPieces))
        {
            StartCoroutine(Rotate(BackPieces, new Vector3(1 * DetectFrontMiddleBackSign(pieces), 0, 0)));
        }

        else if (DetectSide(planes, new Vector3(1, 0, 0), new Vector3(0, 1, 0), LeftPieces))
        {
            StartCoroutine(Rotate(LeftPieces, new Vector3(0, 0, 1 * DetectLeftMiddleRightSign(pieces))));
        }

        else if (DetectSide(planes, new Vector3(1, 0, 0), new Vector3(0, 1, 0), RightPieces))
        {
            StartCoroutine(Rotate(RightPieces, new Vector3(0, 0, 1 * DetectLeftMiddleRightSign(pieces))));
        }
    }

    bool DetectSide(List<GameObject> planes, Vector3 fDirection, Vector3 sDirection, List<GameObject> side)
    {
        GameObject centerPiece = side.Find(x => x.GetComponent<CubePieceScript>().Planes.FindAll(y => y.activeInHierarchy).Count == 1);

        List<RaycastHit> hit1 = new List<RaycastHit>(Physics.RaycastAll(planes[1].transform.position, fDirection)),
                          hit2 = new List<RaycastHit>(Physics.RaycastAll(planes[0].transform.position, fDirection)),
                          hit1_m = new List<RaycastHit>(Physics.RaycastAll(planes[1].transform.position, -fDirection)),
                          hit2_m = new List<RaycastHit>(Physics.RaycastAll(planes[0].transform.position, -fDirection)),

                          hit3 = new List<RaycastHit>(Physics.RaycastAll(planes[1].transform.position, sDirection)),
                          hit4 = new List<RaycastHit>(Physics.RaycastAll(planes[0].transform.position, sDirection)),
                          hit3_m = new List<RaycastHit>(Physics.RaycastAll(planes[1].transform.position, -sDirection)),
                          hit4_m = new List<RaycastHit>(Physics.RaycastAll(planes[0].transform.position, -sDirection));
        return
            hit1.Exists(x => x.collider.gameObject == centerPiece) ||
            hit2.Exists(x => x.collider.gameObject == centerPiece) ||
            hit1_m.Exists(x => x.collider.gameObject == centerPiece) ||
            hit2_m.Exists(x => x.collider.gameObject == centerPiece) ||
            hit3.Exists(x => x.collider.gameObject == centerPiece) ||
            hit4.Exists(x => x.collider.gameObject == centerPiece) ||
            hit3_m.Exists(x => x.collider.gameObject == centerPiece) ||
            hit4_m.Exists(x => x.collider.gameObject == centerPiece);
    }

    float DetectLeftMiddleRightSign(List<GameObject> pieces) {
        float sign = 0;
        if (Mathf.Round(pieces[1].transform.position.y) != Mathf.Round(pieces[0].transform.position.y))
        {
            if (Mathf.Round(pieces[0].transform.position.x) == -2)
            {
                sign = Mathf.Round(pieces[0].transform.position.y) - Mathf.Round(pieces[1].transform.position.y);

            }
            else
            {
                sign = Mathf.Round(pieces[1].transform.position.y) - Mathf.Round(pieces[0].transform.position.y);
            }
        }
        else
        {
            if (Mathf.Round(pieces[0].transform.position.y) == -2)
            {
                sign = Mathf.Round(pieces[1].transform.position.x) - Mathf.Round(pieces[0].transform.position.x);

            }
            else
            {
                sign = Mathf.Round(pieces[0].transform.position.x) - Mathf.Round(pieces[1].transform.position.x);
            }
        }
        return sign;
    }

    float DetectFrontMiddleBackSign(List<GameObject> pieces)
    {
        float sign = 0;
        if (Mathf.Round(pieces[1].transform.position.z) != Mathf.Round(pieces[0].transform.position.z))
        {
            if (Mathf.Round(pieces[0].transform.position.y) == 0)
            {
                sign = Mathf.Round(pieces[1].transform.position.z) - Mathf.Round(pieces[0].transform.position.z);

            }
            else
            {
                sign = Mathf.Round(pieces[0].transform.position.z) - Mathf.Round(pieces[1].transform.position.z);
            }
        }
        else
        {
            if (Mathf.Round(pieces[0].transform.position.z) == 0)
            {
                sign = Mathf.Round(pieces[1].transform.position.y) - Mathf.Round(pieces[0].transform.position.y);

            }
            else
            {
                sign = Mathf.Round(pieces[0].transform.position.y) - Mathf.Round(pieces[1].transform.position.y);
            }
        }
        return sign;
    }
    float DetectUpMiddleDownSign(List<GameObject> pieces)
    {
        float sign = 0;
        if (Mathf.Round(pieces[1].transform.position.z) != Mathf.Round(pieces[0].transform.position.z))
        {
            if (Mathf.Round(pieces[0].transform.position.x) == -2)
            {
                sign = Mathf.Round(pieces[1].transform.position.z) - Mathf.Round(pieces[0].transform.position.z);

            }
            else
            {
                sign = Mathf.Round(pieces[0].transform.position.z) - Mathf.Round(pieces[1].transform.position.z);
            }
        }
        else
        {
            if (Mathf.Round(pieces[0].transform.position.z) == 0)
            {
                sign = Mathf.Round(pieces[0].transform.position.x) - Mathf.Round(pieces[1].transform.position.x);

            }
            else
            {
                sign = Mathf.Round(pieces[1].transform.position.x) - Mathf.Round(pieces[0].transform.position.x);
            }
        }
        return sign;
    }

    public IEnumerator WaitForClickButton()
    {
        YesButton.SetActive(true);
        ExitButton.SetActive(true);
        while (!isUnderstand)
        {
            
            yield return new WaitForSeconds(1);
        }
        
        isUnderstand = false;
        YesButton.SetActive(false);
    }

    public IEnumerator PutCenterPicesOnTheirPlaces(List<List<GameObject>> cubeSides)
    {
        if(IsSolve)
        {
            infoTextGameObj.SetActive(false);
        }
        else
        {
            infoTextGameObj.SetActive(true);
        }

        List<GameObject> centers = new List<GameObject>();






        for (int i = 0; i < cubeSides.Count; i++)
        {
            centers.AddRange(cubeSides[i].FindAll(x => x.GetComponent<CubePieceScript>().Planes.FindAll(y => y.activeInHierarchy).Count == 1));

        }
        GameObject whiteCube = new GameObject() ;
        for (int i = 0; i < 6; i++)
        {
            whiteCube = centers.Find(gg=>gg.GetComponent<CubePieceScript>().Planes[i].GetComponent<Renderer>().material.color == Color.white &&
                gg.GetComponent<CubePieceScript>().Planes[i].activeInHierarchy);
            if (whiteCube != null)
            {
                break;
            }
        }
        if(Mathf.Round(whiteCube.transform.position.y) == 0)
        {

        }
            else if(Mathf.Round(whiteCube.transform.position.y) == -1 )
        {
            if(Mathf.Round(whiteCube.transform.position.x) == 0 && Mathf.Round(whiteCube.transform.position.z) == 1)
            {
                yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
            }
            else if (Mathf.Round(whiteCube.transform.position.x) == -1 && Mathf.Round(whiteCube.transform.position.z) == 0)
            {
                yield return Rotate(UpHorizontal, new Vector3(1, 0, 0), speed);
            }
            else if (Mathf.Round(whiteCube.transform.position.z) == 2 && Mathf.Round(whiteCube.transform.position.z) == 2)
            {
                yield return Rotate(UpHorizontal, new Vector3(-1, 0, 0), speed);
            }
            else if (Mathf.Round(whiteCube.transform.position.z) == 1 && Mathf.Round(whiteCube.transform.position.x) == -2)
            {
                yield return Rotate(UpVertical, new Vector3(0, 0, -1), speed);
            }
        }
        else if(Mathf.Round(whiteCube.transform.position.y) == -2)
        {
            yield return Rotate(UpVertical,new Vector3(0,0,1),speed);
            yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
        }

        
        centers.Clear();
        for (int i = 0; i < cubeSides.Count; i++)
        {
            centers.AddRange(cubeSides[i].FindAll(x => x.GetComponent<CubePieceScript>().Planes.FindAll(y => y.activeInHierarchy).Count == 1));

        }

        GameObject redCube = new GameObject();
        for (int i = 0; i < 6; i++)
        {
            redCube = centers.Find(gg => gg.GetComponent<CubePieceScript>().Planes[i].GetComponent<Renderer>().material.color == Color.red &&
                gg.GetComponent<CubePieceScript>().Planes[i].activeInHierarchy);
            
            if (redCube != null)
            {
                break;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (Mathf.Round(redCube.transform.position.x) == -1 && Mathf.Round(redCube.transform.position.y) == -1 && Mathf.Round(redCube.transform.position.z) == 0)
            {
                yield break;
            }
            else
            {
                yield return Rotate(FrontHorizontalPieces, new Vector3(0,1,0),speed);
            }
        }

        yield return null;
    }
    public IEnumerator BuildWhiteKrest(List<List<GameObject>> cubeSides)
    {
        yield return PutCenterPicesOnTheirPlaces(cubeSides);
        infoText.text = "1. Сбор белого креста:";
        infoText.text = infoText.text.Insert(infoText.text.Length, inProcess);
        
        Color whiteColor = Color.white;

        List<GameObject> Rebra = new List<GameObject>();






        for (int i = 0; i < cubeSides.Count; i++)
        {
            Rebra.AddRange(cubeSides[i].FindAll(x => x.GetComponent<CubePieceScript>().Planes.FindAll(y => y.activeInHierarchy).Count == 2));

        }

        if(IsSolve)
        {
            speed = 2;
        }
        
        List<GameObject> WhiteRebra = GetObjsByColor(Rebra, whiteColor);

        int wsw = 0;
        if(IsSolve)
            solveText.text = "находим кубики с белыми ребрами";
        yield return showAllPieces(WhiteRebra);
        foreach(GameObject c in WhiteRebra)
        {
        
            if(IsPieceOnPlace(c))
            {
                continue;

            }
            else
            {
                wsw++;
            }
            
        }
        
      
        bool canSolve = true;
      //  int speed = 5;
        if (canSolve)
        {

            foreach (GameObject whiteRebro in WhiteRebra) {

                Coroutine lr = null;
                Debug.Log("InSolving:)");
             //   print(whiteRebro.transform.position);
            //    print(startPos(whiteRebro).transform.position);
                
          //      StartCoroutine(morgat(startPos(whiteRebro)));
                if(IsPieceOnPlace(whiteRebro))
                {
                    continue;
                }
                if (IsSolve)
                {
                    lr = StartCoroutine(morgat(whiteRebro));
                }
                if (Mathf.Round(whiteRebro.transform.position.y) == 0 && !IsPieceOnPlace(whiteRebro))
                {
                    if (IsSolve)
                    {   
                        solveText.text = "опускаем кубик вниз";
                        yield return WaitForClickButton();
                    }
                    Debug.Log("In y == 0");
                 
                    if (Mathf.Round(whiteRebro.transform.position.z) == 1 && Mathf.Round(whiteRebro.transform.position.x) == 0)
                    {

                        yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed); 
                        yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                        yield return RotateDownToBuildWhiteKrest(speed, whiteRebro);
                    }
                    
                    else if (Mathf.Round(whiteRebro.transform.position.z) == 2 && Mathf.Round(whiteRebro.transform.position.x) == -1)
                    {

                        yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
                        yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
                        yield return RotateDownToBuildWhiteKrest(speed, whiteRebro);

                    }
                    else if (Mathf.Round(whiteRebro.transform.position.z) == 0 && Mathf.Round(whiteRebro.transform.position.x) == -1)
                    {
                        yield return Rotate(LeftPieces, new Vector3(0, 0, 1), speed);
                        yield return Rotate(LeftPieces, new Vector3(0, 0, 1), speed);
                        yield return RotateDownToBuildWhiteKrest(speed, whiteRebro);

                    }
                    else
                    {
                        yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
                        yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
                        yield return RotateDownToBuildWhiteKrest(speed, whiteRebro);

                    }

                }
                else if (Mathf.Round(whiteRebro.transform.position.y) == -1)
                {
                    if (IsSolve)
                    {
                        solveText.text = "опускаем кубик вниз";
                        yield return WaitForClickButton();
                    }
                    int x = Mathf.RoundToInt(whiteRebro.transform.position.x);
                    int y = Mathf.RoundToInt(whiteRebro.transform.position.y);
                    int z = Mathf.RoundToInt(whiteRebro.transform.position.z);
                    
                    if(x == 0)
                    {
                        if(z == 0)
                        {
                            yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                            yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                            yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                            yield return RotateDownToBuildWhiteKrest(speed, whiteRebro);
                        }
                        else if(z == 2)
                        {
                            yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                            yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                            yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                            yield return RotateDownToBuildWhiteKrest(speed, whiteRebro);
                        }
                    }
                    else if(x == -2)
                    {
                        if (z == 0)
                        {
                            yield return Rotate(BackPieces, new Vector3(-1, 0, 0), speed);
                            yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                            yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
                            yield return RotateDownToBuildWhiteKrest(speed, whiteRebro);
                        }
                        else if (z == 2)
                        {
                            yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
                            yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                            yield return Rotate(BackPieces, new Vector3(-1, 0, 0), speed);
                            yield return RotateDownToBuildWhiteKrest(speed, whiteRebro);
                        }
                    }

                }
                else if (Mathf.Round(whiteRebro.transform.position.y) == -2)
                {
                    Debug.Log("In y == 2");
                    yield return RotateDownToBuildWhiteKrest(speed, whiteRebro);

                }
                if (IsSolve)
                {
                    StopCoroutine(lr);
                    whiteRebro.GetComponent<Renderer>().material.color = CubeCenterPiece.GetComponent<Renderer>().material.color;
                }
                if(IsSolve)
                {
                    break;
                }
            }
        }
        if (IsSolve)
        {
            YesButton.SetActive(true);
            yield return WaitForClickButton();
            YesButton.SetActive(false);
            isUnderstand = false;
            if(wsw==0)
            {
                yield break;
            }
            else
            {
                yield return BuildWhiteKrest(sides);
            }
        }
       
        
        solveText.text = "";
        infoText.text = infoText.text.Replace(inProcess, ready);
        if (IsSolve)
            IsSolve = false;
    }
    public void Understand()
    {
        isUnderstand = true;
    }
    public void ExitSolve()
    {
        IsSolve = false;
        solveText.text = "";
        StopAllCoroutines();
        isUnderstand = false;
        YesButton.SetActive(false);
        ExitButton.SetActive(false);
    }
    IEnumerator RotateDownToBuildWhiteKrest(int speed, GameObject piece)
    {
        foreach (GameObject cubik in GetActivrColors(piece))
        {
       //     print("PlaneName=" + cubik.name  );
        }
        if (IsSolve)
        {
            solveText.text = "ставим кубик под свой цвет";
            yield return WaitForClickButton();
        }

        for (int i = 0; i < 4; i++)

        {
            int x = Mathf.RoundToInt(piece.transform.position.x);
            int y = Mathf.RoundToInt(piece.transform.position.y);
            int z = Mathf.RoundToInt(piece.transform.position.z);
           // print("CenterPiece="+centralPiece(x,y,z).transform.position);
            int num = GetSide(sides[1], sides, piece)[0];
           // print("getSide || num = " + num);
            bool isGoodPiece = IsPieceUnderSameColor(GetActivrColors(piece), GetActivrColors(centralPiece(x, y, z)));
            //  print("I =" + i + " IsGoodPiece = " + isGoodPiece);

            //print("getSide = "+GetSide(DownPieces, sides, piece));
            if (isGoodPiece)
            {
                if (IsSolve)
                {
                    solveText.text = "смотрим на положение кубика";
                    yield return WaitForClickButton();
                }
                print("In zero if");
                //  print(piece.transform.eulerAngles);
                // print("Rotation_x=" + piece.transform.eulerAngles.x + " Rotation_y=" + piece.transform.eulerAngles.y + " Rotation_z=" + piece.transform.eulerAngles.z);
                List<int> angles = new List<int>();
                angles.Add(90);
                angles.Add(270);

                if ((Mathf.Abs(Mathf.Round(piece.transform.eulerAngles.x)) == 180) || (Mathf.Abs(Mathf.Round(piece.transform.eulerAngles.z)) == 180))
                {
                    if (IsSolve)
                    {
                        solveText.text = "белый смотрит вниз - вращаем 2 раза грань с кубиком";
                        yield return WaitForClickButton();
                    }
                    print("InfirstIf");

                    print("::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
                    yield return Rotate(sides[num], RotationVectors[num], speed);
                    yield return Rotate(sides[num], RotationVectors[num], speed);
                    print("::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");

                    break;
                }


                // else if (Mathf.Abs(Mathf.Round(piece.transform.eulerAngles.x)) == 90 || Mathf.Abs(Mathf.Round(piece.transform.eulerAngles.z)) == 90)
                else if (angles.Contains(Mathf.Abs(Mathf.RoundToInt(piece.transform.eulerAngles.x))) || angles.Contains(Mathf.Abs(Mathf.RoundToInt(piece.transform.eulerAngles.z))))
                {
                    if (IsSolve)
                    {
                        solveText.text = "опускаем центр и вставляем туда кубик";
                        yield return WaitForClickButton();
                    }
                    print("InSecondIf");
                    //           speed = 5;
                    int j = GetCentralSide(centralSides, piece);
                    print(j);
                    if (Mathf.Round(piece.transform.position.x) == -2 || Mathf.Round(piece.transform.position.z) == 2)
                    {
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(centralSides[j], RotationVectorsForCenter[j], speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                        yield return Rotate(centralSides[j], RotationVectorsForCenter[j + 3], speed);
                        //      speed = 5;
                        break;
                    }
                    else
                    {
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(centralSides[j], RotationVectorsForCenter[j + 3], speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                        yield return Rotate(centralSides[j], RotationVectorsForCenter[j], speed);
                        //         speed = 5;
                        break;
                    }

                }
            }
            else
            {

                yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
            }

            
        }


    }
               
    bool IsPieceUnderSameColor(List<GameObject> activePlanesPiece, List<GameObject> activePlanesCenter)
    {
        for(int i = 0; i<activePlanesPiece.Count; i++)
        {
            if(activePlanesPiece[i].name == activePlanesCenter[0].name)
            {
                return true;
            }
        }
        return false;
    }
    
    List<int> GetSide(List<GameObject> exception, List<List<GameObject>> cubeSide, GameObject piece)
    {

        print("Start Getside");/*
        foreach (GameObject cubik in exception)
        {
            print("Cubikpos=" + cubik.transform.position);
        }*/
        List<int> sides = new List<int>();
        for (int i = 0; i < 6; i++)
        { //  print("I="+i+" cubeSideContains="+cubeSide[i].Contains(piece) +" exception comparison "+ (cubeSide[i] != exception));
            /*
            foreach(GameObject cubik in cubeSide[i])
            {
                print("Cubikpos="+cubik.transform.position);
            }*/
            
            if (cubeSide[i].Contains(piece) && cubeSide[i]!=exception)
                sides.Add(i);
            
        }
        return sides;
        
    }

    int GetCentralSide(List<List<GameObject>> cubeSide, GameObject piece)
    {
        //print("Start Getside");


        for (int i = 0; i < cubeSide.Count; i++)
        {
            /*
            foreach(GameObject cubik in cubeSide[i])
            {
                print("Cubikpos="+cubik.transform.position);
            }*/

            if (cubeSide[i].Contains(piece))
                return i;

        }
        return -1;
    }
        List<GameObject> GetObjsByColor(List<GameObject> Rebra, Color color)
    {
        List<GameObject> objs = new List<GameObject>();
        int count = 6;
        

        foreach (GameObject gg in Rebra)
        {

            for (int i = 0; i < count; i++)
            {

                if (gg.GetComponent<CubePieceScript>().Planes[i].GetComponent<Renderer>().material.color == color && gg.GetComponent<CubePieceScript>().Planes[i].active)

                {
                    if (!objs.Contains(gg))
                        objs.Add(gg);
                    //Debug.Log("i=" + i + " x=" + gg.transform.position.x + " y =" + gg.transform.position.y + " z = " + gg.transform.position.z);
                    //Debug.Log("Planes = " + Rebra[i].GetComponent<CubePieceScript>().Planes[i].GetComponent<Renderer>().material.name);
                    //Debug.Log("Is active" + gg.GetComponent<CubePieceScript>().Planes[i].active);
                }
            }
        }

        return objs;

    }

    List<GameObject> GetObjsByTwoColors(List<GameObject> Rebra, List<Color> colorsExcept)
    {
        List<GameObject> objs = new List<GameObject>();
        int count = 6;
        foreach(Color color in colorsExcept)
        {
           // print("Except Color ="+color);
        }
           
        foreach (GameObject gg in Rebra)
        {
            List<GameObject> planesCube = GetActivrColors(gg);
            for (int i = 0; i < count; i++)
            {



                if ((planesCube[0].GetComponent<Renderer>().material.color != colorsExcept[0] && planesCube[0].GetComponent<Renderer>().material.color != colorsExcept[1]) &&
                     (planesCube[1].GetComponent<Renderer>().material.color != colorsExcept[1] && planesCube[1].GetComponent<Renderer>().material.color != colorsExcept[0]) &&
                     gg.GetComponent<CubePieceScript>().Planes[i].activeInHierarchy)

                {
                    //     print("Colorplane[0] =" + planesCube[0].GetComponent<Renderer>().material.color);

                    //      print("Colorplane[0] ="+planesCube[0].GetComponent<Renderer>().material.color.Equals(colorsExcept[0]));
                    //          print("Colorplane[0] =" + planesCube[0].GetComponent<Renderer>().material.color.Equals(colorsExcept[1]));
                    //       print("Colorplane[1] =" + planesCube[1].GetComponent<Renderer>().material.color);

                   // print("Colorplane[1] =" + planesCube[1].GetComponent<Renderer>().material.color.Equals(colorsExcept[0]));
                  //  print("Colorplane[1] =" + planesCube[1].GetComponent<Renderer>().material.color.Equals(colorsExcept[1]));
                 //   print(" IsActive =" + gg.GetComponent<CubePieceScript>().Planes[i].activeInHierarchy);
                    if (!objs.Contains(gg))
                    {
                        // print("Obj was added");
                        objs.Add(gg);
                    }
                    //Debug.Log("i=" + i + " x=" + gg.transform.position.x + " y =" + gg.transform.position.y + " z = " + gg.transform.position.z);
                    //Debug.Log("Planes = " + Rebra[i].GetComponent<CubePieceScript>().Planes[i].GetComponent<Renderer>().material.name);
                    //Debug.Log("Is active" + gg.GetComponent<CubePieceScript>().Planes[i].active);
                }
                else continue;//print("skipped obj");
            }
        }
        return objs;
    }

    
    bool IsPieceOnPlace(GameObject piece)
        {
        if (Mathf.Round(piece.transform.eulerAngles.y) == 0 && Mathf.Round(piece.transform.eulerAngles.x) == 0 && Mathf.Round(piece.transform.eulerAngles.z) == 0)
            return true;
        return false;
        }


    List<GameObject> GetActivrColors(GameObject piece
        )
    {
        List<GameObject> planes = new List<GameObject>();
        List<GameObject> planesCube = piece.GetComponent<CubePieceScript>().Planes;
        for (int i = 0; i < 6; i++)
        {
            if(planesCube[i].activeInHierarchy)
            {
                planes.Add(planesCube[i]);
            }
        }
    
        return planes;
    }

    GameObject centralPiece(int x, int y , int z)
    {
        return AllCubePieces.Find(cube => (Mathf.Round(cube.transform.position.x) == x) && (Mathf.Round(cube.transform.position.y) == y+1) && (Mathf.Round(cube.transform.position.z) == z));
    }







    public IEnumerator BuildWhiteCorner(List<List<GameObject>> cubeSides)
    {
        if(IsSolve )
        {
            IsSolve = false;
           
            yield return BuildWhiteKrest(sides);
            
           
                IsSolve = true;
            
        }
        infoText.text = infoText.text.Insert(infoText.text.Length, "\n2. Сбор 1 ряда:"+inProcess);
        Color whiteColor = Color.white;

        List<GameObject> corners = new List<GameObject>();

        print("InWhiteCorner");
        for (int i = 0; i < cubeSides.Count; i++)
        {
            corners.AddRange(cubeSides[i].FindAll(x => x.GetComponent<CubePieceScript>().Planes.FindAll(y => y.activeInHierarchy).Count == 3));

        }
      
       
        List<GameObject> WhiteCorners = GetObjsByColor(corners, whiteColor);

        if (IsSolve)
            solveText.text = "находим кубики с белыми уголками";
        yield return showAllPieces(WhiteCorners);
        foreach (GameObject corner in WhiteCorners)
        {
           
           // int speed = 5;
            int x = Mathf.RoundToInt(corner.transform.position.x);
            int y = Mathf.RoundToInt(corner.transform.position.y);
            int z = Mathf.RoundToInt(corner.transform.position.z);
            print(x+" "+y+" "+z);
            print("IsPieceOnplace "+IsPieceOnPlace(corner));
            if(IsPieceOnPlace(corner))
            {
                continue;
            }
            Coroutine lr = null;
            if (IsSolve)
            {


                lr = StartCoroutine(morgat(corner));
            }

            if (y == 0 && !IsPieceOnPlace(corner))
            {
                
                if (IsSolve)
                {
                    solveText.text = "опускаем кубик вниз";
                    yield return WaitForClickButton();
                }
                if (x == 0)
                {
                    if (z == 0)
                    {
                        yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                        yield return RotateDownToBuildWhiteCorner(corner, speed);
                    }
                    else if (z == 2)
                    {
                        yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                        yield return RotateDownToBuildWhiteCorner(corner, speed);
                    }
                }
                else if (x == -2)
                {
                    if (z == 0)
                    {
                        yield return Rotate(BackPieces, new Vector3(-1, 0, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
                        yield return RotateDownToBuildWhiteCorner(corner, speed);
                    }
                    else if (z == 2)
                    {
                        yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(BackPieces, new Vector3(-1, 0, 0), speed);
                        yield return RotateDownToBuildWhiteCorner(corner, speed);
                    }
                }
                
            }
            else if (y == -2)
            {

                yield return RotateDownToBuildWhiteCorner(corner, speed);
            }
            if (IsSolve)
            {
                StopCoroutine(lr);
                corner.GetComponent<Renderer>().material.color = CubeCenterPiece.GetComponent<Renderer>().material.color;
            }
                if (IsSolve)
                {
                    break;
                }
        }
        if (IsSolve)
        {
            int wsw = 0;
            foreach(GameObject c in WhiteCorners)
            {
                if (!IsPieceOnPlace(c))
                    wsw++;
            }
            YesButton.SetActive(true);
            yield return WaitForClickButton();
            YesButton.SetActive(false);
            isUnderstand = false;
            if (wsw == 0)
            {
                yield break;
            }
            else
            {
                yield return BuildWhiteCorner(sides);
            }
        }
        infoText.text = infoText.text.Replace(inProcess, ready);
        yield return null;
        if (IsSolve)
            IsSolve = false;
    }

    

   
    string GetColorByColor(Color color)
    {
        
        if (color == Color.blue)
            return "blue";
        else if (color == Color.green)
            return "green";
        else if (color == Color.red)
            return "red";
        else if (color == Color.yellow)
            return "yellow";
        else if (color == Color.white)
            return "white";
        else
            return "orange";
    }
    IEnumerator RotateDownToBuildWhiteCorner(GameObject corner, int speed)
    {
        if (IsSolve)
        {
            solveText.text = "ставим кубик под свой цвет";
            yield return WaitForClickButton();
        }
        for (int i = 0; i < 4; i++)
        {
            
            List<int> num = new List<int>();
            if (Mathf.Round(corner.transform.position.y) == 0)
            {
                num = GetSide(sides[0], sides, corner);
            }
            else { num = GetSide(sides[1], sides, corner); }

            List<List<GameObject>> planes = new List<List<GameObject>>();
            planes.Add(sides[num[0]]);
            planes.Add(sides[num[1]]);

            int center1 = GetPlaneCenter(planes[0]);
            int center2 = GetPlaneCenter(planes[1]);
            GameObject Fcenter = planes[0][center1],
                        Scenter = planes[1][center2];
            print(IsPieceBetweenTwoCenters(GetActivrColors(Fcenter), GetActivrColors(Scenter), GetActivrColors(corner)));
            string colorPare = GetColorByColor(GetActivrColors(Scenter)[0].GetComponent<Renderer>().material.color).ToString() + "-" + GetColorByColor(GetActivrColors(Fcenter)[0].GetComponent<Renderer>().material.color).ToString();
            print(colorPare);
            print("Color =" + GetPlaneByColor(corner, colorPare, planes));


            if(IsPieceBetweenTwoCenters(GetActivrColors(planes[0][center1]), GetActivrColors(planes[1][center2]), GetActivrColors(corner)))
            {
                
                int sideNum = SideByColor(GetPlaneByColor(corner, colorPare, planes));
               
                print("Sidenum ="+sideNum);
                bool isChange = false;
                if (sideNum == -1)
                {
                    sideNum = num[0];
                    isChange = true;
                }

                    int vecz = (Mathf.RoundToInt(sides[sideNum][GetPlaneCenter(sides[sideNum])].transform.position.x) - Mathf.RoundToInt(corner.transform.position.x));
                    int vecy = 0;
                    int vecx = (Mathf.RoundToInt(sides[sideNum][GetPlaneCenter(sides[sideNum])].transform.position.z) - Mathf.RoundToInt(corner.transform.position.z));
                    print(vecx + " " + vecy + " " + vecz);
                if(isChange)
                    sideNum = -1;
                if(sideNum !=-1)
                {
                    if (IsSolve)
                    {
                        solveText.text = "кубик не смотрит вниз";
                        yield return WaitForClickButton();
                    }
                }
                
                if(sideNum == -1)
                {
                    if (num[0] == 2 && num[1] == 4)
                    {
                        if (IsSolve)
                        {
                            solveText.text = "Кубик смотрит вниз";
                            yield return WaitForClickButton();
                        }
                        // speed = 1;
                        yield return Rotate(sides[num[1]], new Vector3(vecz, vecy, vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                        yield return Rotate(sides[num[1]], new Vector3(-vecz, -vecy, -vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                        yield return Rotate(sides[num[1]], new Vector3(vecz, vecy, vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(sides[num[1]], new Vector3(-vecz, -vecy, -vecx), speed);
                    }
                    else if(num[0] == 3 && num[1] == 4)
                    {
                        yield return Rotate(sides[num[1]], new Vector3(-vecz, -vecy, -vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0,1, 0), speed);
                        yield return Rotate(sides[num[1]], new Vector3(vecz, vecy, vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(sides[num[1]], new Vector3(-vecz, -vecy, -vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                        yield return Rotate(sides[num[1]], new Vector3(vecz, vecy, vecx), speed);
                    }
                    else if (num[0] == 2 && num[1] == 5)
                    {
                        // speed = 1;
                        
                        yield return Rotate(sides[num[1]], new Vector3(-vecz, -vecy, -vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                        yield return Rotate(sides[num[1]], new Vector3(vecz, vecy, vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(sides[num[1]], new Vector3(-vecz, -vecy, -vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                        yield return Rotate(sides[num[1]], new Vector3(vecz, vecy, vecx), speed);
                    }
                    else if (num[0] == 3 && num[1] == 5)
                    {
                        yield return Rotate(sides[num[1]], new Vector3(vecz, vecy, vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(sides[num[1]], new Vector3(-vecz, -vecy, -vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                        yield return Rotate(sides[num[1]], new Vector3(vecz, vecy, vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(sides[num[1]], new Vector3(-vecz, -vecy, -vecx), speed);
                    }
                }
                else if (sideNum==2)
                {
                    if (num[1] == 5)
                    {
                        print("red-green");
                        yield return Rotate(sides[sideNum], new Vector3(vecx, vecy, vecz), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(sides[sideNum], new Vector3(-vecx, -vecy, -vecz), speed);
                    }
                    else if(num[1] == 4)
                    {
                        yield return Rotate(sides[sideNum], new Vector3(vecx, vecy, vecz), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                        yield return Rotate(sides[sideNum], new Vector3(-vecx, -vecy, -vecz), speed);
                    }
                }
                else if(sideNum == 3)
                {
                    if (num[1] == 4)
                    {


                        print("orange blue");
                        yield return Rotate(sides[sideNum], new Vector3(vecx, vecy, vecz), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(sides[sideNum], new Vector3(-vecx, -vecy, -vecz), speed);
                    }
                    else if(num[1] == 5)
                    {
                        yield return Rotate(sides[sideNum], new Vector3(vecx, vecy, vecz), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                        yield return Rotate(sides[sideNum], new Vector3(-vecx, -vecy, -vecz), speed);
                    }
                }
                else if (sideNum == 4)
                {
                    if(num[0]==2)
                      {
                        print("redblue");
                        yield return Rotate(sides[sideNum], new Vector3(-vecx, -vecy, -vecz), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(sides[sideNum], new Vector3(vecx, vecy, vecz), speed);
                       }
                    else if(num[0] == 3)
                    {
                        print("orangeblue");
                        yield return Rotate(sides[sideNum], new Vector3(-vecx, -vecy, -vecz), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                        yield return Rotate(sides[sideNum], new Vector3(vecx, vecy, vecz), speed);
                    }
                }
                else if(sideNum == 5)
                {
                    if (num[0] == 3)
                    {
                        yield return Rotate(sides[sideNum], new Vector3(-vecx, -vecy, -vecz), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
                        yield return Rotate(sides[sideNum], new Vector3(vecx, vecy, vecz), speed);
                    }
                    else if(num[0] == 2)
                    {
                        yield return Rotate(sides[sideNum], new Vector3(-vecx, -vecy, -vecz), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                        yield return Rotate(sides[sideNum], new Vector3(vecx, vecy, vecz), speed);
                    }
                }
                break;
            }
            else
            {
                yield return Rotate(DownPieces,new Vector3(0,1,0),speed);
            }
           

            
            print(SideByColor(GetPlaneByColor(corner, colorPare, planes)));
            yield return null;
        }
    }
    bool IsPieceBetweenTwoCenters(List<GameObject> FcenterActive, List<GameObject> ScenterActive, List<GameObject> pieceActive)
    {
        List<string> activeplanes = new List<string>();
        for (int i = 0; i < pieceActive.Count; i++)
        {
          //  print("i = "+i);
          //  print("PieceActive ="+pieceActive[i]);
            activeplanes.Add(pieceActive[i].name);
        }
      

        if (activeplanes.Contains(FcenterActive[0].name) && activeplanes.Contains(ScenterActive[0].name))
            return true;
        else
            return false;
    }


    int GetPlaneCenter(List<GameObject> plane)
    {
        for (int i = 0; i < 9; i++)
        {

            if (plane[i].GetComponent<CubePieceScript>().Planes.FindAll(x => x.activeInHierarchy).Count == 1)
            {
                return i;
            }
        }
        return -1;
    }



    string GetPlaneByColor(GameObject cubik, string color, List<List<GameObject>> planes)
    {
        Color baseColor = Color.white;
        Vector3 orientation = cubik.transform.eulerAngles;
        string orientation_string = Mathf.Round(orientation.x).ToString() + Mathf.Round(orientation.y).ToString() + Mathf.Round(orientation.z).ToString();
        print("orientation =" + orientation_string);
        List<Vector3> expectedOrientations;
      
        List<string> baseColorDown = new List<string>();
        List<string> baseColorRed = new List<string>();
        List<string> baseColorBlue = new List<string>();
        switch (color)
        {
            case "blue-red":

                baseColorDown.Add("0-90180");
                baseColorDown.Add("0270180");
                baseColorDown.Add("0-90-180");
                baseColorDown.Add("0270-180");


                baseColorRed.Add("-9000");
                baseColorRed.Add("27000");




                baseColorBlue.Add("-180-18090");
                baseColorBlue.Add("18018090");
                baseColorBlue.Add("-18018090");
                baseColorBlue.Add("180-18090");
                baseColorBlue.Add("180-18090");
                baseColorBlue.Add("-18018090");
                baseColorBlue.Add("00-90");
                baseColorBlue.Add("00270");

                print("COntains_red =" + baseColorRed.Contains(orientation_string));
                if (baseColorDown.Contains(orientation_string))
                    return null;
                else if (baseColorRed.Contains(orientation_string))
                    return "red";
                else if (baseColorBlue.Contains(orientation_string))
                    return "blue";
                else
                    return null;
            case "green-red":

                baseColorDown.Add("180-900");
                baseColorDown.Add("-1802700");
                baseColorDown.Add("1802700");
                baseColorDown.Add("-180-900");
                baseColorDown.Add("090180");
                baseColorDown.Add("0270180");
                baseColorDown.Add("090-180");
                baseColorDown.Add("0270-180");


                baseColorRed.Add("-9090-90");
                baseColorRed.Add("27090270");
                baseColorRed.Add("27090-90");
                baseColorRed.Add("-9090270");
                baseColorRed.Add("-90180180");
                baseColorRed.Add("-90-180-180");
                baseColorRed.Add("-90-180180");
                baseColorRed.Add("-90180-180");
                baseColorRed.Add("-9000");
                baseColorRed.Add("27000");



                baseColorBlue.Add("0090");
                baseColorBlue.Add("00-270");


                print("COntains_red =" + baseColorRed.Contains(orientation_string));
                if (baseColorDown.Contains(orientation_string))
                    return null;
                else if (baseColorRed.Contains(orientation_string))
                    return "red";
                else if (baseColorBlue.Contains(orientation_string))
                    return "green";
                else
                    return null;

            case "green-orange":

                baseColorDown.Add("0-90180");
                baseColorDown.Add("0-90-180");
                baseColorDown.Add("0270180");
                baseColorDown.Add("0270-180");


                baseColorRed.Add("9000");
                baseColorRed.Add("-27000");
             

           
                baseColorBlue.Add("0090");
                baseColorBlue.Add("00-270");


                print("COntains_red =" + baseColorRed.Contains(orientation_string));
                if (baseColorDown.Contains(orientation_string))
                    return null;
                else if (baseColorRed.Contains(orientation_string))
                    return "orange";
                else if (baseColorBlue.Contains(orientation_string))
                    return "green";
                else
                    return null;


            case "blue-orange":

                baseColorDown.Add("180-900");
                baseColorDown.Add("-180-900");
                baseColorDown.Add("1802700");
                baseColorDown.Add("-1802700");


                baseColorRed.Add("9000");
                baseColorRed.Add("-27000");



                baseColorBlue.Add("00-90");
                baseColorBlue.Add("00270");


                print("COntains_red =" + baseColorRed.Contains(orientation_string));
                if (baseColorDown.Contains(orientation_string))
                    return null;
                else if (baseColorRed.Contains(orientation_string))
                    return "orange";
                else if (baseColorBlue.Contains(orientation_string))
                    return "blue";
                else
                    return null;
                //   default: return null;
        }
        return null;
    }

   int SideByColor(string color)
    {
        if(color == null)
        {
            return -1;
        }
        //print(color);
        switch(color)
        {
            case "red":return 2;
            case "orange":return 3;
            case "blue": return 4;
            case "green":return 5;
            
            case "white": return 0;
            case "yellow": return 1;
            
                
        }
        return -1;
    }

    public IEnumerator BuildColourRebra(List<List<GameObject>> cubeSides)
    {
        if (IsSolve)
            
        { IsSolve = false;
            
            yield return BuildWhiteCorner(sides);
            yield return BuildWhiteCorner(sides);
                IsSolve = true;
           
        }
        infoText.text = infoText.text.Insert(infoText.text.Length, "\n3. Сбор 2 ряда:" + inProcess);
        Color whiteColor = Color.white;

        List<GameObject> Rebra = new List<GameObject>();
        
        for (int i = 0; i < cubeSides.Count; i++)
        {
            Rebra.AddRange(cubeSides[i].FindAll(x => x.GetComponent<CubePieceScript>().Planes.FindAll(y => y.activeInHierarchy).Count == 2));

        }
        
        List<Color> colors = new List<Color>();
        colors.Add(new Color(1,1,1,1));
        colors.Add(new Color(1,1,0,1));
        List<GameObject> colorRebra = GetObjsByTwoColors(Rebra, colors);
        if (IsSolve)
            solveText.text = "находим ребра";
        yield return showAllPieces(colorRebra);
    //   int speed = 5;
    /*
        foreach(GameObject cubik in colorRebra)
        {
            print(cubik.transform.position);
        }*/
        foreach (GameObject cubik in colorRebra)
        {
            if(IsPieceOnPlace(cubik))
            {
                continue;

            }
            Coroutine lr = null;
            if (IsSolve)
            {
                lr = StartCoroutine(morgat(cubik));
            }

            if(Mathf.Round(cubik.transform.position.y) == -1 && !IsPieceOnPlace(cubik))
            {
                if (IsSolve)
                {
                    solveText.text = "опускаем кубик вниз";
                    yield return WaitForClickButton();
                }
                List<int> occ = GetSide(sides[0], sides, cubik);

                int vecz = (Mathf.RoundToInt(sides[occ[0]][GetPlaneCenter(sides[occ[0]])].transform.position.x) - Mathf.RoundToInt(cubik.transform.position.x));
                int vecy = 0;
                int vecx = (Mathf.RoundToInt(sides[occ[0]][GetPlaneCenter(sides[occ[0]])].transform.position.z) - Mathf.RoundToInt(cubik.transform.position.z));

              //  speed = 5;
             if(occ[0] == 3 && occ[1] == 4)
                {
                    yield return Rotate(sides[3], -RotationVectors[3],speed);
                    yield return Rotate(sides[1], new Vector3(0, 1, 0), speed);
                    yield return Rotate(sides[3], RotationVectors[3], speed);
                    yield return Rotate(sides[1], new Vector3(0, -1, 0), speed);
                    yield return Rotate(sides[4], RotationVectors[4], speed);
                    yield return Rotate(sides[1], new Vector3(0, -1, 0), speed);
                    yield return Rotate(sides[4], -RotationVectors[4], speed);
                }
             else if(occ[0] == 3 && occ[1] == 5)
                {
                    
                    yield return Rotate(sides[5], -RotationVectors[5], speed);
                    yield return Rotate(sides[1], new Vector3(0, 1, 0), speed);
                    yield return Rotate(sides[5], RotationVectors[5], speed);
                    yield return Rotate(sides[1], new Vector3(0, -1, 0), speed);
                    yield return Rotate(sides[3], RotationVectors[3], speed);
                    yield return Rotate(sides[1], new Vector3(0, -1, 0), speed);
                    yield return Rotate(sides[3], -RotationVectors[3], speed);
                }
             else if(occ[0] == 2 && occ[1] == 5)
                {
                    yield return Rotate(sides[2], -RotationVectors[2], speed);
                    yield return Rotate(sides[1], new Vector3(0, 1, 0), speed);
                    yield return Rotate(sides[2], RotationVectors[2], speed);
                    yield return Rotate(sides[1], new Vector3(0, -1, 0), speed);
                    yield return Rotate(sides[5], RotationVectors[5], speed);
                    yield return Rotate(sides[1], new Vector3(0, -1, 0), speed);
                    yield return Rotate(sides[5], -RotationVectors[5], speed);
                }
             else if(occ[0] == 2 && occ[1] == 4)
                {
                    yield return Rotate(sides[4], -RotationVectors[4], speed);
                    yield return Rotate(sides[1], new Vector3(0, 1, 0), speed);
                    yield return Rotate(sides[4], RotationVectors[4], speed);
                    yield return Rotate(sides[1], new Vector3(0, -1, 0), speed);
                    yield return Rotate(sides[2], RotationVectors[2], speed);
                    yield return Rotate(sides[1], new Vector3(0, -1, 0), speed);
                    yield return Rotate(sides[2], -RotationVectors[2], speed);
                }   
             yield return Rotate(sides[occ[0]], new Vector3(),speed);
                    
                
                
            }

            if (Mathf.Round(cubik.transform.position.y) == -2)
            {
                yield return RotateDownToBuildRebra(speed, cubik);
            }
            if (IsSolve)
            {
                StopCoroutine(lr);
                cubik.GetComponent<Renderer>().material.color = CubeCenterPiece.GetComponent<Renderer>().material.color;
            }
            if (IsSolve)
            {
                break;
            }
        }
        if (IsSolve)
        {
            int wsw = 0;
            foreach (GameObject c in colorRebra)
            {
                if (!IsPieceOnPlace(c))
                    wsw++;
            }
            YesButton.SetActive(true);
            yield return WaitForClickButton();
            YesButton.SetActive(false);
            isUnderstand = false;
            if (wsw == 0)
            {
                yield break;
            }
            else
            {
                yield return BuildWhiteCorner(sides);
            }
        }
        infoText.text = infoText.text.Replace(inProcess, ready);
        yield return null;
        if (IsSolve)
            IsSolve = false;
    }

    IEnumerator RotateDownToBuildRebra(int speed, GameObject cubik)
    {
        if (IsSolve)
        {
            solveText.text = "ставим кубик лицевой стороной под свой цвет";
            yield return WaitForClickButton();
        }
        for (int i = 0; i<4; i++)
        {
            int x = Mathf.RoundToInt(cubik.transform.position.x);
            int y = Mathf.RoundToInt(cubik.transform.position.y);
            int z = Mathf.RoundToInt(cubik.transform.position.z);
            bool isGoodPiece = IsPieceUnderSameColor(GetActivrColors(cubik), GetActivrColors(centralPiece(x, y, z)));
            print(isGoodPiece +" "+ Mathf.Round(cubik.transform.eulerAngles.x) +" "+ Mathf.Round(cubik.transform.eulerAngles.z)+" "+ Mathf.Round(cubik.transform.eulerAngles.y));
            List<int> numbers = new List<int>();
            numbers.Add(270);
            numbers.Add(-90);
            numbers.Add(90);

            
            GameObject CenterUnderCubik = centralPiece(x, y, z);
            GameObject OtherCenterCubik;
            int occ;

            List<GameObject> CubeActiveColors = GetActivrColors(cubik);

            if (GetActivrColors(centralPiece(x, y, z))[0].GetComponent<Renderer>().material.color == CubeActiveColors[0].GetComponent<Renderer>().material.color)
            {
                occ = SideByColor(GetColorByColor(CubeActiveColors[1].GetComponent<Renderer>().material.color));
            }
            else
            {
                occ = SideByColor(GetColorByColor(CubeActiveColors[0].GetComponent<Renderer>().material.color));
            }

            OtherCenterCubik = sides[occ][GetPlaneCenter(sides[occ])];


            //print(OtherCenterCubik.transform.position);
            List<int> SideCube = GetSide(sides[1], sides, cubik);
            string downColor;
            string CenterPlane = GetColorByColor(GetActivrColors(centralPiece(x, y, z))[0].GetComponent<Renderer>().material.color);


            if (GetActivrColors(centralPiece(x, y, z))[0].GetComponent<Renderer>().material.color == CubeActiveColors[0].GetComponent<Renderer>().material.color)
            {
                 downColor = GetColorByColor(CubeActiveColors[1].GetComponent<Renderer>().material.color);
            }
            else
            {
                downColor = GetColorByColor(CubeActiveColors[0].GetComponent<Renderer>().material.color);
            }


            int coef = GetOthercolorForRebro(CenterPlane, downColor);

            int vecz = (Mathf.RoundToInt(sides[occ][GetPlaneCenter(sides[occ])].transform.position.x) - Mathf.RoundToInt(cubik.transform.position.x));
            int vecy = 0;
            int vecx = (Mathf.RoundToInt(sides[occ][GetPlaneCenter(sides[occ])].transform.position.z) - Mathf.RoundToInt(cubik.transform.position.z));



            if (isGoodPiece && ((numbers.Contains(Mathf.RoundToInt(cubik.transform.eulerAngles.x)) || numbers.Contains(Mathf.RoundToInt(cubik.transform.eulerAngles.z))) && Mathf.Round(cubik.transform.eulerAngles.y) == 0 ) 
                )
            {
                if (IsSolve)
                {
                    solveText.text = "Ставим кубик на место";
                    yield return WaitForClickButton();
                }
                //   speed = 5;
                if (SideCube[0] == 2)
                {
                    if (occ == 5)
                    {
                       
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(vecz, vecy, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(-vecz, vecy, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(0, vecy, vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(0, vecy, -vecx), speed);

                        print("UnderSameColor!!!");
                        break;
                    }
                   else 
                    {
                  
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(-vecz, vecy, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(vecz, vecy, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(0, vecy, -vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(0, vecy, vecx), speed);

                        print("UnderSameColor!!!");
                        break;
                    }
                }
                else if(SideCube[0] == 5)
                {
                    if (sides[occ] == sides[3])
                    {
                        
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(0, vecy, vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(0, vecy, -vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(vecz, vecy, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(-vecz, vecy, 0), speed);

                        print("UnderSameColor!!!");
                        break;
                    }
                    else
                    {
                     
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(0, vecy, -vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(0, vecy, vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(-vecz, vecy, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(vecz, vecy, 0), speed);

                        print("UnderSameColor!!!");
                        break;
                    }
                }
                else if (SideCube[0] == 3)
                {
                    if (occ == 5)
                    {
                       
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(-vecz, vecy, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(vecz, vecy, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(0, vecy, -vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(0, vecy, vecx), speed);

                        print("UnderSameColor!!!");
                        break;
                    }
                    else
                    {
                     
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(vecz, vecy, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(-vecz, vecy, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(0, vecy, vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(0, vecy, -vecx), speed);

                        print("UnderSameColor!!!");
                        break;
                    }
                }
                else if (SideCube[0] == 4)
                {
                    if (sides[occ] == sides[3])
                    {
                       
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(0, vecy, -vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(0, vecy, vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(-vecz, vecy, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(vecz, vecy, 0), speed);

                        print("UnderSameColor!!!");
                        break;
                    }
                    else
                    {
                      
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(0, vecy, vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[occ], new Vector3(0, vecy, -vecx), speed);
                        yield return Rotate(DownPieces, new Vector3(0, 1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(vecz, vecy, 0), speed);
                        yield return Rotate(DownPieces, new Vector3(0, -1 * coef, 0), speed);
                        yield return Rotate(sides[SideCube[0]], new Vector3(-vecz, vecy, 0), speed);

                        print("UnderSameColor!!!");
                        break;
                    }
                }
            }
            else
            {
                yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
            }
        }
        yield return null;
    }

    int GetOthercolorForRebro(string color, string downColor)
    {
        string sideToRotate = "";
        switch(color)
        {
            case "orange":
                
                    if (downColor == "blue")
                    {
                        sideToRotate = "left";
                    
                    }
                    else
                    {
                        sideToRotate = "right";
                    
                    }
                break;
           case "blue":
                if (downColor == "red")
                {
                    sideToRotate = "left";

                }
                else
                {
                    sideToRotate = "right";

                }
                break;
            case "red":
                if (downColor == "green")
                {
                    sideToRotate = "left";

                }
                else
                {
                    sideToRotate = "right";

                }
                break;
            case "green":
                if (downColor == "orange")
                {
                    sideToRotate = "left";

                }
                else
                {
                    sideToRotate = "right";

                }
                break;
        }
        if(sideToRotate == "left")
        {
            return 1;
        
        }
        else
        {
            return -1;
        }
    }

    public IEnumerator ReturnAndPlaceOnTruePlaceYellowRebra(List<List<GameObject>> sides)
    {
        if (IsSolve)
        {
            IsSolve = false;
            yield return BuildWhiteKrest(sides);
            yield return BuildWhiteCorner(sides);
            yield return BuildColourRebra(sides);
            IsSolve = true;
        }
        infoText.text = infoText.text.Insert(infoText.text.Length, "\n4. Сбор желтого креста:" + inProcess);
        cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, 180);

        List<GameObject> Rebra = new List<GameObject>();

        for (int i = 0; i < sides.Count; i++)
        {
            Rebra.AddRange(sides[i].FindAll(x => x.GetComponent<CubePieceScript>().Planes.FindAll(y => y.activeInHierarchy).Count == 2));

        }
   //     int speed = 5;
        List<GameObject> yellowRebra = GetObjsByColor(Rebra, new Color(1, 1, 0, 1));
        if (IsSolve)
            solveText.text = "находим кубики с желтыми ребрами";
        yield return showAllPieces(yellowRebra);
        yield return RotateDown(yellowRebra, speed);
        yield return null;
        infoText.text = infoText.text.Replace(inProcess, ready);
        if (IsSolve)
            IsSolve = false;
    }

    List<GameObject> GetnumOfOnPlace(List<GameObject> rebra)
    {
        List<GameObject> CountOfTruStanded = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            int x = Mathf.RoundToInt(rebra[i].transform.position.x);
            int y = Mathf.RoundToInt(rebra[i].transform.position.y);
            int z = Mathf.RoundToInt(rebra[i].transform.position.z);
            GameObject centralpiece = centralPiece(x, y, z);
            if (IsPieceUnderSameColor(GetActivrColors(rebra[i]), GetActivrColors(centralpiece)))
            {
                CountOfTruStanded.Add(rebra[i]);
            }
        }
        return CountOfTruStanded;
    }

    IEnumerator RotateDown(List<GameObject> rebra, int speed)
    {
        if (IsSolve)
        {
            solveText.text = "ставим 2 кубика под свои цвета";
            yield return WaitForClickButton();
        }
        List<GameObject> num = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            num = GetnumOfOnPlace(rebra);
            print(num.Count);
            if (num.Count == 2 || num.Count == 4)
            {
                break;
            }
            else
            {
                yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
            }
        }
        /*
        foreach (GameObject cubik in num)
        {
            print(cubik.transform.position);
        }*/
        for (int i = 0; i < 1; i++)
        {
            if (IsPieceOnPlace(num[i]))
            {
                break;
            }
            else
            {

            }
        }
        List<GameObject> numFalse = new List<GameObject>();
        for (int i = 0; i < rebra.Count; i++)
        {
            if (num.Contains(rebra[i]))
            {
                continue;
            }
            else numFalse.Add(rebra[i]);
        }
        if (numFalse.Count == 2)
        {
            yield return PutYellowRebraOnTheirPlaces(regrding2pieces(numFalse[0], numFalse[1], true), numFalse, speed);//put yellow on need place
        }
        int counter = 0;
        List<GameObject> whoStayWrong = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            if (IsPieceOnPlace(rebra[i]))
            {
                counter++;
            }
            else
            {
                whoStayWrong.Add(rebra[i]);
            }

        }
        print(counter);
        print(whoStayWrong.Count);
        if (counter == 4)
        {
            if (IsSolve)
            {
                solveText.text = "все кубики правильно повернуты";
                yield return WaitForClickButton();
            }
            yield break;
        }
        else if (counter == 2)
        {
            if (IsSolve)
            {
                solveText.text = "два кубика неправильно повернуты";
                yield return WaitForClickButton();
            }
            int x = Mathf.RoundToInt(whoStayWrong[0].transform.position.x);
            int z = Mathf.RoundToInt(whoStayWrong[0].transform.position.z);

            int x1 = Mathf.RoundToInt(whoStayWrong[1].transform.position.x);
            int z1 = Mathf.RoundToInt(whoStayWrong[1].transform.position.z);

            string situation = "";

            if (Mathf.Max(x, x1) - Mathf.Min(x, x1) == 2 || Mathf.Max(z, z1) - Mathf.Min(z, z1) == 2)
            {
                situation = "opposite";
            }
            else { situation = "notOpposite"; }

            print(situation);
            //print(centralSides.Count);
            //print(GetCentralSide(centralSides, regrding2pieces(whoStayWrong[0], whoStayWrong[1], false)[7]));
            // List<GameObject> cs = centralSides[ GetCentralSide(centralSides, regrding2pieces(whoStayWrong[0], whoStayWrong[1], false)[7])];
            if (situation == "opposite")
            {
                if (Mathf.Round(whoStayWrong[0].transform.position.x) == -1)//centralpiece/getcentralside
                {
                    yield return Rotate(UpHorizontal, new Vector3(-1, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(1, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(-1, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(1, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(-1, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(1, 0, 0), speed);


                    yield return Rotate(UpHorizontal, new Vector3(-1, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(1, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(-1, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(1, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(-1, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(1, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);

                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);

                }
                else
                {
                    yield return Rotate(UpVertical, new Vector3(0, 0, -1), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, -1), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, -1), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);

                    yield return Rotate(UpVertical, new Vector3(0, 0, -1), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, -1), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, -1), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);

                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                }
                print("-------------------------------");


            }
            else
            {
                int coef = 1;
                if (regrding2pieces(whoStayWrong[0], whoStayWrong[1], false) == sides[2] || regrding2pieces(whoStayWrong[0], whoStayWrong[1], false) == sides[4])
                {
                    coef = 1;
                }
                else
                {
                    coef = -1;
                }
                print("888888888888888888888888888888888888");
                if (Mathf.Round(regrding2pieces(whoStayWrong[0], whoStayWrong[1], false)[GetPlaneCenter(regrding2pieces(whoStayWrong[0], whoStayWrong[1], false))].transform.position.x) == -1)
                {
                    print("iniq");
                    yield return Rotate(UpHorizontal, new Vector3(-1 * coef, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(1 * coef, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(-1 * coef, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(1 * coef, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(-1 * coef, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpHorizontal, new Vector3(1 * coef, 0, 0), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                }
                else
                {
                    yield return Rotate(UpVertical, new Vector3(0, 0, -1 * coef), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, 1 * coef), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, -1 * coef), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, 1 * coef), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, -1 * coef), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                    yield return Rotate(UpVertical, new Vector3(0, 0, 1 * coef), speed);
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                }

            }

        }
        else if (counter == 0)
        {
            if (IsSolve)
            {
                solveText.text = "все кубики неправильно повернуты";
                yield return WaitForClickButton();
            }
            yield return Rotate(UpVertical, new Vector3(0, 0, -1), speed);
            yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
            yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
            yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
            yield return Rotate(UpVertical, new Vector3(0, 0, -1), speed);
            yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
            yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
            yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
            yield return Rotate(UpVertical, new Vector3(0, 0, -1), speed);
            yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
            yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
            yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);

            yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
            yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);

            yield return Rotate(UpVertical, new Vector3(0, 0, -1), speed);
            yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
            yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
            yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
            yield return Rotate(UpVertical, new Vector3(0, 0, -1), speed);
            yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
            yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
            yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
            yield return Rotate(UpVertical, new Vector3(0, 0, -1), speed);
            yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
            yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);


            yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
        }
        print("DontExit");
        if (numFalse.Count == 2)



            yield return null;
    }

     IEnumerator PutYellowRebraOnTheirPlaces(List<GameObject> side, List<GameObject> wrongpieces, int speed)
    {
        int x = Mathf.RoundToInt(wrongpieces[0].transform.position.x);
        int z = Mathf.RoundToInt(wrongpieces[0].transform.position.z);

        int x1 = Mathf.RoundToInt(wrongpieces[1].transform.position.x);
        int z1 = Mathf.RoundToInt(wrongpieces[1].transform.position.z);

        string situation = "";

        if (Mathf.Max(x, x1) - Mathf.Min(x, x1) == 2 || Mathf.Max(z, z1) - Mathf.Min(z, z1) == 2)
        {
            situation = "opposite";
        }
        else { situation = "notOpposite"; }

        //speed = 5;
        if (situation == "notOpposite")
        {
            if (side == sides[4])
            {


                yield return Rotate(sides[4], new Vector3(-1, 0, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[4], new Vector3(1, 0, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[4], new Vector3(-1, 0, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[4], new Vector3(1, 0, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
            }
            else if (side == sides[5])
            {


                yield return Rotate(sides[5], new Vector3(1, 0, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[5], new Vector3(-1, 0, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[5], new Vector3(1, 0, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[5], new Vector3(-1, 0, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
            }
            else if (side == sides[3])
            {


                yield return Rotate(sides[3], new Vector3(0, 0, -1), speed);

                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);

                yield return Rotate(sides[3], new Vector3(0, 0, 1), speed);

                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);

                yield return Rotate(sides[3], new Vector3(0, 0, -1), speed);

                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);

                yield return Rotate(sides[3], new Vector3(0, 0, 1), speed);

                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
            }
            else if (side == sides[2])
            {


                yield return Rotate(sides[2], new Vector3(0, 0, 1), speed);

                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);

                yield return Rotate(sides[2], new Vector3(0, 0, -1), speed);

                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);

                yield return Rotate(sides[2], new Vector3(0, 0, 1), speed);

                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);

                yield return Rotate(sides[2], new Vector3(0, 0, -1), speed);

                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
            }
        }
        else
        {
            if (Mathf.Round(wrongpieces[0].transform.position.z) == 1 || Mathf.Round(wrongpieces[1].transform.position.z) == 1)
            {

                yield return Rotate(sides[3], new Vector3(0, 0, -1), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[3], new Vector3(0, 0, 1), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[3], new Vector3(0, 0, -1), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[3], new Vector3(0, 0, 1), speed);

                yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);

                yield return Rotate(sides[3], new Vector3(0, 0, -1), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[3], new Vector3(0, 0, 1), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[3], new Vector3(0, 0, -1), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[3], new Vector3(0, 0, 1), speed);
            }
            else
            {
                yield return Rotate(sides[5], new Vector3(1, 0, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[5], new Vector3(-1, 0, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[5], new Vector3(1, 0, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[5], new Vector3(-1, 0, 0), speed);

                yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);

                yield return Rotate(sides[5], new Vector3(1, 0, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[5], new Vector3(-1, 0, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[5], new Vector3(1, 0, 0), speed);
                yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                yield return Rotate(sides[5], new Vector3(-1, 0, 0), speed);

            }
        }
    }


    List<GameObject> regrding2pieces(GameObject fPiece, GameObject sPiece, bool whichreturn)//true - sside, false - fside
    {
        List<GameObject> forreturn = new List<GameObject>();
        int fSide = GetSide(sides[1], sides, fPiece)[0];
        int sSide = GetSide(sides[1], sides, sPiece)[0];
        print(fSide + " " + sSide);
        if (whichreturn)
        {
            if ((fSide == 4 && sSide == 2) || (fSide == 2 && sSide == 4))
            {
                forreturn = sides[4];
            }
            else if ((fSide == 4 && sSide == 3) || (fSide == 3 && sSide == 4))
            {
                forreturn = sides[3];
            }
            else if ((fSide == 3 && sSide == 5) || (fSide == 5 && sSide == 3))
            {
                forreturn = sides[5];
            }
            else if ((fSide == 5 && sSide == 2) || (fSide == 2 && sSide == 5))
            {
                forreturn = sides[2];
            }
        }
        else
        {
            if ((fSide == 4 && sSide == 2) || (fSide == 2 && sSide == 4))
            {
                forreturn = sides[2];
            }
            else if ((fSide == 4 && sSide == 3) || (fSide == 3 && sSide == 4))
            {
                forreturn = sides[4];
            }
            else if ((fSide == 3 && sSide == 5) || (fSide == 5 && sSide == 3))
            {
                forreturn = sides[3];
            }
            else if ((fSide == 5 && sSide == 2) || (fSide == 2 && sSide == 5))
            {
                forreturn = sides[5];
            }
        }
        return forreturn;
    }

    public IEnumerator ReturnAndPlaceOnTruePlaceYellowCorners(List<List<GameObject>> cubeSides)

    {
        if (IsSolve)
        {
            
            IsSolve = false;
            yield return BuildWhiteKrest(sides);
            yield return BuildWhiteCorner(sides);
            yield return BuildColourRebra(sides);
           
            yield return ReturnAndPlaceOnTruePlaceYellowRebra(sides);

            
            
                IsSolve = true;
        }
        infoText.text = infoText.text.Insert(infoText.text.Length, "\n5. Расстановка и поворот уголков:" + inProcess);
        Color whiteColor = new Color(1, 1, 0, 1);

        List<GameObject> corners = new List<GameObject>();

        print("InWhiteCorner");
        for (int i = 0; i < cubeSides.Count; i++)
        {
            corners.AddRange(cubeSides[i].FindAll(x => x.GetComponent<CubePieceScript>().Planes.FindAll(y => y.activeInHierarchy).Count == 3));

        }


        List<GameObject> WhiteCorners = GetObjsByColor(corners, whiteColor);
        if (IsSolve)
            solveText.text = "находим кубики с желтыми ребрами";
        yield return showAllPieces(WhiteCorners);
        List<GameObject> whoStayWrong = new List<GameObject>();
        whoStayWrong.Clear();
        foreach (GameObject cubik in WhiteCorners)
        {

            List<int> num = new List<int>();

            num = GetSide(sides[1], sides, cubik);
            //   if(IsPieceBetweenTwoCenters())
            List<List<GameObject>> planes = new List<List<GameObject>>();
            planes.Add(sides[num[0]]);
            planes.Add(sides[num[1]]);

            int center1 = GetPlaneCenter(planes[0]);
            int center2 = GetPlaneCenter(planes[1]);
            GameObject Fcenter = planes[0][center1],
                        Scenter = planes[1][center2];
            if (IsPieceBetweenTwoCenters(GetActivrColors(Fcenter), GetActivrColors(Scenter), GetActivrColors(cubik)))
            {
                continue;
            }
            else
            {
                whoStayWrong.Add(cubik);
            }


        }
        //

        //int speed = 5;


        yield return PutYellowCornersOnTheirPlaces(whoStayWrong, speed);
        whoStayWrong.Clear();
        foreach (GameObject cubik in WhiteCorners)
        {

            List<int> num = new List<int>();

            num = GetSide(sides[1], sides, cubik);
            //   if(IsPieceBetweenTwoCenters())
            List<List<GameObject>> planes = new List<List<GameObject>>();
            planes.Add(sides[num[0]]);
            planes.Add(sides[num[1]]);

            int center1 = GetPlaneCenter(planes[0]);
            int center2 = GetPlaneCenter(planes[1]);
            GameObject Fcenter = planes[0][center1],
                        Scenter = planes[1][center2];
            if (IsPieceBetweenTwoCenters(GetActivrColors(Fcenter), GetActivrColors(Scenter), GetActivrColors(cubik)))
            {
                continue;
            }
            else
            {
                whoStayWrong.Add(cubik);
            }


        }
        yield return PutYellowCornersOnTheirPlaces(whoStayWrong, speed);
        whoStayWrong.Clear();
        foreach (GameObject cubik in WhiteCorners)
        {

            List<int> num = new List<int>();

            num = GetSide(sides[1], sides, cubik);
            //   if(IsPieceBetweenTwoCenters())
            List<List<GameObject>> planes = new List<List<GameObject>>();
            planes.Add(sides[num[0]]);
            planes.Add(sides[num[1]]);

            int center1 = GetPlaneCenter(planes[0]);
            int center2 = GetPlaneCenter(planes[1]);
            GameObject Fcenter = planes[0][center1],
                        Scenter = planes[1][center2];
            if (IsPieceBetweenTwoCenters(GetActivrColors(Fcenter), GetActivrColors(Scenter), GetActivrColors(cubik)))
            {
                continue;
            }
            else
            {
                whoStayWrong.Add(cubik);
            }


        }
        yield return PutYellowCornersOnTheirPlaces(whoStayWrong, speed);
        whoStayWrong.Clear();
        foreach (GameObject cubik in WhiteCorners)
        {

            List<int> num = new List<int>();

            num = GetSide(sides[1], sides, cubik);
            //   if(IsPieceBetweenTwoCenters())
            List<List<GameObject>> planes = new List<List<GameObject>>();
            planes.Add(sides[num[0]]);
            planes.Add(sides[num[1]]);

            int center1 = GetPlaneCenter(planes[0]);
            int center2 = GetPlaneCenter(planes[1]);
            GameObject Fcenter = planes[0][center1],
                        Scenter = planes[1][center2];
            if (IsPieceBetweenTwoCenters(GetActivrColors(Fcenter), GetActivrColors(Scenter), GetActivrColors(cubik)))
            {
                continue;
            }
            else
            {
                whoStayWrong.Add(cubik);
            }



        }

        yield return PutYellowCornersOnTheirPlaces(whoStayWrong, speed);


        whoStayWrong = wstwr(WhiteCorners);
        //print(wstwr(WhiteCorners).Count);
        print(whoStayWrong.Count);
        yield return RotateCorners(whoStayWrong, speed);
        whoStayWrong.Clear();
        whoStayWrong = wstwr(WhiteCorners);
        yield return RotateCorners(whoStayWrong, speed);
        whoStayWrong.Clear();
        whoStayWrong = wstwr(WhiteCorners);
        yield return RotateCorners(whoStayWrong, speed);
        yield return null;

        infoText.text = infoText.text.Replace(inProcess, ready);
        if (IsSolve)
            IsSolve = false;
        yield return new WaitForSeconds(3);
        infoText.text = "";
        infoTextGameObj.SetActive(false);
    }

    IEnumerator PutYellowCornersOnTheirPlaces(List<GameObject> wsw, int speed)//whoStayWorng
    {
        print("lululululuul");
        print(wsw.Count);
        if (wsw.Count == 0)
        {
            yield break;
        }
        else if (wsw.Count == 3)
        {
            GameObject centrPiece = wsw[GetCentralPieceFromCorners(wsw)];
            print(centrPiece.transform.position);


            List<int> num = new List<int>();

            num = GetSide(sides[1], sides, centrPiece);
            //   if(IsPieceBetweenTwoCenters())
            List<List<GameObject>> planes = new List<List<GameObject>>();
            planes.Add(sides[num[0]]);
            planes.Add(sides[num[1]]);

            int center1 = GetPlaneCenter(planes[0]);
            int center2 = GetPlaneCenter(planes[1]);
            GameObject Fcenter = planes[0][center1],
                        Scenter = planes[1][center2];


            List<GameObject> firstSide = GetPlaneByYellowColor(GetColorByColor(GetActivrColors(Fcenter)[0].GetComponent<Renderer>().material.color), GetColorByColor(GetActivrColors(Scenter)[0].GetComponent<Renderer>().material.color), true);
            List<GameObject> thirdSide = GetPlaneByYellowColor(GetColorByColor(GetActivrColors(Fcenter)[0].GetComponent<Renderer>().material.color), GetColorByColor(GetActivrColors(Scenter)[0].GetComponent<Renderer>().material.color), false);

            foreach (GameObject c in thirdSide)
            {
                print(c.transform.position);
            }

            if (thirdSide == sides[2])//green
            {
             //   speed = 5;
                yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(LeftPieces, new Vector3(0, 0, 1), speed);
                yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(LeftPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(BackPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(LeftPieces, new Vector3(0, 0, 1), speed);
                yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(LeftPieces, new Vector3(0, 0, -1), speed);
            }
            else if (thirdSide == sides[5]) // orange
            {
                print("lalalal");
                yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(LeftPieces, new Vector3(0, 0, 1), speed);
                yield return Rotate(BackPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
                yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(LeftPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(BackPieces, new Vector3(-1, 0, 0), speed);
            }
            else if (thirdSide == sides[4])//red
            {
                print("papaappapapa");
                yield return Rotate(LeftPieces, new Vector3(0, 0, 1), speed);
                yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(LeftPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
                yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
            }
            else if (thirdSide == sides[3])//blue
            {
                print("upupupuppuu");
                yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
                yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(BackPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
            }


        }
        else if (wsw.Count == 4)
        {
        //    speed = 5;
            yield return Rotate(LeftPieces, new Vector3(0, 0, 1), speed);
            yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
            yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
            yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
            yield return Rotate(LeftPieces, new Vector3(0, 0, -1), speed);
            yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
            yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
            yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
        }
        yield return null;
    }

    int GetCentralPieceFromCorners(List<GameObject> whw)//warning  !!!use only if whw.COunt == 3!!! Warning
    {
        List<int> l1 = new List<int>();
        l1.Add(Mathf.RoundToInt(whw[0].transform.position.x));
        l1.Add(Mathf.RoundToInt(whw[0].transform.position.z));
        List<int> l2 = new List<int>();
        l2.Add(Mathf.RoundToInt(whw[1].transform.position.x));
        l2.Add(Mathf.RoundToInt(whw[1].transform.position.z));
        List<int> l3 = new List<int>();
        l3.Add(Mathf.RoundToInt(whw[2].transform.position.x));
        l3.Add(Mathf.RoundToInt(whw[2].transform.position.z));
        foreach (int n in l1)
            print(n);
        foreach (int n in l2)
            print(n);
        foreach (int n in l3)
            print(n);
        int numPodozrevayemye = 0;

        if (l1[0] == l2[0])
        {
            if (l1[1] == l3[1])
                numPodozrevayemye = 0;
            else if (l2[1] == l3[1])
                numPodozrevayemye = 1;
        }

        else if (l2[0] == l3[0])
        {
            if (l1[1] == l3[1])
                numPodozrevayemye = 2;
            else if (l2[1] == l1[1])
                numPodozrevayemye = 1;
        }

        else if (l1[0] == l3[0])
        {
            if (l1[1] == l2[1])
                numPodozrevayemye = 0;
            else if (l2[1] == l3[1])
                numPodozrevayemye = 2;
        }
        return numPodozrevayemye;

    }

    List<GameObject> GetPlaneByYellowColor(string color1, string color2, bool isFirstForRotate)
    {
        print(color1 + " " + color2);
        if (isFirstForRotate)
        {
            if ((color1 == "blue" && color2 == "orange") || (color1 == "orange" && color2 == "blue"))
            {
                return sides[4];
            }
            if ((color1 == "blue" && color2 == "red") || (color1 == "red" && color2 == "blue"))
            {
                return sides[2];
            }
            if ((color1 == "green" && color2 == "orange") || (color1 == "orange" && color2 == "green"))
            {
                return sides[3];
            }
            if ((color1 == "green" && color2 == "red") || (color1 == "red" && color2 == "green"))
            {
                return sides[5];
            }
        }
        else
        {
            if ((color1 == "blue" && color2 == "orange") || (color1 == "orange" && color2 == "blue"))
            {
                return sides[3];
            }
            if ((color1 == "blue" && color2 == "red") || (color1 == "red" && color2 == "blue"))
            {
                return sides[4];
            }
            if ((color1 == "green" && color2 == "orange") || (color1 == "orange" && color2 == "green"))
            {
                return sides[5];
            }
            if ((color1 == "green" && color2 == "red") || (color1 == "red" && color2 == "green"))
            {
                return sides[2];
            }
        }
        return null;
    }

    List<GameObject> getOppositeSide(List<GameObject> forSide)
    {
        if (forSide == sides[2])
            return sides[3];
        if (forSide == sides[3])
            return sides[2];
        if (forSide == sides[0])
            return sides[1];
        if (forSide == sides[4])
            return sides[5];
        if (forSide == sides[5])
            return sides[4];
        if (forSide == sides[1])
            return sides[0];
        return null;
    }

    List<GameObject> wstwr(List<GameObject> corners)
    {
        List<GameObject> whoStayWrong = new List<GameObject>();
        foreach (GameObject cubik in corners)
        {



            if (IsPieceOnPlace(cubik))
            {
                continue;
            }
            else
            {
                whoStayWrong.Add(cubik);
            }



        }
        return whoStayWrong;
    }

    IEnumerator RotateCorners(List<GameObject> whoStWr, int speed)
    {
   //     speed = 5;
        if (whoStWr.Count == 0) yield break;
        if (whoStWr.Count == 2)
        {
            List<int> sidesToRotate = GetSide(sides[1], sides, whoStWr[0]);
            int x1 = 0;
            int x2 = 0;

            int z1 = 0;
            int z2 = 0;

            if (sidesToRotate[0] == 2 && sidesToRotate[1] == 4)
            {
                x2 = -1;
                x1 = 0;
                z2 = 0;
                z1 = -1;

                yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                if (IsPieceOnPlace(whoStWr[0]))
                {
                    yield return RotateDownCornersToPutOnNeedCoords(whoStWr[1], whoStWr[0], speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);



                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);




                }
                else
                {
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);

                    yield return RotateDownCornersToPutOnNeedCoords(whoStWr[1], whoStWr[0], speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                }



                for (int i = 0; i < numOfRotations; i++)
                {
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);

                }
                numOfRotations = 0;
            }

            else if (sidesToRotate[0] == 3 && sidesToRotate[1] == 4)
            {
                x2 = 1;
                x1 = 0;
                z2 = 0;
                z1 = -1;
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                if (IsPieceOnPlace(whoStWr[0]))
                {
                    yield return RotateDownCornersToPutOnNeedCoords(whoStWr[1], whoStWr[0], speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);

                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                }
                else
                {
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);

                    yield return RotateDownCornersToPutOnNeedCoords(whoStWr[1], whoStWr[0], speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                }
                for (int i = 0; i < numOfRotations; i++)
                {
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);

                }
                numOfRotations = 0;
            }

            else if (sidesToRotate[0] == 2 && sidesToRotate[1] == 5)
            {
                x2 = -1;
                x1 = 0;
                z2 = 0;
                z1 = 1;
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                if (IsPieceOnPlace(whoStWr[0]))
                {
                    yield return RotateDownCornersToPutOnNeedCoords(whoStWr[1], whoStWr[0], speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);

                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                }
                else
                {
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);

                    yield return RotateDownCornersToPutOnNeedCoords(whoStWr[1], whoStWr[0], speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                }
                for (int i = 0; i < numOfRotations; i++)
                {
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);

                }
                numOfRotations = 0;
            }


        }
        else if (whoStWr.Count == 3)
        {
            List<int> sidesToRotate = GetSide(sides[1], sides, whoStWr[0]);
            int x1 = 0;
            int x2 = 0;

            int z1 = 0;
            int z2 = 0;

            foreach (int n in sidesToRotate)
            {
                print(n);
            }

            if (sidesToRotate[0] == 2 && sidesToRotate[1] == 4)
            {
                x2 = -1;
                x1 = 0;
                z2 = 0;
                z1 = -1;

                yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                if (IsPieceOnPlace(whoStWr[0]))
                {
                    yield return RotateDownCornersToPutOnNeedCoords(whoStWr[1], whoStWr[0], speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);


                    yield return RotateDownCornersToPutOnNeedCoords(whoStWr[2], whoStWr[0], speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);


                }
                else
                {
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);

                    yield return RotateDownCornersToPutOnNeedCoords(whoStWr[1], whoStWr[0], speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                }
                for (int i = 0; i < numOfRotations; i++)
                {
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);

                }
                numOfRotations = 0;
            }
            else if (sidesToRotate[0] == 3 && sidesToRotate[1] == 4)
            {
                x2 = 1;
                x1 = 0;
                z2 = 0;
                z1 = -1;

                yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                if (IsPieceOnPlace(whoStWr[0]))
                {
                    yield return RotateDownCornersToPutOnNeedCoords(whoStWr[1], whoStWr[0], speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);

                    yield return RotateDownCornersToPutOnNeedCoords(whoStWr[2], whoStWr[0], speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                }
                else
                {
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);

                    yield return RotateDownCornersToPutOnNeedCoords(whoStWr[1], whoStWr[0], speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(x2, 0, z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(x1, 0, z1), speed);
                    yield return Rotate(sides[sidesToRotate[1]], new Vector3(-x2, 0, -z2), speed);
                    yield return Rotate(sides[sidesToRotate[0]], new Vector3(-x1, 0, -z1), speed);
                }
                for (int i = 0; i < numOfRotations; i++)
                {
                    yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
                }
                numOfRotations = 0;
            }
        }
        else if (whoStWr.Count == 4)
        {
            yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
            yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
            yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
            yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
            yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
            yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
            yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
            yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
            if (wstwr(whoStWr).Count == 3)
            {
                yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);

                yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
                yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);

                yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
                yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
            }
            else
            {
                yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
                yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);

                yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);

                yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
                yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, -1), speed);
                yield return Rotate(FrontPieces, new Vector3(-1, 0, 0), speed);
                yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);

            }
            yield return Rotate(DownPieces, new Vector3(0, -1, 0), speed);
        }


        yield return null;
    }


    IEnumerator RotateDownCornersToPutOnNeedCoords(GameObject corner, GameObject baseCorner, int speed)
    {
        Vector3 coords = baseCorner.transform.position;
        int x = Mathf.RoundToInt(baseCorner.transform.position.x);
        int y = Mathf.RoundToInt(baseCorner.transform.position.y);
        int z = Mathf.RoundToInt(baseCorner.transform.position.z);



        print(x + " " + y + " " + z);

        for (int i = 0; i < 4; i++)
        {
            int ox = Mathf.RoundToInt(corner.transform.position.x);
            int oy = Mathf.RoundToInt(corner.transform.position.y);
            int oz = Mathf.RoundToInt(corner.transform.position.z);
            print(ox + " " + oy + " " + oz);
            if (x == ox && y == oy && z == oz)
            {
                break;
            }
            else
            {
                numOfRotations++;
                yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
            }
        }
        yield return null;
    }

    public IEnumerator design_center()
    {
        /* for (int i = 0; i < 4; i++)
         {
             yield return Rotate(UpHorizontal, new Vector3(-1, 0, 0), speed);
             yield return Rotate(FrontHorizontalPieces, new Vector3(0, 1, 0), speed);
         }*/
        yield return Rotate(UpHorizontal, new Vector3(-1, 0, 0), speed);
        yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
        yield return Rotate(UpHorizontal, new Vector3(1, 0, 0), speed);
        yield return Rotate(UpVertical, new Vector3(0, 0, -1), speed);

    }

    public IEnumerator design_chess()
    {
        yield return Rotate(RightPieces, new Vector3(0,0,1),speed);
        yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
        yield return Rotate(LeftPieces, new Vector3(0, 0, 1), speed);
        yield return Rotate(LeftPieces, new Vector3(0, 0, 1), speed);
        yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
        yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
        yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
        yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
        yield return Rotate(DownPieces, new Vector3(0, 1, 0), speed);
        yield return Rotate(UpPieces, new Vector3(0, 1, 0), speed);
        yield return Rotate(UpPieces, new Vector3(0, 1, 0), speed);
    }

    public IEnumerator design_cubeincube()
    {
        yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
        yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
        yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
        yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
        yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
        yield return Rotate(UpVertical, new Vector3(0, 0, 1), speed);
        yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
        yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
        yield return Rotate(LeftPieces, new Vector3(0, 0, 1), speed);
        yield return Rotate(LeftPieces, new Vector3(0, 0, 1), speed);
       

    }

    public IEnumerator design_Unknown()
    {
        yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
        yield return Rotate(RightPieces, new Vector3(0, 0, 1), speed);
        yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
        yield return Rotate(FrontPieces, new Vector3(1, 0, 0), speed);
  
        yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
        yield return Rotate(BackPieces, new Vector3(1, 0, 0), speed);
        yield return Rotate(LeftPieces, new Vector3(0, 0, 1), speed);
        yield return Rotate(LeftPieces, new Vector3(0, 0, 1), speed);


    }
}
