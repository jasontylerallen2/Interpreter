using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
     /**
     * 
     * Represents a node with two boolean expressions that can be
     * evaluated with a relative operator; gives 1.0 for true
     * and 0.0 for false.
     * 
     * @author  Jason Allen
     *
     */
    public class NodeBoolExpr : Node
    {

        private NodeExpr  expr1;
        private NodeRelOp relOp;
        private NodeExpr  expr2;

        /**
         * Constructs a NodeBoolExpr given the two exprs to be compared and
         * the relative operation
         */
        public NodeBoolExpr(NodeExpr expr1, NodeRelOp relOp, NodeExpr expr2)
        {
            this.expr1 = expr1;
            this.relOp = relOp;
            this.expr2 = expr2;
        }

        /**
         * @return 1.0 if true and 0.0 if false
         */
        public override double Eval(EvalEnvironment env)
        {
            return relOp.Op(expr1.Eval(env), expr2.Eval(env));
        }

    }

}
