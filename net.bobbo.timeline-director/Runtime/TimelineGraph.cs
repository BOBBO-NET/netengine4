using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BobboNet.Timelines
{
    [CreateAssetMenu(fileName = "Timeline Graph", menuName = "Bobbo-Net/Timeline Graph", order = 200)]
    public class TimelineGraph : NodeGraph
    {
        public TimelineNode startingNode;
    }
}
