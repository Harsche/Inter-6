using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Puzzles.Hack{
    public class Line : MonoBehaviour{
        private SphereCollider sphereCollider;
        public bool Connected{ get; private set; }

        public void ResetLine(){
            Connected = false;
        }

        private bool CheckForOtherLine(IEnumerable<Collider> colliders){
            return colliders.Any(resultCollider =>
                resultCollider
                && resultCollider != sphereCollider
                && resultCollider.gameObject.CompareTag("HackConnection"));
        }

        public void SetupLine(){
            sphereCollider = GetComponent<SphereCollider>();
        }

        public void CheckConnection(){
            Transform myTransform = transform;
            Vector3 center = myTransform.position + myTransform.right * sphereCollider.center.x;
            var colliders = new Collider[2];
            Physics.OverlapSphereNonAlloc(center, sphereCollider.radius / 2f, colliders,
                LayerMask.GetMask("Puzzle"), QueryTriggerInteraction.Collide);
            if (!CheckForOtherLine(colliders)){ return; }

            Connected = true;
        }
    }
}