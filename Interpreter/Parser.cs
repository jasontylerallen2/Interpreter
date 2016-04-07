using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{

     /**
     * Given tokens split up from the Scanner, this class contains the
     * appropriate methods to construct each kind of node in the
     * parse tree. This requires matching some characters to ensure
     * the given statements follow the syntax specified in our
     * Grammar (semi-colon at the end a of stmt, '=' to assign
     * an expression to an id, etc).
     *
     * See the node type classes to better understand how each is
     * being parsed, and view the Grammar for a better idea of
     * the rules surrounding the language.
     *
     * @author Jason Allen
     *
     */
    public class Parser
    {

        private Scanner scanner;

        /**
         * Calls the scanners match() method, which indicates if there
         * is a syntax error due to unexpected characters.
         *
         * @return void
         */
        private void Match(String s)
        {
            scanner.Match(new Token(s));
        }

        /**
         * Calls the scanners curr() method, which indicates
         * the scanner's current token.
         *
         * @return the current Token
         */
        private Token Curr()
        {
            return scanner.Curr();
        }

        /**
         * Calls the scanners pos() method, which indicates the current
         * position
         *
         * @return the current position in the scanner
         */
        private int Pos()
        {
            return scanner.Pos();
        }

        /**
         * Parses a multiplication operation and creates/returns
         * the appropriate NodeMulop.
         *
         * @return the parsed NodeMulop
         */
        private NodeMulop ParseMulop()
        {
            if (Curr().Equals(new Token("*")))
            {
                Match("*");
                return new NodeMulop(Pos(), "*");
            }
            if (Curr().Equals(new Token("/")))
            {
                Match("/");
                return new NodeMulop(Pos(), "/");
            }
            return null;
        }

        /**
         * Parses an addition operation and creates/returns
         * the appropriate NodeAddop
         *
         * @return the parsed NodeAddop
         */
        private NodeAddop ParseAddop()
        {
            if (Curr().Equals(new Token("+")))
            {
                Match("+");
                return new NodeAddop(Pos(), "+");
            }
            if (Curr().Equals(new Token("-")))
            {
                Match("-");
                return new NodeAddop(Pos(), "-");
            }
            return null;
        }

        /**
         * Parses a fact and creates/returns the appropriate NodeFact.
         * This can be an id, an expr in parantheses, or a double.
         *
         * @return the parsed NodeFact
         */
        private NodeFact ParseFact()
        {
            if (Curr().Equals(new Token("(")))
            {
                Match("(");
                NodeExpr expr = ParseExpr();
                Match(")");
                return new NodeFactExpr(expr);
            }
            if (Curr().Equals(new Token("id")))
            {
                Token id = Curr();
                Match("id");
                return new NodeFactId(Pos(), id.Lex());
            }
            Token dbl = Curr();
            Match("dbl");
            return new NodeFactDbl(dbl.Lex());
        }

        /**
         * Parses a NegFact and returns the corresponding NegFactNode
         * with an isNegative boolean indicating whether the fact
         * is made negative with a prefix unary minus operator
         * or not.
         *
         * @return the parsed NodeNegFact
         */
        private NodeNegFact ParseNegFact()
        {
            Boolean isNegative = false;
            if (Curr().Equals(new Token("-")))
            {
                Match("-");
                isNegative = true;
            }
            NodeFact fact = ParseFact();
            NodeNegFact negfact = new NodeNegFact(isNegative, fact);
            return negfact;
        }

        /**
         * Parses a Term and creates/returns the appropriate NodeTerm.
         * This can be a negfact multiplied or divided by another term, or
         * just a negfact.
         *
         * @return the parsed NodeTerm
         */
        private NodeTerm ParseTerm()
        {
            NodeNegFact negfact = ParseNegFact();
            NodeMulop mulop = ParseMulop();
            if (mulop == null)
                return new NodeTerm(negfact, null, null);
            NodeTerm term = ParseTerm();
            term.Append(new NodeTerm(negfact, mulop, null));
            return term;
        }

        /**
         * Parses an Expr and creates/returns the appropriate NodeExpr.
         * This can be a term * or / another expr, or just a term.
         *
         * @return the parsed NodeExpr
         */
        private NodeExpr ParseExpr()
        {
            NodeTerm term = ParseTerm();
            NodeAddop addop = ParseAddop();
            if (addop == null)
                return new NodeExpr(term, null, null);
            NodeExpr expr = ParseExpr();
            expr.Append(new NodeExpr(term, addop, null));
            return expr;
        }

        /**
         * Parses an Assignment and creates/returns the appropriate NodeAssn.
         * This requires an identifier followed be an equals sign followed by
         * an expression.
         *
         * @return the parsed NodeAssn
         */
        private NodeAssn ParseAssn()
        {
            Token id = Curr();
            Match("id");
            Match("=");
            NodeExpr expr = ParseExpr();
            NodeAssn assn = new NodeAssn(id.Lex(), expr);
            return assn;
        }

        /**
         * Parses an relative operator and creates/returns the appropriate NodeRelOp.
         * 
         * @return the parsed NodeRelOp
         */
        private NodeRelOp ParseRelOp()
        {
            if (Curr().Equals(new Token("<")))
            {
                Match("<");
                return new NodeRelOp(Pos(), "<");
            }
            else if (Curr().Equals(new Token("<=")))
            {
                Match("<=");
                return new NodeRelOp(Pos(), "<=");
            }
            else if (Curr().Equals(new Token(">")))
            {
                Match(">");
                return new NodeRelOp(Pos(), ">");
            }
            else if (Curr().Equals(new Token(">=")))
            {
                Match(">=");
                return new NodeRelOp(Pos(), ">=");
            }
            else if (Curr().Equals(new Token("<>")))
            {
                Match("<>");
                return new NodeRelOp(Pos(), "<>");
            }
            else if (Curr().Equals(new Token("==")))
            {
                Match("==");
                return new NodeRelOp(Pos(), "==");
            }
            return null;
        }

        /**
         * Parses a boolean expression and creates/returns the appropriate NodeBoolExpr.
         *
         * @return the parsed NodeBoolExpr
         */
        private NodeBoolExpr ParseBoolExpr()
        {
            NodeExpr expr1 = ParseExpr();
            NodeRelOp relop = ParseRelOp();
            NodeExpr expr2 = ParseExpr();
            return new NodeBoolExpr(expr1, relop, expr2);
        }

        /**
         * Parses a statement and creates/returns the appropriate NodeStmt.
         * View the NodeStmt class for more information on the cases
         * marked in this method.
         *
         * @return the parsed NodeStmt
         */
        private NodeStmt ParseStmt()
        {
            if (Curr().Equals(new Token("wr")))
            {
                /* Case 2 */
                Token keyword = Curr();
                Match("wr");
                NodeExpr expr = ParseExpr();
                return new NodeStmt(null, keyword, null,
                                    expr, null, null, null, null, Pos());
            }
            else if (Curr().Equals(new Token("if")))
            {
                /* Case 3-4 */
                Token keyword = Curr();
                Match("if");
                NodeBoolExpr boolExpr = ParseBoolExpr();
                Match("then");
                NodeStmt stmt1 = ParseStmt();
                if (Curr().Equals(new Token("else")))
                {
                    /* Case 4 */
                    Match("else");
                    NodeStmt stmt2 = ParseStmt();
                    return new NodeStmt(null, keyword, null, null,
                                        boolExpr, stmt1, stmt2, null, Pos());
                }
                else
                {
                    /* Case 5 */
                    return new NodeStmt(null, keyword, null, null,
                                        boolExpr, stmt1, null, null, Pos());
                }
            }
            else if (Curr().Equals(new Token("while")))
            {
                /* Case 5 */
                Token keyword = Curr();
                Match("while");
                NodeBoolExpr boolExpr = ParseBoolExpr();
                Match("do");
                NodeStmt stmt1 = ParseStmt();
                return new NodeStmt(null, keyword, null, null,
                                    boolExpr, stmt1, null, null, Pos());
            }
            else if (Curr().Equals(new Token("begin")))
            {
                /* Case 6 */
                Token keyword = Curr();
                Match("begin");
                NodeBlock block = ParseBlock();
                Match("end");
                return new NodeStmt(null, keyword, null, null,
                                    null, null, null,
                                    block, Pos());
            }
            else
            {
                /* Case 1 */
                NodeAssn assn = ParseAssn();
                NodeStmt stmt = new NodeStmt(assn, null, null, null,
                                             null, null, null, null, Pos());
                return stmt;
            }
        }

        /**
         * Parses a block and creates/returns the appropriate NodeBlock
         *
         * @return the parsed NodeBlock
         */
        private NodeBlock ParseBlock()
        {
            NodeStmt stmt = ParseStmt();
            NodeBlock block = new NodeBlock(stmt, null);
            if (Curr().Equals(new Token(";")))
            {
                Match(";");
                NodeBlock nextBlock = ParseBlock();
                block.Append(nextBlock);
                return block;
            }
            return block;
        }

        /**
         * Parses a program and creates/returns the appropriate NodeProg
         *
         * @return the parsed NodeProg
         */
        private NodeProg ParseProg()
        {
            NodeBlock block = ParseBlock();
            return new NodeProg(block);
        }

        /**
         * Creates and advances the scanner used in this parser,
         * and begins by parsing the statement; the first thing
         * in our Grammar.
         *
         * @return the first node in our parse tree
         */
        public Node Parse(String program)
        {
            scanner = new Scanner(program);
            scanner.Next();
            return ParseProg();
        }
    }

}
