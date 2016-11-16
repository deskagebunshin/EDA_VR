using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{
    AudioSource audio;
    public GameObject prefab;
    private bool isPlaying = false;
    public AudioClip destroy;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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



        if (Random.Range(0,100) < 25)
        {
            int x = Random.Range(0, GameManager.Instance.stonesPlayed.Length);
            Destroy(GameManager.Instance.stonesPlayed[x]);
            audio.PlayOneShot(destroy, 2F);
        } else
        {
            RaycastHit hit = Shoot();

            var startRot = Quaternion.LookRotation(hit.normal);
            Instantiate(prefab, hit.point, startRot);
        }
        GameManager.Instance.whiteTurn = true;
        isPlaying = false;


    }

    RaycastHit Shoot()
    {
        RaycastHit hit;
        Vector3 rand = transform.TransformDirection(Random.Range(-0.5f, 0.5f), Random.Range(-1.0f, 0.0f), Random.Range(0.0f, 1.0f));
        
        if (Physics.Raycast(transform.position, rand, out hit))
        {

            if (hit.transform.tag == "board" )
            {


               // Debug.Log("rando is: " + rand);
                return hit;
                
            }
            else
            {
                return Shoot();
            }

        }
        else
        {
            return Shoot();
        }


    }

}
