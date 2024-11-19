using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Game Manager is Null!");
            }
            return instance;
        }

    }

    public bool HasCard { get; set; }
    public PlayableDirector introCutscene;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            introCutscene.time = 62.0f;
            AudioManager.Instance.PlayMusic();
        }
    }
}
