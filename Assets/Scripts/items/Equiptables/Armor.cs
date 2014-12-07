using UnityEngine;
using System.Collections;

public class Armor : Equipment {

    public override void addEquipment()
    {
        PlayerReference.equippedArmor = eqStats;
        PlayerReference.refreshEquipStats();
    }



}
