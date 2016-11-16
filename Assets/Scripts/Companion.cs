using UnityEngine;
using System.Collections;

public class Companion : MonoBehaviour {

    public GameObject prefab;
    private bool isPlaying = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!isPlaying && !GameManager.Instance.whiteTurn)
        {
            isPlaying = true;
            StartCoroutine(MyCoroutine());
        }
	
	}

    IEnumerator MyCoroutine()
    {

        float time = Random.Range(0.0f, 10.0f);

        yield return new WaitForSeconds(time);

        RaycastHit hit = Shoot();
        var startRot = Quaternion.LookRotation(hit.normal);
        Instantiate(prefab, hit.point, startRot);
        GameManager.Instance.whiteTurn = true;
        isPlaying = false;

    }

    RaycastHit Shoot ()
    {
        RaycastHit hit;
        Vector3 rand = transform.TransformDirection(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        Debug.Log("rando is: " + rand);
        if (Physics.Raycast(transform.position, rand, out hit))
        {
            Debug.Log("trying");
            if (hit.transform.tag != "stone")
            {
                Debug.Log("success!");
                return hit;
            } else
            {
                Debug.Log("retry");
                return Shoot();
            }

        } else
        {
            Debug.Log("giveUp");
            return Shoot();
        }
        

    }

}
