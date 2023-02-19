using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransitionType
{
    FadeInToScene,
    FadeToBlack,
}
[CreateAssetMenu(fileName = "TransitionDirection", menuName = "DialogueObjects/TransitionDirection", order = 0)]
public class TransitionDirection : DirectionBase
{
    public TransitionType TransitionType;
    public float DurationOfTransition;
    public float DurationDelay;
}
