using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TurretArm", menuName = "Scriptable Objects/Arm/TurretArm")]
public class TurretArm : ScriptableObject
{
    public ArmState state;
    public ArmPosition position;
}
public enum ArmState { open, close };
public enum ArmPosition { left, right, back };
