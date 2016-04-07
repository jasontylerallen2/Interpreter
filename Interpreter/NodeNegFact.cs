using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
     /**
     * 
     * Allows an entire node to be evaluated as negative IF constructed
     * with a boolean indicating to do so. Otherwise, it will simply
     * act as a regular NodeFact (id, expr, or dbl). This is essentially
     * just a wrapper around the NodeFact classes to allow a prefix
     * unary minus operator. See the grammar for more detail.
     * 
     * @author Jason Allen
     * 
     */
    public class NodeNegFact : Node
    {
        private NodeFact nodefact;
        private Boolean isNegative;

        /**
         * Constructs a NodeNegFact given a boolean indicating
         * whether it should be evaluated as negative or not
         * and a regular NodeFact.
         */
        public NodeNegFact(Boolean isNegative, NodeFact nodefact)
        {
            this.isNegative = isNegative;
            this.nodefact = nodefact;
        }

        /**
         * @return a negative evaluation of the NodeFact or a normal
         * evaluation of the NodeFact using the NodeFacts eval() method;
         * depending on the value of isNegative
         */
        public override double eval(EvalEnvironment env)
        {
            return this.isNegative ? -nodefact.eval(env) : nodefact.eval(env) ;
        }
    }


}
