using Microsoft.Extensions.Options;
using Mutants.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mutants.Services
{
    public class DnaSequenceEvaluator : IDnaSequenceEvaluator
    {
        private readonly DnaEvaluationSettings _settings;

        public DnaSequenceEvaluator(IOptions<DnaEvaluationSettings> settingsWrapper)
        {
            _settings = settingsWrapper.Value;
        }

        public bool IsMutantDna(string[] dna)
        {
            if (dna.Any(sequence => sequence.Length != dna.Length))
                throw new IndexOutOfRangeException("The length of each sequence must match the number of sequences.");
            var dnaLength = dna.Length;
            if (dnaLength < _settings.MinimumNumberOfMutantSequencesRequired)
                return false;
            
            var evaluatedBases = new EvaluatedDnaBase[dnaLength, dnaLength];
            var numberOfMutantsSequencesFound = 0;
            for (int rowIndex = 0; rowIndex < dnaLength; rowIndex++)
            {
	            var bases = dna[rowIndex].ToCharArray();
	            for (int colIndex = 0; colIndex < bases.Length; colIndex++) 
                {
                    var currentEvaluatedBase = new EvaluatedDnaBase(bases[colIndex]);                    
		            evaluatedBases[rowIndex, colIndex] = currentEvaluatedBase;
                    if (rowIndex == 0 && colIndex == 0)			
			            continue;

                    if (GetEvaluationMethods().Any(
                        evaluate => 
                            evaluate(
                                evaluatedBases, 
                                currentEvaluatedBase, 
                                rowIndex,
                                colIndex,
                                dnaLength,
                                ref numberOfMutantsSequencesFound)))
                        return true;
                }
            }

            return false;
        }

        private delegate bool EvaluateBaseMethod(
            EvaluatedDnaBase[,] evaluatedDnaBases, 
            EvaluatedDnaBase currentEvaluatedBase, 
            int rowIndex,
            int colIndex,
            int dnaLength,
            ref int numberOfMutantsSequencesFound);

        private IEnumerable<EvaluateBaseMethod> GetEvaluationMethods()
        {
            yield return EvaluateBaseHorizontally;
            yield return EvaluateBaseVertically;
            yield return EvaluateBaseThroughLeftDiagonal;
            yield return EvaluateBaseThroughRightDiagonal;
        }

        private bool EvaluateBaseHorizontally(
            EvaluatedDnaBase[,] evaluatedDnaBases, 
            EvaluatedDnaBase currentEvaluatedBase, 
            int rowIndex,
            int colIndex,
            int dnaLength,
            ref int numberOfMutantsSequencesFound)
        {
            return colIndex > 0 
                && EvaluateBase(
                    evaluatedDnaBases,
                    currentEvaluatedBase,
                    rowIndex,
                    colIndex -1,
                    @base => @base.HorizontalCount,
                    (@base, count) => @base.HorizontalCount = count,
                    ref numberOfMutantsSequencesFound);
        }

        private bool EvaluateBaseVertically(
            EvaluatedDnaBase[,] evaluatedDnaBases, 
            EvaluatedDnaBase currentEvaluatedBase, 
            int rowIndex,
            int colIndex,
            int dnaLength,
            ref int numberOfMutantsSequencesFound)
        {            
            return rowIndex > 0
                && EvaluateBase(
                    evaluatedDnaBases,
                    currentEvaluatedBase,
                    rowIndex -1,
                    colIndex,
                    @base => @base.VerticalCount,
                    (@base, count) => @base.VerticalCount = count,
                    ref numberOfMutantsSequencesFound);
        }

        private bool EvaluateBaseThroughLeftDiagonal(
            EvaluatedDnaBase[,] evaluatedDnaBases, 
            EvaluatedDnaBase currentEvaluatedBase, 
            int rowIndex,
            int colIndex,
            int dnaLength,
            ref int numberOfMutantsSequencesFound)
        {            
            return rowIndex > 0 && colIndex > 0
                && EvaluateBase(
                    evaluatedDnaBases,
                    currentEvaluatedBase,
                    rowIndex -1,
                    colIndex -1,
                    @base => @base.LeftDiagonalCount,
                    (@base, count) => @base.LeftDiagonalCount = count,
                    ref numberOfMutantsSequencesFound);
        }

        private bool EvaluateBaseThroughRightDiagonal(
            EvaluatedDnaBase[,] evaluatedDnaBases, 
            EvaluatedDnaBase currentEvaluatedBase, 
            int rowIndex,
            int colIndex,
            int dnaLength,
            ref int numberOfMutantsSequencesFound)
        {            
            return rowIndex > 0 && (colIndex < dnaLength -1)
                && EvaluateBase(
                    evaluatedDnaBases,
                    currentEvaluatedBase,
                    rowIndex -1,
                    colIndex +1,
                    @base => @base.RightDiagonalCount,
                    (@base, count) => @base.RightDiagonalCount = count,
                    ref numberOfMutantsSequencesFound);
        }

        private bool EvaluateBase(
            EvaluatedDnaBase[,] evaluatedDnaBases, 
            EvaluatedDnaBase currentEvaluatedBase, 
            int rowIndex,
            int colIndex,
            Func<EvaluatedDnaBase, int> getEvaluatedCount, 
            Action<EvaluatedDnaBase, int> setEvaluatedCount,
            ref int numberOfMutantsSequencesFound)
        {
            var previousEvaluatedBase = evaluatedDnaBases[rowIndex, colIndex];
            var previousEvaluatedCount = getEvaluatedCount(previousEvaluatedBase);
            if (previousEvaluatedCount < _settings.NumberOfDupeBasesRequired
                && previousEvaluatedBase.DnaBase == currentEvaluatedBase.DnaBase)
            {
                setEvaluatedCount(currentEvaluatedBase, previousEvaluatedCount + 1);
                if (getEvaluatedCount(currentEvaluatedBase) == _settings.NumberOfDupeBasesRequired)
                    numberOfMutantsSequencesFound++;
                if (numberOfMutantsSequencesFound == _settings.MinimumNumberOfMutantSequencesRequired)
                    return true;
            }

            return false;
        }

        private class EvaluatedDnaBase
        {
            public EvaluatedDnaBase(char dnaBase){
                DnaBase = dnaBase;
            }

            public char DnaBase { get; private set; }
            public int HorizontalCount { get; set; } = 1;
            public int VerticalCount { get; set; } = 1;
            public int RightDiagonalCount { get; set; } = 1;
            public int LeftDiagonalCount { get; set; } = 1;
        }
    }
}