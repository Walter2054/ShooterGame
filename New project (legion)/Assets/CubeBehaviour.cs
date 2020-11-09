using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    public float scalemax;
    public float scalemin;
    public float maxOrbitSpeed;
    private float OrbitSpeed;
    public Transform orbitAnchor;
    private Vector3 orbitDirection;
    private Vector3 maxscale;
    public float growningSpeed;
    private bool isScaled=false;
    public int health;
    private bool isAlive = true;
    // Start is called before the first frame update
    void CubeSettings()
    {
        orbitAnchor = Camera.main.transform;
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        orbitDirection = new Vector3(x, y, z);
        OrbitSpeed = Random.Range(.5f, maxOrbitSpeed);
        float scale = Random.Range(scalemin, scalemax);
        maxscale = new Vector3(scale, scale, scale);
        transform.localScale = Vector3.zero;

    }

    // Update is called once per frame
    private void Update()
    {
        RotateCube();
        if (!isScaled)
        {
            ScaleObject();
        }
    }
    private void RotateCube()
    {
        transform.RotateAround(orbitAnchor.position, orbitDirection, OrbitSpeed * Time.deltaTime);
        transform.Rotate(orbitDirection * 30 * Time.deltaTime);
    }
    private void Start()
    {
        CubeSettings();
    }
    void ScaleObject()
    {
        if (transform.localScale != maxscale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, maxscale, Time.deltaTime);
            isScaled = true;
        }
    }
    public bool Hit(int hitDamage)
    {
        health -= hitDamage;
        if(health>=0 && isAlive)
        {
            StartCoroutine("DestroyCube");
            return true;
        }
        return false;
    }
    private IEnumerator DestroyCube()
    {
        isAlive = false;
        GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
