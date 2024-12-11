using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    [SerializeField] AudioSource lobbyMusic;
    [SerializeField] AudioSource[] elevatorMusic;
    [SerializeField] float fadeTime_lobby = 1f;
    [SerializeField] float fadeTime_elevator = 1f;
    [SerializeField] float startDelay = 1f;

    [SerializeField] public bool inElevator = false;
    [SerializeField] public bool onFirstFloor = false;

    [SerializeField] PlayerTriggerZone completionZone1 = null;
    [SerializeField] PlayerTriggerZone completionZone2 = null;
    [SerializeField] PlayerTriggerZone completionZone3 = null;

    Coroutine lobbyRoutine = null;
    Coroutine elevatorRoutine = null;
    private void Awake()
    {
        foreach (PlayerTriggerZone c in GameObject.FindObjectsOfType<PlayerTriggerZone>())//FindObjectsByType(typeof(CompletionZone), FindObjectsSortMode.None))
        {
            if(c.name == "LevelCompletionZone")
            {
                completionZone1 = c;
            }
            if (c.name == "LevelCompletionZone_2")
            {
                completionZone2 = c;
            }
        }
    }

    void Start()
    {
        if(completionZone1 != null)
        {
            completionZone1.PlayerEnteredTriggerZone += InElevator;
            completionZone1.PlayerExitedTriggerZone += LeavingElevator;
            completionZone1.PlayerEnteredTriggerZone += LeaveFirstFloor;
        }

        if(completionZone2 != null)
        {
            completionZone2.PlayerEnteredTriggerZone += InElevator;
            completionZone2.PlayerExitedTriggerZone += LeavingElevator;
        }

        if (completionZone3 != null)
        {
            completionZone3.PlayerEnteredTriggerZone += EndGame;
        }


        Invoke("PlayLobbyMusic", startDelay);
    }

    private void OnDisable()
    {
        if (completionZone3 != null)
        {
            completionZone3.PlayerEnteredTriggerZone -= EndGame;
        }

        if (completionZone2 != null)
        {
            completionZone2.PlayerEnteredTriggerZone -= InElevator;
            completionZone2.PlayerExitedTriggerZone -= LeavingElevator;
        }

        if (completionZone1 != null)
        {
            completionZone1.PlayerEnteredTriggerZone -= InElevator;
            completionZone1.PlayerExitedTriggerZone -= LeavingElevator;
        }
    }

    public void EndGame()
    {
        
    }

    public void PlayLobbyMusic()
    {
        onFirstFloor = true;
        if(lobbyMusic != null)
        {
            lobbyMusic.Play();
        }
    }

    public void InElevator()
    {
        inElevator = true;
        if (elevatorRoutine != null)
        {
            StopCoroutine(elevatorRoutine);
            elevatorRoutine = null;
        }
        elevatorRoutine = StartCoroutine(FadeInRoutine(fadeTime_elevator, elevatorMusic));
    }

    public void LeavingElevator()
    {
        Debug.Log("LEAVING ELEVATOR");
        inElevator = false;

        if(elevatorRoutine != null)
        {
            StopCoroutine(elevatorRoutine);
            elevatorRoutine = null;
        }
        elevatorRoutine = StartCoroutine(FadeOutRoutine(fadeTime_elevator, elevatorMusic));
    }

    public void OnFirstFloor()
    {
        onFirstFloor = true;
        if (lobbyRoutine != null)
        {
            StopCoroutine(lobbyRoutine);
            lobbyRoutine = null;
        }
        lobbyRoutine = StartCoroutine(FadeInRoutine(1f, lobbyMusic));
    }

    public void LeaveFirstFloor()
    {
        //Debug.Log("LEAVING FIRST FLOOR");
        onFirstFloor = false;
        if (lobbyRoutine != null)
        {
            StopCoroutine(lobbyRoutine);
            lobbyRoutine = null;
        }
        lobbyRoutine = StartCoroutine(FadeOutRoutine(fadeTime_lobby, lobbyMusic));
    }

    public IEnumerator FadeOutRoutine(float fadeTime, params AudioSource[] source)
    {
        if (source[0] != null)
        {
            while (source[0].volume > 0f)
            {
                foreach (AudioSource a in source)
                {
                    a.volume -= Time.deltaTime * fadeTime;
                }
                yield return null;
            }
        }

        foreach (AudioSource a in source)
        {
            a.volume = 0f;
        }
        yield return null;
    }

    public IEnumerator FadeInRoutine(float fadeTime, params AudioSource[] source)
    {
        if (source[0] != null)
        {
            while (source[0].volume < 1f)
            {
                foreach (AudioSource a in source)
                {
                    a.volume += Time.deltaTime * fadeTime;
                }
                yield return null;
            }
        }

        foreach (AudioSource a in source)
        {
            a.volume = 1f;
        }
        yield return null;
    }
}
