using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Starter", menuName = "Scriptable Objects/Turret/Primary/Starter")]
public class Starter : ScriptableObject
{
    public StarterState state;
    public StarterPosition position;
}
public enum StarterState { Open, Close }
public enum StarterPosition { Left, Right }
