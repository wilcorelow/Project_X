using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shotgun", menuName = "Scriptable Objects/Turret/Primary/Shotgun")]
public class Shotgun : ScriptableObject
{
    public ShotgunState state;
    public ShotgunPosition position;
}
public enum ShotgunState { Open, Close}
public enum ShotgunPosition { Left, Right }
