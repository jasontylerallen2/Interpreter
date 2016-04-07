using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
     /**
     * 
     * Represents a node for the relative operators in the parse tree for this interpreter.
     * This is ultimately used by the BoolExpr node to evaluate the relative value
     * of two expressions.
     * 
     * @author Jason Allen
     *
     */
    public class NodeRelOp : Node
    {

        private String relOp;

        /**
         * Construct a NodeRelOp given the current position we've scanned to and
         * which kind of operation.
         */
        public NodeRelOp(int pos, String relOp)
        {
            this.pos = pos;
            this.relOp = relOp;
        }

        /**
         * Evaluate the six kinds of relative operators supported (see the Grammar)
         * for double values (obtained from two expressions) and return 0.0 if false
         * and 1.0 if true.
         * 
         * @return 0.0 if false and 1.0 if true
         */
        public double op(double o1, double o2)
        {
            Boolean ret;
            if (relOp.Equals("<"))
                ret = o1 < o2;
            else if (relOp.Equals("<="))
                ret = o1 <= o2;
            else if (relOp.Equals(">"))
                ret = o1 > o2;
            else if (relOp.Equals(">="))
                ret = o1 >= o2;
            else if (relOp.Equals("<>"))
                ret = o1 != o2;
            else if (relOp.Equals("=="))
                ret = o1 == o2;
            else
                throw new Exception("At position: " + pos + ", bogus relop: " + relOp);

            if(ret)
                return 1.0;
            else
                return 0.0;
        }

    }

}
