using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Slower", menuName = "Scriptable Objects/Turret/Secondary/Slower")]
public class Slower : ScriptableObject
{
    public SlowerPosition position;
    public SlowerState state;
}
public enum SlowerPosition { Fronts, Backs }
public enum SlowerState { Open, Close }
