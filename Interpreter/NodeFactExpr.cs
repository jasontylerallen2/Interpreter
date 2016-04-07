using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
     /**
     * 
     * This class represents an expression that is contained
     * within parentheses according to the grammar and can
     * therefore be treated as a single fact, as it is
     * evaluated first given the typical order of operations
     * with parentheses.
     * 
     * @author Jason Allen
     * 
     */
    public class NodeFactExpr : NodeFact
    {

        private NodeExpr expr;

        /**
         * Constructs a NodeFactExpr given the contained NodeExpr
         */
        public NodeFactExpr(NodeExpr expr)
        {
            this.expr = expr;
        }

        /**
         * @return a double value for evaluating the expr, done
         * using the NodeExpr classes eval method already
         * established
         */
        public override double Eval(EvalEnvironment env)
        {
            return expr.Eval(env);
        }

    }

}
