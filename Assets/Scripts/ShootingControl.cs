using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingControl : MonoBehaviour
{
    public GameObject blaster1;
    public GameObject blaster2;

    public AudioSource shot_sound;
   
    private Shooter shooter1;
    private Shooter shooter2;

    void Start()
    {
        shooter1 = blaster1.GetComponent<Shooter>();
        shooter2 = blaster2.GetComponent<Shooter>();
    }

    public void Fire()
    {
        shooter1.Fire();
        shooter2.Fire();
        shot_sound.Play();
    }
}
