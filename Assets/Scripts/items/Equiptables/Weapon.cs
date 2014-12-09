using UnityEngine;
using System.Collections;

public class Weapon : Equipment {

    public override void addEquipment()
    {
        PlayerReference.equippedSword = eqStats;
        PlayerReference.refreshEquipStats();
    }



}
