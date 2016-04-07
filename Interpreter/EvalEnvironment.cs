using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Interpreter
{

    /**
     * 
     * A class with methods used to assign and evaluate variables in an interpreter.
     * 
     * @author Jason Allen
     *
     */
    public class EvalEnvironment
    {

        private Dictionary<String, double> table = new Dictionary<String, double>();
        private List<Double> output = new List<Double>();

        /**
         * Add a double to the output of this program
         * 
         * @return void
         */
        public void addOutput(Double d)
        {
            output.Add(d);
        }

        /**
         * Get the output of the program
         * 
         * @return the output of the program
         */
        public List<Double> getOutput()
        {
            return output;
        }

	    /**
	     * Place a variable and a value in the table for storing
	     * 
	     * @return the value of the variable
	     */
        public double put(String var, double val)
        {
            if (table.ContainsKey(var))
            {
                table[var] = val;
            }
            else
            {
                table.Add(var, val);
            }
            return val;
        }
    
	    /**
	     * @return a value given a position and variable name from the dictionary
	     */
        public double get(int pos, String var)
        {
            return table[var]; 
        }

    }

}
