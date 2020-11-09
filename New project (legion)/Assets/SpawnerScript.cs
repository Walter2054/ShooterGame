using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SpawnerScript : MonoBehaviour
{ public GameObject spawnee;
    public int TotalSpawnee;
    public float TimeToSpawn;
    private GameObject[] SpawneeList;
    private bool PositionSet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangePositon());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool SetPosition()
    {
        Transform cam = Camera.main.transform;
        transform.position = cam.forward * 10;
        return true;
    }

    private IEnumerator ChangePositon()
    {
        yield return new WaitForSeconds(0.2f);
        if (PositionSet) 
        {
            if (VuforiaBehaviour.Instance.enabled)
            {
                SetPosition();
            }
        }
    }
    private IEnumerator Spawnloop()
    {
        yield return new WaitForSeconds(1f);
        int i = 0;
            while (i <= TotalSpawnee - 1)
        {
            SpawneeList[i] = SpawnElement();
            i++;
        }
        yield return new WaitForSeconds(1.5f);
    }
    private GameObject SpawnElement()
    {
        GameObject spawnobject = Instantiate(spawnee, (Random.insideUnitSphere * 4), transform.rotation);
        float scale = Random.Range(0.5f, 2f);
        spawnobject.transform.localScale = new Vector3(scale, scale, scale);
        return spawnobject;
    }
}
