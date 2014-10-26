using UnityEngine;
using System.Collections;

public abstract partial class Entities : MonoBehaviour {


        private Vector3 direction;

        public void setDirection( Vector3 dir)
        {
            direction = dir.normalized;
        }

        protected void Move() {
            if(rigidbody2D == null)
                Debug.Log("things be wwronmg");
            else
                Debug.Log("is all cool");

            this.rigidbody2D.velocity = direction * cStat.speed * cStat.getSpeedx();
        }

}
