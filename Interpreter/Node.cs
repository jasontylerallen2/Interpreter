using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
     /**
     * Represents a node in the parse tree for this interpreter. See the
     * Grammar file for more information on various nodes. This is
     * an abstract class, extended by all the types of nodes based
     * on the Grammar.
     * 
     * @author Jason Allen
     */
    public abstract class Node
    {

        protected int pos = 0;

        /**
         * Guarantees that all nodes have a method of evaluation
         * given an environment; in this case throws an exception
         * because there should be successful eval() methods
         * defined in other node classes and we should not get here.
         * 
         * @return an error indicating we should not be running this high-level
         * eval() method, but instead be using the eval() methods in inheriting
         * classes
         */
        public virtual double Eval(EvalEnvironment env)
        {
            throw new Exception ("At position: " + pos + ", cannot eval() node!");
        }

    }

}
