using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
     /**
     * 
     * Represents the overall node for the program, which contains a single overarching block
     * that may have multiple other blocks chained onto it to allow greater length and
     * complexity in the program
     * 
     * @author Jason Allen
     *
     */
    public class NodeProg : Node
    {

        private NodeBlock block;

        /**
         * Construct a NodeProg given the overall program block
         */
        public NodeProg(NodeBlock block)
        {
            this.block = block;
        }

        /**
         * Evaluates the entire program; essentially just by calling the overall
         * block (see the grammar) to evaluate itself.
         * 
         * @return a double resulting from evaluation of the overall block
         */
        public override double Eval(EvalEnvironment env)
        {
            double ret = block.Eval(env);
            return ret;
        }

    }

}
