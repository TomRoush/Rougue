using UnityEngine;
using System.Collections;

public class Necklace : Equipment {

    public override void addEquipment()
    {
        PlayerReference.equippedNecklace = eqStats;
        PlayerReference.refreshEquipStats();
    }



}
