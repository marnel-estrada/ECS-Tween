using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECSTween
{
    [UpdateAfter(typeof(TweenTimeUpdateGroup))]
    [UpdateInGroup(typeof(TweenEasingUpdateGroup))]
    public class TweenEasingExpInSystem : JobComponentSystem
    {
        [BurstCompile]
        [RequireComponentTag(typeof(TweenEasingExpIn))]
        struct TweenExpInEasingJob : IJobProcessComponentData<TweenTime>
        {
            public void Execute(ref TweenTime time)
            {
                time.Value = time.Value == 0f ? 0f : math.pow(1024f, time.Value - 1f);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return new TweenExpInEasingJob().Schedule(this, 128, inputDeps);
        }
    }
}