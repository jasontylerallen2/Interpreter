using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    /**
     * 
     * Represents a + or - node in the parse tree for this interpreter.
     * This node is ultimately called by the NodeExpr class - See the
     * Grammar file for more information on the structure of this
     * program and the various node types.
     * 
     * @author Jason Allen
     *
     */
    public class NodeAddop : Node
    {

        private String addop;

        /**
         * Constructs a node for add operations given a position and a String indicating
         * which operation.
         */
        public NodeAddop(int pos, String addop)
        {
            this.pos = pos;
            this.addop = addop;
        }

        /**
         * Performs the math and returns a double for addition OR subtraction
         * of two doubles, depending on the value of addop
         * 
         * @return a double adding or subtracting two doubles
         */
        public double Op(double o1, double o2)
        {
            if (addop.Equals("+"))
            {
                return o1 + o2;
            }
            if (addop.Equals("-"))
            {
                return o1 - o2;
            }
            throw new Exception("At pos: " + pos + ", bogus addop: " + addop);
        }

    }

}
