using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public float range=10f;
    public GameObject effect;
    public float force = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
            {
                Debug.Log(hit.collider.name);

                GameObject fx = Instantiate(effect, hit.point, Quaternion.identity);
                Destroy(fx, 0.5f);

                if (hit.collider.GetComponent<Rigidbody>() != null) {
                    hit.collider.GetComponent<Rigidbody>().AddForce(hit.normal * force);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * range);
    }
}
