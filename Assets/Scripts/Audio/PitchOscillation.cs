using UnityEngine;

public class PitchOscillation : MonoBehaviour
{
    [SerializeField] AudioSource audioSource = null;
    [SerializeField] Vector2 pitchRange = new Vector2();

    void Update()
    {
        if(audioSource != null)
        {
            audioSource.pitch = 1f + Mathf.Sin(Time.time) * pitchRange.y;
            //Debug.Log(audioSource.pitch);
        }
    }
}
