using RadgarGames.Singleton;
using RadgarGames.Interface;
using UnityEngine;

namespace RadgarGames.NavMesh.Influencer
{
    public class InfluencerAI : Singleton<InfluencerAI>, IInfluencer
    {
        public Transform GetTransform()
        {
            return transform;
        }
    }
}