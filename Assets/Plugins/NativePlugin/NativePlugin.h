#pragma once

#ifdef _WIN32
#define DLL_EXPORT extern "C" __declspec(dllexport)
#else
#define DLL_EXPORT extern "C"
#endif

#include <stddef.h>

// Structures
struct Vector3 {
    float x, y, z;
};

struct GameStats {
    int score;
    float health;
    bool isAlive;
    char playerName[32];
};

// Callback
typedef void (*LogCallback)(const char* message);

// Functions
DLL_EXPORT int AddNumbers(int a, int b);
DLL_EXPORT float CalculateDistance(float x1, float y1, float x2, float y2);
DLL_EXPORT int GetStringLength(const char* str);
DLL_EXPORT char* CreateGreeting(const char* name);
DLL_EXPORT void FreeString(char* str);

DLL_EXPORT void ProcessArray(int* array, int size);
DLL_EXPORT float CalculateSum(float* array, int size);

DLL_EXPORT float Vector3Magnitude(Vector3 vec);
DLL_EXPORT Vector3 Vector3Normalize(Vector3 vec);
DLL_EXPORT void UpdateGameStats(GameStats* stats, int scoreBonus);

DLL_EXPORT void SetLogCallback(LogCallback callback);
DLL_EXPORT void ProcessWithLogging(int iterations);

DLL_EXPORT void FastMatrixMultiply(float* a, float* b, float* result, int size);
DLL_EXPORT void ParallelProcess(int* data, int size, int threads);
