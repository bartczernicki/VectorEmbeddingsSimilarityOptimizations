﻿using BenchmarkDotNet.Running;
using System.Runtime.Intrinsics;


namespace VectorEmbeddingsSimilarityOptimizations
{
    public class Program
    {
        static void Main(string[] args)
        {
            // How BenchmarkDotNet Looks at Projects to find your benchmark DLLs
            // https://stackoverflow.com/questions/67766289/benchmarkdotnet-unable-to-find-tests-when-it-faces-weird-solution-structure  

            Console.WriteLine("Vector Performance Benchmark");

            #if NET8_0_OR_GREATER
                Console.WriteLine("AVX-128: " + Vector128.IsHardwareAccelerated.ToString());
                Console.WriteLine("AVX-256: " + Vector256.IsHardwareAccelerated.ToString());
                Console.WriteLine("AVX-512: " + Vector512.IsHardwareAccelerated.ToString());
#endif

            // Benchmark - Vector calculations using Multi-Threading
            Console.WriteLine(string.Format("Using {0} CPU cores for multithreaded benchmark.", Util.BenchmarkConfig.ProcessorsAvailableAt75Percent));
            var summary = BenchmarkRunner.Run<Jobs.VectorMultithread.Benchmark>();

            // Benchmark - Vector Calculation comparison of Cosine Similarity vs Dot Product
            // var summary = BenchmarkRunner.Run<Jobs.VectorCalculation.Benchmark>();

            // Benchmark - Vector Dimensions comparison of 768 vs 1536 Dimensions
            //var summary = BenchmarkRunner.Run<Jobs.VectorDimensions.Benchmark>();

            // Print Benchmark results
            Console.WriteLine(summary.ToString());
        }

        //[Params(1000, 100)] //<-- Change this to determine the amount of vectors to "mimic" a Vector database  (very small, large)
        //// 1mil embeddings is roughly 700,000-1mil document pages with a decent amount of text present
        //public int NumberOfVectorsToCreate { get; set; }

        //[Params(false, true)] //<-- Change this to determine if to run on a single thread or leverage 75% of available threads 
        //public bool MultiThreaded { get; set; }

        //[Benchmark]
        //public void CosineSimilarityVectors768Dimensions()
        //{
        //    var results = Util.Vectors.TopMatchingVectors(vectors?.VectorToCompareTo768Dimensions, vectors?.TestVectors768Dimensions, true, MultiThreaded);
        //}

        //[Benchmark]
        //public void DotProductVectors768Dimensions()
        //{
        //    var results = Util.Vectors.TopMatchingVectors(vectors?.VectorToCompareTo768Dimensions, vectors?.TestVectors768Dimensions, false, MultiThreaded);
        //}

        //[Benchmark]
        //public void CosineSimilarityVectors1536Dimensions()
        //{
        //    var results = Util.Vectors.TopMatchingVectors(vectors?.VectorToCompareTo1536Dimensions, vectors?.TestVectors1536Dimensions, true, MultiThreaded);
        //}

        //[Benchmark]
        //public void DotProductVectors1536Dimensions()
        //{
        //    var results = Util.Vectors.TopMatchingVectors(vectors?.VectorToCompareTo1536Dimensions, vectors?.TestVectors1536Dimensions, false, MultiThreaded);
        //}
    }
}
