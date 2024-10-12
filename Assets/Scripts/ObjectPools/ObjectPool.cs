using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ObjectPool:MonoBehaviour
{
    private GameObject prefab;
    public List<GameObject> usedGameObjectList = new List<GameObject>();
    public List<GameObject> unusedGameObjectList = new List<GameObject>();
    public int capacity = -1;
    public void SetPrefab(string prefabPath)
    {
        prefab = Resources.Load<GameObject>(prefabPath);
    }
    public abstract void Activate(GameObject gameObject);
    public abstract void Deactivate(GameObject gameObject);
    public GameObject Spawn(Vector3 pos, Quaternion quaternion, Transform parent = null)
    {
        GameObject go;
        if (unusedGameObjectList.Count > 0)
        {
            go = unusedGameObjectList[0];
            unusedGameObjectList.RemoveAt(0);
            usedGameObjectList.Add(go);
            go.transform.SetParent(parent,false);
            go.transform.localPosition = pos;
            go.transform.localRotation = quaternion;
            Activate(go);
        }
        else
        {
            go=Instantiate(prefab,pos,quaternion,parent);
            usedGameObjectList.Add(go);
            Activate(go);
        }
        //执行OnSpawn函数
        return go;
    }

    public void Despawn(GameObject go) 
    {
        
        if (!go)
            return;
        //Debug.Log("despawn");
        for (int i = 0; i < usedGameObjectList.Count; i++)
        {
            if(usedGameObjectList[i] == go)
            {
                //对象容量已满
                if(capacity>0 && usedGameObjectList.Count >= capacity)
                {
                    //在未使用的池子里删除一个对象
                    if(unusedGameObjectList.Count > 0)
                    {
                        Destroy(unusedGameObjectList[0]);
                        unusedGameObjectList.RemoveAt(0);
                    }
                }
                unusedGameObjectList.Add(go);
                usedGameObjectList.RemoveAt(i);
                Deactivate(go);
                go.transform.SetParent(transform, false);
            }
            

            //TODO:执行对象被回收时的方法OnDespawn
        }
        
        
    }
}