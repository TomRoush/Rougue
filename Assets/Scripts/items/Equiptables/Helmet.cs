using UnityEngine;
using System.Collections;

public class Helmet : Equipment {

    public override void addEquipment()
    {
        PlayerReference.equippedHelmet = eqStats;
        PlayerReference.refreshEquipStats();
    }



}
