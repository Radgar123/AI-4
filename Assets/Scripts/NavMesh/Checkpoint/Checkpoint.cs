using UnityEngine;

namespace RadgarGames.NavMesh.Checkpoints
{
    public class Checkpoint : MonoBehaviour
    {
        public int id;
        public Vector3 Position => transform.position;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}