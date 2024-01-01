using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[Serializable]
public class Ability 
{
    [field: SerializeField] public string AnimationName { get; private set; }

    [field: SerializeField] public float TransitionDuration { get; private set; }
    [field: SerializeField] public float ForceTime { get; private set; }

    [field: SerializeField] public float Force { get; private set; }

    [field: SerializeField] public int Damage { get; private set; }

    [field: SerializeField] public float KnockBack { get; private set; }

    [field: SerializeField] public VisualEffect VFXAttack { get; private set; }


}
