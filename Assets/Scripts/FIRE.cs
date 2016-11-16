using UnityEngine;
using System.Collections;

public class FIRE : MonoBehaviour
{
    
    public GameObject whiteCamera;
    public GameObject blackCamera;
    public GameObject whiteStone;
    private Collider currentHit;
    private bool isOverObject = false;
    public AudioClip destroy;
    AudioSource audio;


    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        GameManager.Instance.whiteTurn = true;
    }


    // Update is called once per frame
    void Update()
    {


        Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            if (Input.GetButtonDown("Fire1") && GameManager.Instance.whiteTurn && hit.transform.tag == "board")
            {
                var startRot = Quaternion.LookRotation(hit.normal);
                GameObject stone = Instantiate(whiteStone, hit.point, startRot) as GameObject;
                GameManager.Instance.stonesPlayed = GameObject.FindGameObjectsWithTag("stone");
                endTurn();
            }

            if (currentHit != hit.collider)
            {
                isOverObject = false;
                if (currentHit)
                {
                    if(currentHit.gameObject.tag == "stone")
                    {
                        currentHit.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
                    }
                }
                currentHit = hit.collider;
            }

            if (hit.transform.tag == "stone" && !isOverObject && GameManager.Instance.whiteTurn)
            {
                isOverObject = true;
                hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.3F, 0.3F, 0.3F));

            }
            if (Input.GetButtonDown("Fire2") && hit.transform.tag == "stone" && GameManager.Instance.whiteTurn)
            {
                audio.PlayOneShot(destroy, 2F);
                Destroy(hit.collider.gameObject);
                endTurn();

            }

        }
    }
    void endTurn ()
    {
        for (int i = 0; i < GameManager.Instance.change.Length; i++)
        {
            Debug.Log("i = "+i);
            GameManager.Instance.change[i] = true;
        }
        GameManager.Instance.whiteTurn = false;

        if (GameManager.Instance.multiplayer)
        {
            whiteCamera.SetActive(false);
            blackCamera.SetActive(true);
        }


    }
}
