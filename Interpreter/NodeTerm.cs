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
    public class NodeTerm : Node
    {
     
        private NodeNegFact negfact;
        private NodeMulop mulop;
        private NodeTerm term;

        /**
         * Constructs a NodeTerm given a negfact, a * or / operation as indicated
         * by a NodeMulop, and another term. Use null for the second two parameters
         * if it is just a negfact.
         */
        public NodeTerm(NodeNegFact negfact, NodeMulop mulop, NodeTerm term)
        {
            this.negfact = negfact;
            this.mulop = mulop;
            this.term = term;
        }

        /**
         * If the current NodeTerm held in this class is null, then
         * we can simply append another NodeTerm by instantiating
         * it to be equal to the new NodeTerm. Otherwise, we can
         * build a more complicated, recursive NodeTerm by appending
         * it to the non-null NodeTerm. This allows more complexity
         * in our language.
         *
         * @return void
         */
        public void Append(NodeTerm term)
        {
            if (this.term == null)
            {
                this.mulop=term.mulop;
                this.term=term;
                term.mulop=null;
            }
            else
                this.term.Append(term);
        }

        /**
         * @return a double with the value of the NodeTerm; either just
         * the eval() of a negfact, or if there is a term and a corresponding
         * mulop, the eval of the term * or / the eval of the negfact
         */
        public override double Eval(EvalEnvironment env)
        {
            return term==null
                ? negfact.Eval(env)
                : mulop.Op(term.Eval(env), negfact.Eval(env));
        }

    }

}
