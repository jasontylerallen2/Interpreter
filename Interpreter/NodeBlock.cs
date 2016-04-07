using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
     /**
     * 
     * Represents the block node in the tree for this interpreter.
     * This node represents a typical "block" of code in a program.
     * See the grammar for more detail.
     * 
     * @author Jason Allen
     *
     */
    public class NodeBlock : Node
    {

        private NodeStmt stmt;
        private NodeBlock nextBlock;

        /**
         * Constructs a NodeBlock given a statement, and potentially
         * another block.
         */
        public NodeBlock(NodeStmt stmt, NodeBlock nextBlock)
        {
            this.stmt = stmt;
            this.nextBlock = nextBlock;
        }

        /**
         * If the current nextBlock held in this class is null, then
         * we can simply append another NodeBlock by instantiating
         * it to be equal to the new NodeBlock. Otherwise, we can
         * build a more complicated, recursive NodeBlock by appending
         * it to the non-null NodeBlock. This allows more complexity
         * in our language.
         * 
         * @return void
         */
        public void append(NodeBlock nextBlock)
        {
            if (this.nextBlock != null)
            {
                this.nextBlock.append(nextBlock);
            }
            else
            {
                this.nextBlock = nextBlock;
            }
        }

        /**
         * @return the stmt contained within this block
         */
        public NodeStmt getStmt()
        {
            return stmt;
        }

        /**
         * @return the NextBlock, to allow recursive block parsing
         */
        public NodeBlock getNextBlock()
        {
            return nextBlock;
        }

        /**
         * Evaluates a block; this essentially just uses the NodeExpr
         * eval() method of the block
         *
         * @return a double resulting from the expression in the assignment
         * statement
         */
        public override double eval(EvalEnvironment env)
        {
            double d = stmt.eval(env);
            if (nextBlock != null)
            {
                return nextBlock.eval(env);
            }
            return d;
        }

    }

}
