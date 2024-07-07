using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RadgarGames.NavMesh.Checkpoints;

namespace RadgarGames.UI
{
    public class CommandUI : MonoBehaviour
    {
        [SerializeField] private Button _moveButton;
        [SerializeField] private CheckpointGraph _checkpointGraph;
        [SerializeField] private Transform _player;

        private void Start()
        {
            _moveButton.onClick.AddListener(OnMoveButtonClicked);
        }

        private void OnMoveButtonClicked()
        {
            List<Checkpoint> path = _checkpointGraph.GetShortestPath(1, 2);
            if (path != null && path.Count > 0)
            {
                StartCoroutine(MoveAlongPath(path));
            }
        }

        private System.Collections.IEnumerator MoveAlongPath(List<Checkpoint> path)
        {
            foreach (var checkpoint in path)
            {
                while (Vector3.Distance(_player.position, checkpoint.Position) > 0.1f)
                {
                    _player.position = Vector3.MoveTowards(_player.position, checkpoint.Position, Time.deltaTime * 5);
                    yield return null;
                }
            }
        }
    }
}