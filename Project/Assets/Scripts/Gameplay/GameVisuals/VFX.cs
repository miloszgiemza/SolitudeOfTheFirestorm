using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    public VFXID ID => id;

    [SerializeField] private VFXID id;

    private ParticleSystem particleSystem;

    private void OnEnable()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void PlayEffect()
    {
        particleSystem.Play();
    }
}
