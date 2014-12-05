using UnityEngine;
using System.Collections;

public abstract partial class Entities : MonoBehaviour {


        private Vector3 direction;

        public void setDirection( Vector3 dir)
        {
            direction = dir.normalized;
        }

        protected void Move() {
            this.rigidbody2D.velocity = direction * cStat.getSpeed();
        }

}
