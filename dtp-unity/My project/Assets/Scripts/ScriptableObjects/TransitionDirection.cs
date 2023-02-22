using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransitionType
{
    SetOverlayAlpha,
    FadeInToScene,
    FadeToBlack,
}
[CreateAssetMenu(fileName = "TransitionDirection", menuName = "DialogueObjects/TransitionDirection", order = 0)]
public class TransitionDirection : Direction
{
    public TransitionType TransitionType;
    public float alphaLevel;
    public float DurationOfTransition;
    public float DurationDelay;
}
