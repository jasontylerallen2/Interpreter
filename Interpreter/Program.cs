using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{

    /**
     * Main Class
     * 
     * @author Jason Allen
     */
    class Program
    {
        /**
        * Main. Runs a test suite to ensure the correctness of the interpreter with various
        * simple programs. To add another test, simply add a folder into the "test" folder
        * that contains a file named "prg" that indicates the program and a file named "exp"
        * that indicates the expected result. In the exp file, one double should go each
        * line alone.
        */
        static void Main(String[] args)
        {
            Parser parser = new Parser();
           
            String testPath = "test";
            if (!Directory.Exists(testPath))
            {
                throw new Exception("WHERE ARE MY TESTS!?!");
            }

            String[] subDirectories = Directory.GetDirectories(testPath);
            foreach(String subDirectory in subDirectories)
            {
                String[] fileEntries = Directory.GetFiles(subDirectory);
                String prgFile = "";
                String expFile = "";

                foreach (String filePath in fileEntries)
                {
                    String file = Path.GetFileName(filePath);

                    if (file == "prg")
                    {
                        prgFile = filePath;
                    }
                    else if (file == "exp")
                    {
                        expFile = filePath;
                    }
                }

                if (prgFile.Length == 0 || expFile.Length == 0)
                {
                    throw new Exception("In Directory " + subDirectory + ". WHERE ARE MY prg/exp FILES!?!");
                }

                String prg = File.ReadAllText(prgFile);
                String exp = File.ReadAllText(expFile);

                Console.WriteLine("==================================================");
                Console.WriteLine("TESTING DIRECTORY " + subDirectory);

                EvalEnvironment env = new EvalEnvironment();
                Node n = parser.parse(prg.Trim());
                n.eval(env);
                List<Double> output = env.getOutput();
                String outputString = "";

                foreach(Double d in output)
                {
                    outputString += d + "\n";
                }

                if (outputString.Trim() != exp.Trim())
                {
                    Console.WriteLine("FAIL! Output doesn't match expected.");
                    Console.WriteLine("Output:");
                    Console.WriteLine(outputString);
                    Console.WriteLine("--------------------:");
                    Console.WriteLine("Expected:");
                    Console.WriteLine(exp);
                }
                else
                {
                    Console.WriteLine("SUCCESS!");
                    Console.WriteLine("Output:");
                    Console.WriteLine(outputString);
                }

            }

            Console.ReadLine();
        }

    }
}
