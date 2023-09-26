using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] raiderSkillSounds;
    [SerializeField] private AudioSource[] undeadSkillSounds;
    [SerializeField] private AudioSource[] footmanSkillSounds;
    [SerializeField] private AudioSource[] playerSkillSounds;

    public AudioSource RaiderSkillSound(int i)
    {
        return raiderSkillSounds[i];
    }

    public AudioSource UndeadSkillSound(int i)
    {
        return undeadSkillSounds[i];
    }

    public AudioSource FootmanSkillSound(int i)
    {
        return footmanSkillSounds[i];
    }

    public AudioSource PlayerSkillSound(int i)
    {
        return playerSkillSounds[i];
    }
}