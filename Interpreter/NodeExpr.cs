using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
     /**
     * 
     * Represents the Term Node in our parse tree for this interpreter.
     * A term can be a negfact multiplied or divided by another term, or
     * just a negfact. See these classes and the Grammar file for more
     * information on the structure of this program and the various node
     * types.
     * 
     * @author Jason Allen
     *
     */
    public class NodeExpr : Node
    {

        private NodeTerm term;
        private NodeAddop addop;
        private NodeExpr expr;

        /**
         * Constructs a NodeExpr given a term, a + or - operation node, and
         * another NodeExpr. See each of these node classes and the Grammar
         * for more detail.
         */
        public NodeExpr(NodeTerm term, NodeAddop addop, NodeExpr expr)
        {
            this.term = term;
            this.addop = addop;
            this.expr = expr;
        }

        /**
         * If the current NodeExpr held in this class is null, then
         * we can simply append another NodeExpr by instantiating
         * it to be equal to the new NodeExpr. Otherwise, we can
         * build a more complicated, recursive NodeExpr by appending
         * it to the non-null NodeExpr. This allows more complexity
         * in our language.
         * 
         * @return void
         */
        public void Append(NodeExpr expr)
        {
            if (this.expr == null)
            {
                this.addop = expr.addop;
                this.expr = expr;
                expr.addop = null;
            }
            else
            {
                this.expr.Append(expr);
            }
        }

        /**
         * Evaluates the NodeExpr using the appropriate + or - addop,
         * the term, and the next, internal, layered expression.
         * 
         * @return void
         */
        public override double Eval(EvalEnvironment env)
        {
            if (expr == null)
            {
                return term.Eval(env);
            }
            else
            {
                double exprEval = expr.Eval(env);
                double termEval = term.Eval(env);
                return addop.Op(exprEval, termEval);
            }
        }

    }
}
