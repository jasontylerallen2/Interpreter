using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
     /**
     * 
     * Represents a * or / node in the parse tree for this interpreter.
     * This node is ultimately used by the NodeTerm class - See the
     * Grammar file for more information on the structure of this
     * program and the various node types.
     * 
     * @author Jason Allen
     *
     */
    public class NodeMulop : Node 
    {

        private String mulop;

        /**
         * Constructs a mulop given a position and the String representing
         * the operation ( * or / )
         */
        public NodeMulop(int pos, String mulop) 
        {
            this.pos = pos;
            this.mulop = mulop;
        }

        /**
         * Performs the math and returns a double for multiplication OR division
         * of two doubles, depending on the value of mulop
         * 
         * @return a double from multiplying or dividing two doubles
         */
        public double Op(double o1, double o2) 
        {
            if (mulop.Equals("*"))
                return o1 * o2;
            if (mulop.Equals("/"))
                return o1 / o2;
            throw new Exception("At position: " + pos + ", bogus mulop: " + mulop);
        }

    }

}
