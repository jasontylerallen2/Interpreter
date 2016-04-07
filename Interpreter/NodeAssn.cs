using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
     /**
     * 
     * Represents the assignment node in the tree for this interpreter.
     * This node is ultimately responsible for handling the assignment
     * of an id to a value. See the Grammar file for more information
     * on the structure of this program and the various node types.
     * 
     * @author Jason Allen
     *
     */
    public class NodeAssn : Node
    {
        private String id;
        private NodeExpr expr;

        /**
         * Constructs an assignment node given the id that is being assigned
         * a value and the expression.
         */
        public NodeAssn(String id, NodeExpr expr)
        {
            this.id = id;
            this.expr = expr;
        }

        /**
         * Evaluates an assignment Node; this essentially just uses the NodeExpr
         * eval() method.
         * 
         * @return a double resulting from the expression in the assignment
         * statement
         */
        public override double Eval(EvalEnvironment env)
        {
            return env.Put(id, expr.Eval(env));
        }

    }

}
