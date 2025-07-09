using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Scripting;

public class Il2CppAotExamples : MonoBehaviour
{
    void Start()
    {
        RunGenericDelegateExample();
        RunReflectionExample();
    }

    //Example 1: Generic delegate — may be stripped in IL2CPP if not explicitly used
    void RunGenericDelegateExample()
    {
        // this explicit call ensures IL2CPP generates code for T = int
        UseDelegate<int>(x => x * x);

        // If you did this instead, it might fail in IL2CPP:
        /// Func<int, int> square = x => x * x;
        /// UseDelegate(square); //  IL2CPP may strip this method
    }

    // This generic method may be stripped unless T is clearly referenced
    void UseDelegate<T>(Func<T, T> func)
    {
        T result = func.Invoke(default);
        Debug.Log($"[GenericDelegate] Result: {result}");
    }

    //Example 2: Reflection + Preserve Reflection — method might not be found if code stripping removed it
    void RunReflectionExample()
    {
        var type = typeof(MyPreservedClass);
        var method = type.GetMethod("Hello", BindingFlags.Static | BindingFlags.Public);
        method?.Invoke(null, null);
    }

    //Example 3: This ensures MyRuntimeReferencedClass.Hello() is not stripped
    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {
        var t = typeof(MyRuntimeReferencedClass);
    }
}

[Preserve]
public class MyPreservedClass
{
    [Preserve]
    public static void Hello()
    {
        Debug.Log("[Reflection] Hello from [Preserve] class!");
    }
}

public class MyRuntimeReferencedClass
{
    public static void Hello()
    {
        Debug.Log("[Reflection] Hello from RuntimeInitializeOnLoadMethod!");
    }
}

