using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis
{
    public sealed class Const
    {

        // This will get the current WORKING directory (i.e. \bin\Debug)
        public static readonly string workingDirectory = Environment.CurrentDirectory;
        // or: Directory.GetCurrentDirectory() gives the same result
        // This will get the current PROJECT directory
        public static readonly string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
        //SOLUTION DIRECTORY
        public static readonly string solutionDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

        public static readonly string imagesFolderDirectory = Path.Combine(projectDirectory, @"extract_features\", @"img\");


        public static readonly string afterKeyExtractionFileNameEnding = ".haraff.sift";
        
        
        //RUN PARAMETERS:
        public static readonly string imgName01 = "tab1";
        public static readonly string imgName02 = "tab2";

        public static readonly int neighborsToCheck = 5;
        public static readonly int neighborsCondition = 4;

        public static readonly int maxError = 20;
        public static readonly int iterationNumber = 15000;

        


        


    }
}
