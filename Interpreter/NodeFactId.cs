using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
     /**
     *
     * This class extends the abstract NodeFact class to support Ids.
     * See the Grammar file for more information on the structure of this
     * program and the various node types.
     *
     * @author Jason Allen
     *
     */
    public class NodeFactId : NodeFact
    {

        private String id;

        /**
         * Constructs a NodeFactId given a position and the name
         * of String representing the actual identifier.
         */
        public NodeFactId(int pos, String id) 
        {
            this.pos = pos;
            this.id = id;
        }

        /**
         * @returns a double value from evaluating the fact, done
         * simply using the environment's get() method
         */
        public override double Eval(EvalEnvironment env)
        {
            return env.Get(pos, id);
        }

    }

}
