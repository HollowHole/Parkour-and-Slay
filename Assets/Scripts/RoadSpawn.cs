using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RoadSpawn : MonoBehaviour
{
    public List<GameObject> RoadPieces;
    public GameObject SwerveZonePrefab;
    public List<Material> Materials;
    private int materialIndex = 0;
    public Transform Road;

    public float SpawnHeightOffset;
    public int SwervePlanesCount = 40;

    private Vector3 spawnPoint;

    bool stopSpawn = false;//生成弯道时触发，转弯成功后复位

    public int collidingObjectCount;//判定区域内没有RoadBlock了就生成新的

    RoadBlockObjectPool pool;

    // Start is called before the first frame update
    private void Start()
    {
        pool = RoadBlockObjectPool.Instance;

        spawnPoint = transform.position;
        spawnPoint.z =spawnPoint.z - 1 + RoadPieces[0].transform.localScale.z / 2;
        spawnPoint.y -= SpawnHeightOffset;

        collidingObjectCount = 0;

        float endZ = transform.position.z;
        for (float z = 0; z <= endZ; ) { 
            Vector3 curSpawnPoint = new Vector3(0,0,z);
            GameObject go = pool.Spawn( curSpawnPoint, Quaternion.identity, Road);
            z += go.transform.localScale.z;
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("exit");
        if (other.CompareTag("RoadBlock"))
        {
            collidingObjectCount--;
            if(!stopSpawn&&collidingObjectCount==0)
            { 
                RandomSpawn(); 
            }

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("1");
        if (other.CompareTag("RoadBlock"))
        {
            collidingObjectCount++;
        }
    }
    private void RandomSpawn()
    {
        float ran =Random.value;
        if(ran>0.5f)
        SpawnStraight();
        else
        {
            SpawnSwerve();
        }
    }
    public void EnableSpawn()
    {
        stopSpawn = false;
    }
    private void SpawnStraight()
    {
        GameObject go = pool.Spawn(spawnPoint, Quaternion.identity, Road);

        if (go != null)
        {
            //Debug.Log("successfully spawned");
        }

        //随机上个材质
        go.GetComponent<MeshRenderer>().material = Materials[materialIndex];
        materialIndex++;
        if (materialIndex >= Materials.Count) { materialIndex = 0; }
    }
    private void SpawnSwerve()
    {
        bool swerveZoneSetParent = false;
        GameObject swerveZone = Instantiate(SwerveZonePrefab, spawnPoint, Quaternion.identity);

        Vector3 curSpawnPoint = spawnPoint;
        
        bool left = Random.Range(0, 2)==0;

        if (left) { curSpawnPoint.x += RoadPieces[0].transform.localScale.x / 2 - RoadPieces[0].transform.localScale.z / 2; }
        else { curSpawnPoint.x -= RoadPieces[0].transform.localScale.x / 2 - RoadPieces[0].transform.localScale.z / 2; }
        curSpawnPoint.z += RoadPieces[0].transform.localScale.x / 2 - RoadPieces[0].transform.localScale.z / 2;

        for (int i = 0; i < SwervePlanesCount; i++)
        {
            GameObject go = pool.Spawn(curSpawnPoint, Quaternion.AngleAxis(90, Vector3.up), Road);

            if(!swerveZoneSetParent) { 
                swerveZone.transform.SetParent(go.transform);
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
        stopSpawn = true ;
    }
}
