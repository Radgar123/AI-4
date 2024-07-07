using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RadgarGames.NavMesh.Checkpoints
{
    public class CheckpointGraph : MonoBehaviour
    {
        [SerializeField] private GameObject _checkpointsParent;
        [Tooltip("If checked, checkpoints will be automatically found in the scene. If unchecked, checkpoints will be taken from the children of the specified parent object.")]
        [SerializeField] private bool _autoFindCheckpoints = true;

        private Dictionary<int, List<int>> _graph = new Dictionary<int, List<int>>();
        private Dictionary<int, Checkpoint> _checkpoints = new Dictionary<int, Checkpoint>();

        public void AddCheckpoint(Checkpoint checkpoint)
        {
            if (!_checkpoints.ContainsKey(checkpoint.id))
            {
                _checkpoints.Add(checkpoint.id, checkpoint);
                _graph.Add(checkpoint.id, new List<int>());
            }
        }

        public void AddEdge(int fromId, int toId)
        {
            if (_graph.ContainsKey(fromId) && _graph.ContainsKey(toId))
            {
                _graph[fromId].Add(toId);
                _graph[toId].Add(fromId);
            }
        }

        public List<Checkpoint> GetShortestPath(int fromId, int toId)
        {
            var previous = new Dictionary<int, int>();
            var distances = new Dictionary<int, float>();
            var nodes = new List<int>();

            List<Checkpoint> path = null;

            foreach (var checkpoint in _graph)
            {
                if (checkpoint.Key == fromId)
                {
                    distances[checkpoint.Key] = 0;
                }
                else
                {
                    distances[checkpoint.Key] = float.MaxValue;
                }

                nodes.Add(checkpoint.Key);
            }

            while (nodes.Count != 0)
            {
                nodes.Sort((x, y) => distances[x].CompareTo(distances[y]));

                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest == toId)
                {
                    path = new List<Checkpoint>();
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(_checkpoints[smallest]);
                        smallest = previous[smallest];
                    }

                    path.Reverse();
                    break;
                }

                if (distances[smallest] == float.MaxValue)
                {
                    break;
                }

                foreach (var neighbor in _graph[smallest])
                {
                    var alt = distances[smallest] + Vector3.Distance(_checkpoints[smallest].Position, _checkpoints[neighbor].Position);
                    if (alt < distances[neighbor])
                    {
                        distances[neighbor] = alt;
                        previous[neighbor] = smallest;
                    }
                }
            }

            return path;
        }

        public void InitializeGraph()
        {
            Checkpoint[] checkpoints;

            if (_autoFindCheckpoints)
            {
                checkpoints = FindObjectsOfType<Checkpoint>();
            }
            else
            {
                checkpoints = _checkpointsParent.GetComponentsInChildren<Checkpoint>();
            }

            foreach (var checkpoint in checkpoints)
            {
                AddCheckpoint(checkpoint);
            }

            var checkpointIds = new List<int>(_checkpoints.Keys);
            checkpointIds.Sort();

            for (int i = 0; i < checkpointIds.Count - 1; i++)
            {
                AddEdge(checkpointIds[i], checkpointIds[i + 1]);
            }
        }

        private void Start()
        {
            InitializeGraph();
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                InitializeGraph();
            }

            Gizmos.color = Color.green;
            foreach (var checkpoint in _checkpoints.Values)
            {
                foreach (var neighborId in _graph[checkpoint.id])
                {
                    var neighbor = _checkpoints[neighborId];
                    Gizmos.DrawLine(checkpoint.Position, neighbor.Position);

                    // Draw arrow
                    DrawArrow(checkpoint.Position, neighbor.Position);

                    // Draw numbers
                    Vector3 midPoint = (checkpoint.Position + neighbor.Position) / 2;
                    Handles.Label(midPoint, $"{checkpoint.id} -> {neighbor.id}");
                }
            }
        }

        private void DrawArrow(Vector3 start, Vector3 end)
        {
            Vector3 direction = (end - start).normalized;
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + 20, 0) * Vector3.forward;
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - 20, 0) * Vector3.forward;

            Gizmos.DrawLine(end, end + right * 0.5f);
            Gizmos.DrawLine(end, end + left * 0.5f);
        }
    }
}
