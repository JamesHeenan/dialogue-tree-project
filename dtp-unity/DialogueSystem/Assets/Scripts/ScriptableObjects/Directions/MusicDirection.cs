using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicDirection", menuName = "DialogueObjects/MusicDirection", order = 0)]
public class MusicDirection : Direction
{
    public bool loop;
    public AudioClip Music;
}
