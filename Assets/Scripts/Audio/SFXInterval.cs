using UnityEngine;

public class SFXInterval : SFXMaker
{
    [Header("Interval")]
    [SerializeField] Vector2 intervalRange = new Vector2(5f, 15f);
    float internalTimer = 0f;

    void Update()
    {
        if (internalTimer <= 0f)
        {
            try
            {
                base.MakeSound(out float clipLength);
                internalTimer = clipLength + Random.Range(intervalRange.x, intervalRange.y);
            }
            catch { }

        }
        else
        {
            internalTimer -= Time.deltaTime;
        }
    }
}
