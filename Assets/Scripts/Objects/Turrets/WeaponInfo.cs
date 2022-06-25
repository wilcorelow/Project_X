using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Info", menuName = "Scriptable Objects/Weapon Info")]
public class WeaponInfo : ScriptableObject
{
    public WeaponInfoPosition position;
    public WeaponInfoState state;
}
public enum WeaponInfoPosition { Left, Right, Back }
public enum WeaponInfoState { Open, Close }
