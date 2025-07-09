
using UnityEngine;
using UnityEngine.LowLevel;

public class PlayerLoopModifier : MonoBehaviour
{
    private static bool isModified = false;

    [System.Serializable]
    public struct CustomUpdateSystem { }

    [System.Serializable]
    public struct CustomLateUpdateSystem { }

    void Start()
    {
        if (!isModified)
        {
            ModifyPlayerLoop();
            isModified = true;
        }
    }

    void ModifyPlayerLoop()
    {
        // Get current Player Loop
        var playerLoop = PlayerLoop.GetCurrentPlayerLoop();

        Debug.Log("=== ORIGINAL PLAYER LOOP ===");
        PrintPlayerLoop(playerLoop, 0);

        // Create custom systems
        var customUpdateSystem = new PlayerLoopSystem
        {
            type = typeof(CustomUpdateSystem),
            updateDelegate = CustomUpdateFunction
        };

        var customLateUpdateSystem = new PlayerLoopSystem
        {
            type = typeof(CustomLateUpdateSystem),
            updateDelegate = CustomLateUpdateFunction
        };

        // Add custom system to Update
        AddSystemToUpdate(ref playerLoop, customUpdateSystem);

        // Add custom system to PreLateUpdate
        AddSystemToPreLateUpdate(ref playerLoop, customLateUpdateSystem);

        // Set modified Player Loop
        PlayerLoop.SetPlayerLoop(playerLoop);

        Debug.Log("\n=== MODIFIED PLAYER LOOP ===");
        PrintPlayerLoop(playerLoop, 0);

        Debug.Log("\n✅ Player Loop successfully modified!");
    }

    void AddSystemToUpdate(ref PlayerLoopSystem playerLoop, PlayerLoopSystem systemToAdd)
    {
        // Find Update system
        if (playerLoop.subSystemList != null)
        {
            for (int i = 0; i < playerLoop.subSystemList.Length; i++)
            {
                if (playerLoop.subSystemList[i].type == typeof(UnityEngine.PlayerLoop.Update))
                {
                    var updateSystem = playerLoop.subSystemList[i];

                    // Create new subsystems array
                    var existingSubSystems = updateSystem.subSystemList ?? new PlayerLoopSystem[0];
                    var newSubSystems = new PlayerLoopSystem[existingSubSystems.Length + 1];

                    // Add our system at the beginning
                    newSubSystems[0] = systemToAdd;

                    // Copy existing systems
                    for (int j = 0; j < existingSubSystems.Length; j++)
                    {
                        newSubSystems[j + 1] = existingSubSystems[j];
                    }

                    updateSystem.subSystemList = newSubSystems;
                    playerLoop.subSystemList[i] = updateSystem;
                    break;
                }
            }
        }
    }

    void AddSystemToPreLateUpdate(ref PlayerLoopSystem playerLoop, PlayerLoopSystem systemToAdd)
    {
        // Find PreLateUpdate system
        if (playerLoop.subSystemList != null)
        {
            for (int i = 0; i < playerLoop.subSystemList.Length; i++)
            {
                if (playerLoop.subSystemList[i].type == typeof(UnityEngine.PlayerLoop.PreLateUpdate))
                {
                    var preLateUpdateSystem = playerLoop.subSystemList[i];

                    // Create new subsystems array
                    var existingSubSystems = preLateUpdateSystem.subSystemList ?? new PlayerLoopSystem[0];
                    var newSubSystems = new PlayerLoopSystem[existingSubSystems.Length + 1];

                    // Copy existing systems
                    for (int j = 0; j < existingSubSystems.Length; j++)
                    {
                        newSubSystems[j] = existingSubSystems[j];
                    }

                    // Add our system at the end
                    newSubSystems[existingSubSystems.Length] = systemToAdd;

                    preLateUpdateSystem.subSystemList = newSubSystems;
                    playerLoop.subSystemList[i] = preLateUpdateSystem;
                    break;
                }
            }
        }
    }

    // Custom update functions
    static void CustomUpdateFunction()
    {
        // Add any logic that should run every frame
        if (Time.frameCount % 60 == 0) // Every 60 frames
        {
            Debug.Log($"🔄 Custom Update System working! Frame: {Time.frameCount}, Time: {Time.time:F2}s");
        }

        // Example: performance check
        if (Time.deltaTime > 0.033f) // More than 33ms = less than 30 FPS
        {
            Debug.LogWarning($"⚠️ Low performance! DeltaTime: {Time.deltaTime * 1000:F1}ms");
        }
    }

    static void CustomLateUpdateFunction()
    {
        // Logic that should run after main updates
        if (Time.frameCount % 120 == 0) // Every 120 frames
        {
            Debug.Log($"🔄 Custom Late Update System working! Objects: {FindObjectsOfType<MonoBehaviour>().Length}");
        }
    }

    // Utility for printing Player Loop structure
    void PrintPlayerLoop(PlayerLoopSystem system, int depth)
    {
        string indent = new string(' ', depth * 2);
        string systemName = system.type?.Name ?? "Unknown";

        Debug.Log($"{indent}• {systemName}");

        if (system.subSystemList != null)
        {
            foreach (var subSystem in system.subSystemList)
            {
                PrintPlayerLoop(subSystem, depth + 1);
            }
        }
    }

    // Method to restore original Player Loop
    [ContextMenu("Reset Player Loop")]
    public void ResetPlayerLoop()
    {
        PlayerLoop.SetPlayerLoop(PlayerLoop.GetDefaultPlayerLoop());
        isModified = false;
        Debug.Log("🔄 Player Loop restored to default values");
    }

    void OnDestroy()
    {
        // Restore original Player Loop when object is destroyed
        if (isModified)
        {
            ResetPlayerLoop();
        }
    }
}