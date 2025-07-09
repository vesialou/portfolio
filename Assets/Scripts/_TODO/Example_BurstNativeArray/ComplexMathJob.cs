using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

[BurstCompile(CompileSynchronously = true)]
public struct ComplexMathJob : IJobParallelFor
{
    public NativeArray<float> Data;
    public int Size;

    public void Execute(int index)
    {
        float x = index * 0.01f;
        float y = index * 0.02f;

        Data[index] = Unity.Mathematics.math.sin(x) * Unity.Mathematics.math.cos(y) +
                     Unity.Mathematics.math.sqrt(index);
    }
}