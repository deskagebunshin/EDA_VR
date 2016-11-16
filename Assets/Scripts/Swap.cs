using UnityEngine;
using System.Collections;

public class Swap : MonoBehaviour {
    public int id;
    public float speed;
    public int currentGameObject = 0;
    public GameObject[] Characters;

	// Use this for initialization
	void Start () {
        if(id == null)
        {
            id = 0;
        }
        GameManager.Instance.change[id] = false;
        Change();
       // StartCoroutine(MyCoroutine());
    }

    // Update is called once per frame
    void Update () {

        if (GameManager.Instance.change[id] == true)
        {
            Change();
            StartCoroutine(Timer());
        }

    }

    void Change()
    {
        currentGameObject = Random.Range(0, Characters.Length);
        int i = 0;
        foreach (GameObject character in Characters)
        {
            if (i == currentGameObject)
            {
                character.SetActive(true);
            }
            else
            {
                character.SetActive(false);
            }
            i++;
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.change[id] = false;
    }

  /*  IEnumerator MyCoroutine()
    {
        currentGameObject = Random.Range(0, Characters.Length);
        int i = 0;
        foreach (GameObject character in Characters)
        {
            if(i == currentGameObject)
            {
                character.SetActive(true);
            } else
            {
                character.SetActive(false);
            }
            i++;
        }

        yield return new WaitForSeconds(Random.Range(0, 10));

        Change();
    }*/
}