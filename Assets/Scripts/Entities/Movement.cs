using UnityEngine;
using System.Collections;

public abstract partial class Entities : MonoBehaviour {


        private Vector3 direction;

        public void setDirection( Vector3 dir)
        {
            direction = dir.normalized;
        }

        protected void Move() {
			if(direction.x < 0) {
				Vector3 theScale = transform.localScale;
				theScale.x = 1;
				transform.localScale = theScale;
			} else {
				Vector3 theScale = transform.localScale;
				theScale.x = -1;
				transform.localScale = theScale;
			}
            this.rigidbody2D.velocity = direction * cStat.getEffectiveSpeed();
        }

}
