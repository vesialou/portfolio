// 1. Include the header file
#include "NativePlugin.h"
#include <string>
#include <cmath>
#include <cstring>
#include <iostream>
#include <mutex>

// 2. Define the class with internal logic
class NativePlugin
{
private:
    static LogCallback s_logCallback;

public:
    static int AddNumbers(int a, int b)
    {
        return a + b;
    }

    static float CalculateDistance(float x1, float y1, float x2, float y2)
    {
        float dx = x2 - x1;
        float dy = y2 - y1;
        return sqrt(dx * dx + dy * dy);
    }

    static int GetStringLength(const char* str)
    {
        if (str == nullptr) 
        {
            return 0;
        }
        return static_cast<int>(strlen(str));
    }

    static char* CreateGreeting(const char* name)
    {
        if (name == nullptr)
        {
            return nullptr;
        }

        std::string greeting = "INternal message from plugin C++, " + std::string(name) + "!";
        char* result = new char[greeting.length() + 1];
        strcpy_s(result, greeting.length() + 1, greeting.c_str());
        return result;
    }

    static void FreeString(char* str)
    {
        if (str != nullptr)
        {
            delete[] str;
        }
    }

    static void ProcessArray(int* array, int size)
    {
        for (int i = 0; i < size; i++)
        {
            array[i] *= 2;
        }
    }

    static float CalculateSum(float* array, int size)
    {
        float sum = 0.0f;
        for (int i = 0; i < size; i++)
        {
            sum += array[i];
        }
        return sum;
    }

    static float Vector3Magnitude(Vector3 vec)
    {
        return sqrt(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);
    }

    static Vector3 Vector3Normalize(Vector3 vec)
    {
        float magnitude = Vector3Magnitude(vec);
        if (magnitude > 0)
        {
            vec.x /= magnitude;
            vec.y /= magnitude;
            vec.z /= magnitude;
        }
        return vec;
    }

    static void UpdateGameStats(GameStats* stats, int scoreBonus)
    {
        if (stats != nullptr)
        {
            stats->score += scoreBonus;
            if (stats->health <= 0)
            {
                stats->isAlive = false;
            }
        }
    }

    static void SetLogCallback(LogCallback callback)
    {
        s_logCallback = callback;
    }

    static void ProcessWithLogging(int iterations)
    {
        if (s_logCallback)
        {
            s_logCallback("Starting processing...");
        }

        for (int i = 0; i < iterations; i++)
        {
            if (s_logCallback && i % 10 == 0)
            {
                std::string msg = "Processing iteration: " + std::to_string(i);
                s_logCallback(msg.c_str());
            }
        }

        if (s_logCallback)
        {
            s_logCallback("Processing completed!");
        }
    }

    static void FastMatrixMultiply(float* a, float* b, float* result, int size)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result[i * size + j] = 0;
                for (int k = 0; k < size; k++)
                {
                    result[i * size + j] += a[i * size + k] * b[k * size + j];
                }
            }
        }
    }

    static void ParallelProcess(int* data, int size, int threads)
    {
        int chunkSize = size / threads;
        for (int t = 0; t < threads; t++)
        {
            int start = t * chunkSize;
            int end = (t == threads - 1) ? size : start + chunkSize;
            for (int i = start; i < end; i++)
            {
                data[i] = data[i] * data[i];
            }
        }
    }
};


// 3. Define static variables (must be outside the class)
LogCallback NativePlugin::s_logCallback = nullptr;

// 4. Provide C-style API functions to be exported via DLL
extern "C"
{
    DLL_EXPORT int AddNumbers(int a, int b)
    {
        return NativePlugin::AddNumbers(a, b);
    }

    DLL_EXPORT float CalculateDistance(float x1, float y1, float x2, float y2)
    {
        return NativePlugin::CalculateDistance(x1, y1, x2, y2);
    }

    DLL_EXPORT int GetStringLength(const char* str)
    {
        return NativePlugin::GetStringLength(str);
    }

    DLL_EXPORT char* CreateGreeting(const char* name)
    {
        return NativePlugin::CreateGreeting(name);
    }

    DLL_EXPORT void FreeString(char* str)
    {
        NativePlugin::FreeString(str);
    }

    DLL_EXPORT void ProcessArray(int* array, int size)
    {
        NativePlugin::ProcessArray(array, size);
    }

    DLL_EXPORT float CalculateSum(float* array, int size)
    {
        return NativePlugin::CalculateSum(array, size);
    }

    DLL_EXPORT float Vector3Magnitude(Vector3 vec)
    {
        return NativePlugin::Vector3Magnitude(vec);
    }

    DLL_EXPORT Vector3 Vector3Normalize(Vector3 vec)
    {
        return NativePlugin::Vector3Normalize(vec);
    }

    DLL_EXPORT void UpdateGameStats(GameStats* stats, int scoreBonus)
    {
        NativePlugin::UpdateGameStats(stats, scoreBonus);
    }

    DLL_EXPORT void SetLogCallback(LogCallback callback)
    {
        NativePlugin::SetLogCallback(callback);
    }

    DLL_EXPORT void ProcessWithLogging(int iterations)
    {
        NativePlugin::ProcessWithLogging(iterations);
    }

    DLL_EXPORT void FastMatrixMultiply(float* a, float* b, float* result, int size)
    {
        NativePlugin::FastMatrixMultiply(a, b, result, size);
    }

    DLL_EXPORT void ParallelProcess(int* data, int size, int threads)
    {
        NativePlugin::ParallelProcess(data, size, threads);
    }
}
