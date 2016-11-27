using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour {

    private TileManager _tileManager;

    //public GameObject _objects;

    [SerializeField]
    private float waitSpawnTime, minIntervalTime, maxIntervalTime;

    private List<Object> objs = new List<Object>();

    public float _lat, _lon;
    public ObjectType _type;
    

    // Use this for initialization
    void Start () {
        _tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();

        //オブジェクトの初期化
        //_objects = GameObject.CreatePrimitive(PrimitiveType.Cube);
        CreateObject(_lat, _lon, _type);
    }
	
	// Update is called once per frame
	void Update () {
        //if (waitSpawnTime < Time.time)
        //{
        //    waitSpawnTime = Time.time + UnityEngine.Random.Range(minIntervalTime, maxIntervalTime);
        //    CreateObject(_lat, _lon, _type);
        //}
        

    }

    public void CreateObject(float lat, float lon, ObjectType type)
    {
        //ObjectType type = ObjectType.CASTLE;
        //float newLat = 1.378619f;
        //float newLon = 103.849837f;

        //ObjectType type = ObjectType.CASTLE;// (ObjectType)(int)UnityEngine.Random.Range(0, Enum.GetValues(typeof(ObjectType)).Length);
        float newLat = _tileManager.getLat + UnityEngine.Random.Range(-0.0001f, 0.0001f);
        float newLon = _tileManager.getLon + UnityEngine.Random.Range(-0.0001f, 0.0001f);

        Object prefab = Resources.Load("MapObject/" + type.ToString(), typeof(Object)) as Object;
        Object obj = Instantiate(prefab, Vector3.zero, Quaternion.identity) as Object;
        obj.tileManager = _tileManager;
        obj.Init(newLat, newLon);

        objs.Add(obj);
    }

    public void UpdateObjectPosition()
    {
        if (objs.Count == 0)
            return;

        Object[] obj = objs.ToArray();
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].UpdatePosition();
        }
    }
}
