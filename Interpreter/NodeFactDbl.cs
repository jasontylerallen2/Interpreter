using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
     /**
     * 
     * This class extends the abstract NodeFact class to support Doubles.
     * See the Grammar file for more information on the structure of this
     * program and the various node types.
     * 
     * @author Jason Allen
     * 
     */
    public class NodeFactDbl : NodeFact
    {

        private String num;

        /**
         * Constructs a double given a num in String form.
         */
        public NodeFactDbl(String num)
        {
            this.num = num;
        }

        /**
         * Parses the String to give us a double-precision value
         * for mathematical operations.
         * 
         * @return a double from the String used to create
         * the node
         */
        public override double eval(EvalEnvironment env)
        {
            return Double.Parse(num);
        }

    }

}
