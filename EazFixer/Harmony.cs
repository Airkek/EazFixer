﻿using System;
using System.Diagnostics;
using System.Reflection;
using HarmonyLib;

namespace EazFixer
{
    internal static class Harmony
    {
        public static void Patch()
        {
            var h = new HarmonyLib.Harmony("holly.eazfixer");
            h.PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(StackFrame), "GetMethod")]
        public class PatchStackTraceGetMethod
        {
            public static MethodInfo MethodToReplace;

            public static void Postfix(ref MethodBase __result)
            {
                if (__result.DeclaringType == typeof(RuntimeMethodHandle))
                {
                    //just replace it with a method
                    __result = MethodToReplace ?? MethodBase.GetCurrentMethod();
                    Debug.WriteLine("[D] Patched stacktrace entry");
                }
            }
        }
    }
}
