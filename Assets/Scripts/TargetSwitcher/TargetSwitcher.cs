using NaughtyAttributes;
using UnityEngine;
using RadgarGames.Interface;
using RadgarGames.NavMesh.Influencer;

public class TargetSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _followersParent;
    [SerializeField] private Transform _playerTransform;

    private IFollower[] _followers;

    private void Awake()
    {
        _followers = _followersParent.GetComponentsInChildren<IFollower>();
    }

    [Button]
    public void SetTargetToPlayer()
    {
        var player = _playerTransform.GetComponent<IInfluencer>();
        if (player != null)
        {
            foreach (var follower in _followers)
            {
                follower.SetTarget(player.GetTransform());
            }
        }
    }

    [Button]
    public void SetTargetToInfluencer()
    {
        var influencer = InfluencerAI.Instance;
        if (influencer != null)
        {
            foreach (var follower in _followers)
            {
                follower.SetTarget(influencer.GetTransform());
            }
        }
    }

    public void SetTarget(IInfluencer influencer)
    {
        foreach (var follower in _followers)
        {
            follower.SetTarget(influencer.GetTransform());
        }
    }
}