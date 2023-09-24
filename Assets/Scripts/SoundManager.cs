using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] raiderSkillSounds;
    [SerializeField] private AudioSource[] undeadSkillSounds;
    [SerializeField] private AudioSource[] footmanSkillSounds;

    public AudioSource RaiderSkillSound(int i)
    {
        return raiderSkillSounds[i];
    }

    public AudioSource UndeadSkillSound(int i)
    {
        return raiderSkillSounds[i];
    }

    public AudioSource FootmanSkillSound(int i)
    {
        return raiderSkillSounds[i];
    }
}