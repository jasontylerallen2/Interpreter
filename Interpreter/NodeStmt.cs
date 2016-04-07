using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{

    /**
     *
     * Represents the stmt node in the parse tree for this interpreter.
     * A statement can assign a value to a variable, read in a variable
     * from the user, write a variable to the screen, perform if-then
     * and if-then-else statements, while-do loops, and basic begin-end
     * blocks.
     *
     * @author Jason Allen
     *
     */
    public class NodeStmt : Node
    {

        private NodeAssn assn;
        private Token keyword;
        private Token id;
        private NodeExpr expr;
        private NodeBoolExpr boolExpr;
        private NodeStmt stmt1;
        private NodeStmt stmt2;
        private NodeBlock block;
    

        /**
         * Constructor that handles every possible case for a stmt as explained above. Additionally,
         * see the grammar.
         */
        public NodeStmt(NodeAssn assn,
                        Token keyword,
                        Token id,
                        NodeExpr expr, NodeBoolExpr boolExpr, NodeStmt stmt1, NodeStmt stmt2,
                        NodeBlock block,
                        int pos)
        {

            this.assn = assn;
            this.keyword = keyword;
            this.id = id;
            this.expr = expr;
            this.boolExpr = boolExpr;
            this.stmt1 = stmt1;
            this.stmt2 = stmt2;
            this.block = block;
            this.pos = pos;
        }

        /**
         * @return the value of the statement. A keyword of 'wr' means print an expression,
         * if-then-else statements mimic the C# execution, but based on BoolExpr
         * evals() that return 1.0 for true and 0.0 for false, the while loop simply
         * executes a statement while a BoolExpr node evaluates to 1.0, and begin/end
         * lumps statements together into a more standard programming "block" - i.e. statements
         * combined between curly braces in C based languages.
         */
        public override double eval(EvalEnvironment env)
        {
            if ( this.keyword != null )
            {
                if ( this.keyword.tok().Equals("wr") )
                {
                    double d = this.expr.eval(env);
                    //Console.WriteLine( d );
                    env.addOutput(d);
                    return d;
                }
                else if ( this.keyword.tok().Equals("if") )
                {
                    /* So, this is only used if it's the last value in a program. And, if Joe ends his program with
                     * an if statement, what do we really want to return? It's confusing. Don't do that. */
                    if ( boolExpr.eval(env) == 1.0 )
                    {
                        stmt1.eval(env);
                    }
                    else
                    {
                        if ( stmt2 != null )
                        {
                            stmt2.eval(env);
                        }
                    }
                    return 0.0;
                }
                else if ( this.keyword.tok().Equals("while") )
                {
                    /* Again, don't end a program with a 'while' and expect a return value. */
                    while ( boolExpr.eval(env) == 1.0 )
                    {
                        stmt1.eval(env);
                    }
                    return 0.0;
                }
                else if ( this.keyword.tok().Equals("begin") )
                {
                    double ret = this.block.getStmt().eval( env );
                    if (this.block.getNextBlock() != null)
                        ret = this.block.getNextBlock().eval(env);
                    return ret;
                }
                else
                {
                    throw new Exception("At position: " + this.pos + " What's with the keyword? (" + this.keyword.tok() + ")");
                }
            }
            else
            {
                return assn.eval(env);
            }
        }

    }


}
