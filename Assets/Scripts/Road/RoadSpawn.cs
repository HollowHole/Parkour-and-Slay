using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RoadSpawn : MonoBehaviour
{
    [Tooltip("关卡内所有路块类型")]
    public List<GameObject> RoadPieces;
    public GameObject SwerveZonePrefab;
    GameObject swerveZone;
    public List<Material> Materials;
    private int materialIndex = 0;
    public Transform Road;
    private List<GameObject> AllRoadPieceOnUse;

    public float SpawnHeightOffset;

    public int SwervePlanesCount = 40;

    private Vector3 spawnPoint;

    [SerializeField]private float ZofDespawn = -8f;

    [SerializeField]bool stopSpawn = false;//生成弯道时触发，转弯成功后复位

    [SerializeField]private int collidingObjectCount;//判定区域内没有RoadBlock了就生成新的

    [SerializeField] private bool spawnSwerveTrigger;

    RoadBlockObjectPool pool;
    Transform lastSpawnedPiece;

    // Start is called before the first frame update
    private void Awake()
    {
        AllRoadPieceOnUse = new List<GameObject>();
    }
    private void Start()
    {
        pool = RoadBlockObjectPool.Instance;
        swerveZone = Instantiate(SwerveZonePrefab, transform);
        swerveZone.GetComponent<SwerveZone>().Hide();

        collidingObjectCount = 0;

        //Spawn First RoadPiece;
        lastSpawnedPiece = pool.Spawn(Vector3.zero, Quaternion.identity, Road).transform;
        AllRoadPieceOnUse.Add(lastSpawnedPiece.gameObject);
        //for (float z = 0; z <= endZ; ) { 
        //    Vector3 curSpawnPoint = new Vector3(0,0,z);
        //    go = pool.Spawn( curSpawnPoint, Quaternion.identity, Road);
        //    z += go.transform.localScale.z;
        //}
        SwerveManager.instance.OnSwerveBegin += StopSpawn;
        SwerveManager.instance.OnSwerveEnd += AllowSpawn;

        StartCoroutine(GenerateInitRoad());
        //关卡结束生成转弯
        LevelMgr.Instance.OnLevelEnd += () => { spawnSwerveTrigger = true; };
    }
    private void Update()
    {
        
        HandleDeSpawn();
    }
    private void TrySpawn()
    {
        if (!stopSpawn && collidingObjectCount == 0)
        {
            
            //if (Random.value > 0.1f)
            //    SpawnStraight();
            //else
            //    SpawnSwerve();
            if (spawnSwerveTrigger)
            {
                spawnSwerveTrigger = false;
                SpawnSwerve();
            }
            else
                SpawnStraight();
        }
    }

    private void HandleDeSpawn()
    {
        for(int i = AllRoadPieceOnUse.Count - 1; i >= 0; i--)
        {
            GameObject obj = AllRoadPieceOnUse[i];
            if(obj.transform.position.z < ZofDespawn)
            {
                pool.Despawn(obj);
                AllRoadPieceOnUse.Remove(obj);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("exit");
        if (other.CompareTag("RoadBlock"))
        {
            collidingObjectCount--;
            lastSpawnedPiece = other.transform;
            TrySpawn();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoadBlock"))
        {
            collidingObjectCount++;
        }
    }
    public void AllowSpawn()
    {
        stopSpawn = false;
    }
    public void StopSpawn()
    {
        stopSpawn=true;
    }
    IEnumerator GenerateInitRoad()
    {
        while(collidingObjectCount == 0)
        {
            TrySpawn();
            yield return null;
        }
    }
    private void SpawnStraight(bool spawnSwerve = false)
    {
        //get SpwanPoint
        spawnPoint = lastSpawnedPiece.position;
        if (spawnSwerve)
            spawnPoint.z += lastSpawnedPiece.localScale.x / 2 + RoadPieces[0].transform.localScale.z / 2;
        else
            spawnPoint.z += lastSpawnedPiece.localScale.z;
        spawnPoint.x = 0;
        //
        lastSpawnedPiece = pool.Spawn(spawnPoint, Quaternion.identity, Road).transform;
        AllRoadPieceOnUse.Add(lastSpawnedPiece.gameObject);
        //随机上个材质
        lastSpawnedPiece.GetComponent<MeshRenderer>().material = Materials[materialIndex];
        materialIndex++;
        if (materialIndex >= Materials.Count) { materialIndex = 0; }
    }
    private void SpawnSwerve()
    {
        //get SpwanPoint
        spawnPoint = lastSpawnedPiece.position;
        spawnPoint.z += RoadPieces[0].transform.localScale.z;
        spawnPoint.x = 0;
        //
        bool swerveZoneSetParent = false;
        

        Vector3 curSpawnPoint = spawnPoint;
        
        bool left = Random.Range(0, 2)==0;

        if (left) { curSpawnPoint.x += RoadPieces[0].transform.localScale.x / 2 - RoadPieces[0].transform.localScale.z / 2; }
        else { curSpawnPoint.x -= RoadPieces[0].transform.localScale.x / 2 - RoadPieces[0].transform.localScale.z / 2; }
        curSpawnPoint.z += RoadPieces[0].transform.localScale.x / 2 - RoadPieces[0].transform.localScale.z / 2;

        for (int i = 0; i < SwervePlanesCount; i++)
        {
            GameObject go= pool.Spawn(curSpawnPoint, Quaternion.AngleAxis(90, Vector3.up), Road);
            AllRoadPieceOnUse.Add(go);

            if(!swerveZoneSetParent) {
                lastSpawnedPiece = go.transform;
                swerveZone.transform.SetParent(go.transform,true);
                swerveZone.transform.position = go.transform.position;
                swerveZoneSetParent = true;
                if(left) swerveZone.GetComponent<SwerveZone>().SetSwerveDirection(Global.SwerveDirection.LEFT);
                else swerveZone.GetComponent<SwerveZone>().SetSwerveDirection(Global.SwerveDirection.RIGHT);
            }

            if (left)
            {
                curSpawnPoint.x -= go.transform.localScale.z;
            }
            else
            {
                curSpawnPoint.x += go.transform.localScale.z;
            }
        }

        
        SpawnStraight(true);
    }
}
