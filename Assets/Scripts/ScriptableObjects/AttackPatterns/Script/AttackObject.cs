using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Object", menuName = "Weapons/Attack Pattern/Sword")]
public class AttackObject : ScriptableObject
{
    public AnimationClip[] attackCombo;
}
