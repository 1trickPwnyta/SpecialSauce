using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpecialSauce.Harmony
{
    public static class HarmonyUtility
    {
        public static IEnumerable<CodeInstruction> Transpile(this IEnumerable<CodeInstruction> instructions, Action<List<CodeInstruction>> transpiler)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            transpiler(instructionsList);
            return instructionsList;
        }
    }
}
