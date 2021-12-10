using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{

    private CubePos nowCube = new CubePos(0, 1, 0);
    public float cubeChangePlaceSpeed = 0.5f;//скорость смены позиции пустого квадрата
    public Transform cubeToPlace;
    public GameObject cubeToCreate, allCubes,vfx;
    private Rigidbody allCubesRb;
    public Coroutine showCubePlace;
    private List<Vector3> allCubesPositions = new List<Vector3>()
    {
        new Vector3(0,0,0),
        new Vector3(1,0,0),
        new Vector3(-1,0,0),
        new Vector3(0,1,0),
        new Vector3(0,-1,0),
        new Vector3(0,0,1),
        new Vector3(0,0,-1),
        new Vector3(1,0,1),
        new Vector3(-1,0,-1),
        new Vector3(1,0,-1),
        new Vector3(-1,0,1)
    };
    private Transform mainCam;
    private void Start()
    {
        
        toCameraColor = Camera.main.backgroundColor;
        mainCam = Camera.main.transform;
        camMoveToYPosition = 5.9f + nowCube.y - 1f;
        allCubesRb = allCubes.GetComponent<Rigidbody>();
        showCubePlace = StartCoroutine(ShowCubePlace());
        scoreTxt.text = "Best result: " + PlayerPrefs.GetInt("score") + " \n Cube: 0";

        ;
    } 
    IEnumerator ShowCubePlace()
    {
        while (true)
        {
            SpawnPositions();
            yield return new WaitForSeconds(cubeChangePlaceSpeed);
        }
    }
    private bool FirstCube;
    public GameObject[] canvasStartPage;
    private float camMoveSpeed = 2f;
    //при нажатии на экран создается новый объект (куб)
    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && cubeToPlace != null && allCubes != null)
        {
#if !UNITY_EDITOR //если прил запущено не в unity
if(Input.GetTouch(0).phase!=TouchPhase.Began)
            {
                return;
            }
#endif
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (!FirstCube)
            {
                FirstCube = true;
                foreach (GameObject obj in canvasStartPage)
                    Destroy(obj);
            }

            GameObject newCube = Instantiate(
                   cubeToCreate,
                   cubeToPlace.position,
                   Quaternion.identity
                   ) as GameObject;

            newCube.transform.SetParent(allCubes.transform);
            nowCube.setVector(cubeToPlace.position);
            allCubesPositions.Add(nowCube.getVector());
          GameObject newVFX =  Instantiate(vfx,cubeToPlace.position,Quaternion.identity) as GameObject;
            Destroy(newVFX, 1.5f);
            allCubesRb.isKinematic = true;
            allCubesRb.isKinematic = false;
            SpawnPositions();
            MoveCameraChangeBg();


            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().Play();
            }
        }
           
        if (IsLose == false && allCubesRb.velocity.magnitude > 0.1f)
            {
                Destroy(cubeToPlace.gameObject);
                IsLose = true;
                StopCoroutine(showCubePlace);
            mainCam.localPosition -= new Vector3(0, 0, 5f);
        }
        
            mainCam.localPosition = Vector3.MoveTowards(
                mainCam.localPosition, 
                new Vector3(mainCam.localPosition.x, camMoveToYPosition, mainCam.localPosition.z), 
                camMoveSpeed * Time.deltaTime);

            if(Camera.main.backgroundColor!=toCameraColor)
            {
                Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor,toCameraColor,Time.deltaTime/1.5f);
            }
        
    }

    private bool IsLose;
    private Color toCameraColor;
    private void SpawnPositions()
    {
        if (!IsLose)
        {
            List<Vector3> positions = new List<Vector3>();//все доступные позиции для размещения
            if (IsPositionEmply(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z)) && nowCube.x + 1 != cubeToPlace.position.x)
            {
                positions.Add(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z));
            }
            if (IsPositionEmply(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z)) && nowCube.x - 1 != cubeToPlace.position.x)
            {
                positions.Add(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z));
            }
            if (IsPositionEmply(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z)) && nowCube.y + 1 != cubeToPlace.position.y)
            {
                positions.Add(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z));
            }
            if (IsPositionEmply(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z)) && nowCube.y - 1 != cubeToPlace.position.y)
            {
                positions.Add(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z));
            }
            if (IsPositionEmply(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1)) && nowCube.z + 1 != cubeToPlace.position.z)
            {
                positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1));
            }
            if (IsPositionEmply(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1)) && nowCube.z - 1 != cubeToPlace.position.z)
            {
                positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1));
            }
            if (positions.Count > 1)
            { cubeToPlace.position = positions[UnityEngine.Random.Range(0, positions.Count)]; }
            else if (positions.Count == 0)
            {
                IsLose = false;
            }
            else cubeToPlace.position = positions[0];
        }

    }
    private bool IsPositionEmply(Vector3 targetpos)
    {
        if (targetpos.y == 0) return false;
        foreach (Vector3 pos in allCubesPositions)
        {
            if (pos.x == targetpos.x && pos.y == targetpos.y && pos.z == targetpos.z) 
            {
                return false;
            } 
        }
            return true;
        
    }
    private float camMoveToYPosition;
    private int prevCountMaxHor = 0;
    public Color[] Bgcolor;
    public Text scoreTxt;
    private void MoveCameraChangeBg()
    {
        int maxX = 0;
        int maxY = 0;
        int maxZ = 0;
        int maxHor;
        foreach (Vector3 pos in allCubesPositions)
        {
            if (Math.Abs(pos.x) > maxX) maxX = Convert.ToInt32(Math.Abs(pos.x));
            if (Math.Abs(pos.y) > maxY) maxY = Convert.ToInt32((pos.y));
            if (Math.Abs(pos.z) > maxZ) maxZ = Convert.ToInt32(Math.Abs(pos.z));
        }
        maxY--;
        if (PlayerPrefs.GetInt("score") < maxY)
            PlayerPrefs.SetInt("score", maxY);
        scoreTxt.text = "Best result: "+ PlayerPrefs.GetInt("score") + " \n Cube: "+maxY;

        camMoveToYPosition = 5.9f + nowCube.y - 1f;
        maxHor = maxX > maxZ ? maxX : maxZ;
        if(maxHor % 2 == 0 && prevCountMaxHor!=maxHor)
        {
            mainCam.localPosition -= new Vector3(0, 0, 5f);
            prevCountMaxHor = maxHor;
        }
        if (maxY >= 120) toCameraColor = Bgcolor[5];
        else if (maxY >= 80) toCameraColor = Bgcolor[4];
        else if (maxY >= 40) toCameraColor = Bgcolor[3];
        else if (maxY >= 20) toCameraColor = Bgcolor[2];
        else if (maxY >= 10) toCameraColor = Bgcolor[1];
        else if (maxY >= 5) toCameraColor = Bgcolor[0];
    }
}


struct CubePos //сохранение координат куба
{
    public int x, y, z;
        public CubePos(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 getVector()
        {
            return new Vector3(x, y, z);
        }
        public void setVector(Vector3 pos)
        {
            x = Convert.ToInt32(pos.x);
            y = Convert.ToInt32(pos.y);
            z = Convert.ToInt32(pos.z);

        }
    }


