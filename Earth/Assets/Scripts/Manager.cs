using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using System.Diagnostics;
using System;

public class Manager : MonoBehaviour {

    public GameObject prefab;
    private Stopwatch dt = new Stopwatch();

    private List<SatelliteData> satellites;

    public static List<TLEObject> objects;
    private Transform[] trans;
    private ObjectState[] states;

    private int num_obj;

    // Use this for initialization
    void Start () {

        InitializeSatellites();
        InitializeObjects();
	}

    // Update is called once per frame
    void Update() {
        dt.Reset();
        dt.Start();

        for(int i = 0; i< num_obj; i++)
        {
            states[i] = DllAdapter.KeplerianMotionAdapter(states[i], objects[i]);
            trans[i].position = states[i].PositionToVector3(200f);
        }

        dt.Stop();
        DllAdapter.T += Time.deltaTime;
        UnityEngine.Debug.Log(dt.ElapsedMilliseconds);
	}

    private void InitializeSatellites()
    {
        satellites = Parser.ReadAllSatellites("./Assets/Resources/sat_data_clean.txt");
    }

    private void InitializeObjects()
    {
        objects = Parser.ReadAllTLE("./Assets/Resources/TLE_Objects_16_03_2018_clean.txt");

        num_obj = objects.Count;

        trans = new Transform[num_obj];
        states = new ObjectState[num_obj];

        for (int i = 0; i < num_obj; i++)
        {
            GameObject go = InstantiateSatelliteOrDebris(objects[i]); 
            trans[i] = go.transform;
            states[i] = DllAdapter.Kep2CarAdapter(objects[i]);
            //TODO controllare validità conversione
            trans[i].position = states[i].PositionToVector3(200f);
        }
    }

    private GameObject InstantiateSatelliteOrDebris(TLEObject obj)
    {
        //TODO instanziare il prefab di un satellite o di un debris 
        return Instantiate(prefab);
    }

}
