using System;
using UnityEngine;

public class FootstepInterval : SFXMaker
{
    [SerializeField] Player player;

    [SerializeField] float baseInterval;
    [SerializeField] float intervalMod;
    [SerializeField] float speed;

    float timer = 0f;

    void Update()
    {
        if (player.currentHealth <= 0f) { return; }
        speed = player.currentSpeed;

        if(speed > 0f)
        {
            float diff = Mathf.Abs(speed - 5); //how much faster than normal walking speed
            timer += Time.deltaTime;
            if (timer > baseInterval - ((diff>0f)?intervalMod:0f))
            {
                base.MakeSound();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }
    }
}
