using UnityEngine;
using System.Collections;

namespace Pathfinding
{
	[UniqueComponent(tag = "ai.destination")]
	public class OrbPathTarget : VersionedMonoBehaviour
	{
		private Transform target;
		IAstarAI ai;

		void Start()
        {
			target = GameObject.Find("PlayerTarget").transform;
		}

		void OnEnable()
		{
			ai = GetComponent<IAstarAI>();
			if (ai != null) ai.onSearchPath += Update;
		}

		void OnDisable()
		{
			if (ai != null) ai.onSearchPath -= Update;
		}

		void Update()
		{
			if (target != null && ai != null) ai.destination = target.position;
		}
	}
}
