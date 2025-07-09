using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

[BurstCompile(CompileSynchronously = true)]
public struct BurstMathJob : IJob
{
    public NativeArray<int> Input;
    public NativeArray<long> Result;
    public int ArraySize;

    public void Execute()
    {
        for (var i = 0; i < ArraySize; i++)
        {
            Input[i] = i * 2;
        }

        var sum = 0L;
        for (var i = 0; i < ArraySize; i++)
        {
            sum += Input[i] * Input[i];
        }

        Result[0] = sum;
    }
}