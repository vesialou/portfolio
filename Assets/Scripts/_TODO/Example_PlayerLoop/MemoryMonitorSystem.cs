using UnityEngine;
using UnityEngine.LowLevel;

public class MemoryMonitorSystem : MonoBehaviour
{
    [System.Serializable]
    public struct MemoryMonitor { }

    private void Start()
    {
        AddMemoryMonitorToPlayerLoop();
    }

    private void OnDestroy()
    {
        PlayerLoop.SetPlayerLoop(PlayerLoop.GetDefaultPlayerLoop());
        Debug.Log("🔄 Player Loop restored to default values");
    }

    private void AddMemoryMonitorToPlayerLoop()
    {
        var playerLoop = PlayerLoop.GetCurrentPlayerLoop();

        var memorySystem = new PlayerLoopSystem
        {
            type = typeof(MemoryMonitor),
            updateDelegate = MonitorMemory
        };

        // Fixed version - proper work with subSystemList
        if (playerLoop.subSystemList != null)
        {
            for (var i = 0; i < playerLoop.subSystemList.Length; i++)
            {
                if (playerLoop.subSystemList[i].type == typeof(UnityEngine.PlayerLoop.PostLateUpdate))
                {
                    var postLateUpdateSystem = playerLoop.subSystemList[i];

                    // Create new subsystems list
                    var existingSubSystems = postLateUpdateSystem.subSystemList ?? new PlayerLoopSystem[0];
                    var newSubSystems = new PlayerLoopSystem[existingSubSystems.Length + 1];

                    // Copy existing systems
                    for (int j = 0; j < existingSubSystems.Length; j++)
                    {
                        newSubSystems[j] = existingSubSystems[j];
                    }

                    // Add our system at the end
                    newSubSystems[existingSubSystems.Length] = memorySystem;

                    // Update system
                    postLateUpdateSystem.subSystemList = newSubSystems;
                    playerLoop.subSystemList[i] = postLateUpdateSystem;
                    break;
                }
            }
        }

        PlayerLoop.SetPlayerLoop(playerLoop);
        Debug.Log("📊 Memory Monitor added to Player Loop");
    }


    private const int BASE_FPS = 60;
    private const int UPDATE_RATE = 5 * BASE_FPS; // Every 5 seconds at 60 FPS
    private const float WARN_MEMEORY = 100f;
    private static void MonitorMemory()
    {
        if (Time.frameCount % UPDATE_RATE == 0)
        {
            var memoryUsage = System.GC.GetTotalMemory(false);
            var memoryMB = memoryUsage / (1024f * 1024f);
            Debug.Log($"💾 Memory usage: {memoryMB:F1} MB");

            if (memoryMB > WARN_MEMEORY) // If more than 100 MB
            {
                Debug.LogWarning("⚠️ High memory usage!");
            }
        }
    }
}