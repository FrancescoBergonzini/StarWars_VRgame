using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SW_VRGame
{
    public class SW_EndGameCollider : MonoBehaviour
    {
        public Action checkRobotCollision;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != Tags.Sliceable)
                return;

            
            checkRobotCollision();
            Destroy(other);
        }
    }
}

