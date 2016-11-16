using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameMangaer");
                go.AddComponent<GameManager>();

            }

            return _instance;
        }
    }

    public bool multiplayer;
    public bool whiteTurn;
    public bool [] change = new bool[10];

    public GameObject[] stonesPlayed;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        whiteTurn = true;
    }
}
