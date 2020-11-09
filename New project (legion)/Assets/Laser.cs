using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Laser : MonoBehaviour
{
    public float firerate;
    public float FireRange;
    public float HitForce;
    public int LaserDamage;
    private LineRenderer laserline;
    private bool laserlineEnabled;
    private WaitForSeconds laserDuration;
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        laserline = GetComponent<LineRenderer>();
    }
    void Fire()
    {
        Transform cam = Camera.main.transform;
        nextFire = Time.time + firerate;
        Vector3 rayorigin = cam.position;
        laserline.SetPosition(0, transform.up * -10);
        RaycastHit hit;
        if (Physics.Raycast(rayorigin, cam.forward, out hit, FireRange))
        {
            laserline.SetPosition(1, hit.point);
            CubeBehaviour cubecrt1 = hit.collider.GetComponent<CubeBehaviour>();
            if(cubecrt1!=null)
            {
                if (hit.rigidbody != null) ;
                {
                    hit.rigidbody.AddForce(-hit.normal * HitForce);
                    cubecrt1.Hit(LaserDamage);
                }
            }
        }
        else
        {
            laserline.SetPosition(1, cam.forward * FireRange);
        }
        StartCoroutine("LaserFX");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }
    private IEnumerator LaserFX()
    {
        laserline.enabled = true;
        yield return laserDuration;
        laserline.enabled = false;
    }
}
