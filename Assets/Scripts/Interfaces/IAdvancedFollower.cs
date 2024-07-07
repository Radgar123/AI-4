using UnityEngine;
using UnityEngine.Events;

namespace RadgarGames.Interface
{
    public interface IAdvancedFollower : IFollower
    {
        float DetectionRange { get; }
        float ActionRange { get; }
        UnityEvent OnAction { get; }
        Transform GetTransform();
    }
}