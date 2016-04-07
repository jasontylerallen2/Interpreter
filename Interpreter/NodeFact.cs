using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
     /**
     * According to our Grammar, a fact can be an id (identifier),
     * a dbl (double), or an entire expression in parentheses.
     * This abstract parent class will be extended to support
     * each case.
     * 
     * @author Jason Allen
     * 
     */
    public abstract class NodeFact : Node {};

}
