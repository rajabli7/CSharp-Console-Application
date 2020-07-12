using PCPSolver.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PCPSolver.Classes
{
    public static class PCPSolver
    {
        public static void Process(Blocks blocks)
        {
            var posibleSequences = new List<string>() { "" };
            var depth = 1;

            while (depth <= blocks.Depth)
            {
                var newPosibleSequences = new List<string>();

                foreach (var posibleSequence in posibleSequences)
                {
                    string top = string.Empty, bottom = string.Empty;

                    ConcatenateCells(blocks, posibleSequence, ref top, ref bottom);

                    if (FindSolution(blocks, newPosibleSequences, posibleSequence, top, bottom, depth))
                    {
                        return;
                    }
                }
                
                posibleSequences = newPosibleSequences;
                depth++;
            }
            ConsoleHelper.WriteRedLine($"Reject. Posible variants { posibleSequences.Count }");
        }

        private static void ConcatenateCells(Blocks blocks, string posibleSequence, ref string top, ref string bottom)
        {
            var blocksIndeces = posibleSequence.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var blockIndex in blocksIndeces)
            {
                top += blocks.TopCells[Convert.ToInt32(blockIndex)];
                bottom += blocks.BottomCells[Convert.ToInt32(blockIndex)];
            }
        }

        private static bool FindSolution(Blocks blocks, List<string> posibleSequences, string posibleSequence, string top, string bottom,
            int depth)
        {
            for (int i = 0; i < blocks.TopCells.Count; i++)
            {
                var newTop = top + blocks.TopCells[i];
                var newBottom = bottom + blocks.BottomCells[i];

                var result = CompareStrings(newTop, newBottom);

                if (result == ComparisonResult.Substring)
                {
                    posibleSequences.Add($"{ posibleSequence }{ i } ");
                }
                else if (result == ComparisonResult.Equal)
                {
                    ConsoleHelper.WriteGreenLine($"Accept. \nIndeces: { posibleSequences.Last() }. \nString: { newTop }. \nDepth: { depth }.");

                    return true;
                }
            }

            return false;
        }

        private static ComparisonResult CompareStrings(string strA, string strB)
        {
            if (strA == strB)
            {
                return ComparisonResult.Equal;
            }

            if (strA.Length < strB.Length)
            {
                if (strB.Substring(0, strA.Length) == strA)
                {
                    return ComparisonResult.Substring;
                }
            }
            else
            {
                if (strA.Substring(0, strB.Length) == strB)
                {
                    return ComparisonResult.Substring;
                }
            }

            return ComparisonResult.Unequal;
        }
    }
}
