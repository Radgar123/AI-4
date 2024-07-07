using UnityEngine;

namespace RadgarGames.Interface
{
    public interface IFollower
    {
        void SetTarget(Transform target);
        void FollowTarget();
    }
}