using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Puzzles.Hack{
    public class Line : MonoBehaviour{
        private BoxCollider boxCollider;
        public bool Connected{ get; private set; }

        public void Reset(){
            Connected = false;
        }

        private void OnDrawGizmos(){
            Transform myTransform = transform;
            Vector3 center = myTransform.position + myTransform.right * boxCollider.center.x;

            Gizmos.DrawCube(center, boxCollider.size);
        }

        private bool CheckForOtherLine(IEnumerable<Collider> colliders){
            return colliders.Any(resultCollider =>
                resultCollider
                && resultCollider != boxCollider
                && resultCollider.gameObject.CompareTag("HackConnection"));
        }

        public void SetupLine(){
            boxCollider = GetComponent<BoxCollider>();
        }

        public void CheckConnection(){
            Transform myTransform = transform;
            Vector3 center = myTransform.position + myTransform.right * boxCollider.center.x;
            var colliders = new Collider[2];
            int size = Physics.OverlapBoxNonAlloc(center, boxCollider.size / 2, colliders, Quaternion.identity,
                LayerMask.NameToLayer("Puzzle"));
            if (!CheckForOtherLine(colliders)) return;
            Connected = true;
        }
    }
}