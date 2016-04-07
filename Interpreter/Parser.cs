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
        private void match(String s)
        {
            scanner.match(new Token(s));
        }

        /**
         * Calls the scanners curr() method, which indicates
         * the scanner's current token.
         *
         * @return the current Token
         */
        private Token curr()
        {
            return scanner.curr();
        }

        /**
         * Calls the scanners pos() method, which indicates the current
         * position
         *
         * @return the current position in the scanner
         */
        private int pos()
        {
            return scanner.pos();
        }

        /**
         * Parses a multiplication operation and creates/returns
         * the appropriate NodeMulop.
         *
         * @return the parsed NodeMulop
         */
        private NodeMulop parseMulop()
        {
            if (curr().equals(new Token("*")))
            {
                match("*");
                return new NodeMulop(pos(), "*");
            }
            if (curr().equals(new Token("/")))
            {
                match("/");
                return new NodeMulop(pos(), "/");
            }
            return null;
        }

        /**
         * Parses an addition operation and creates/returns
         * the appropriate NodeAddop
         *
         * @return the parsed NodeAddop
         */
        private NodeAddop parseAddop()
        {
            if (curr().equals(new Token("+")))
            {
                match("+");
                return new NodeAddop(pos(), "+");
            }
            if (curr().equals(new Token("-")))
            {
                match("-");
                return new NodeAddop(pos(), "-");
            }
            return null;
        }

        /**
         * Parses a fact and creates/returns the appropriate NodeFact.
         * This can be an id, an expr in parantheses, or a double.
         *
         * @return the parsed NodeFact
         */
        private NodeFact parseFact()
        {
            if (curr().equals(new Token("(")))
            {
                match("(");
                NodeExpr expr = parseExpr();
                match(")");
                return new NodeFactExpr(expr);
            }
            if (curr().equals(new Token("id")))
            {
                Token id = curr();
                match("id");
                return new NodeFactId(pos(), id.lex());
            }
            Token dbl = curr();
            match("dbl");
            return new NodeFactDbl(dbl.lex());
        }

        /**
         * Parses a NegFact and returns the corresponding NegFactNode
         * with an isNegative boolean indicating whether the fact
         * is made negative with a prefix unary minus operator
         * or not.
         *
         * @return the parsed NodeNegFact
         */
        private NodeNegFact parseNegFact()
        {
            Boolean isNegative = false;
            if (curr().equals(new Token("-")))
            {
                match("-");
                isNegative = true;
            }
            NodeFact fact = parseFact();
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
        private NodeTerm parseTerm()
        {
            NodeNegFact negfact = parseNegFact();
            NodeMulop mulop = parseMulop();
            if (mulop == null)
                return new NodeTerm(negfact, null, null);
            NodeTerm term = parseTerm();
            term.append(new NodeTerm(negfact, mulop, null));
            return term;
        }

        /**
         * Parses an Expr and creates/returns the appropriate NodeExpr.
         * This can be a term * or / another expr, or just a term.
         *
         * @return the parsed NodeExpr
         */
        private NodeExpr parseExpr()
        {
            NodeTerm term = parseTerm();
            NodeAddop addop = parseAddop();
            if (addop == null)
                return new NodeExpr(term, null, null);
            NodeExpr expr = parseExpr();
            expr.append(new NodeExpr(term, addop, null));
            return expr;
        }

        /**
         * Parses an Assignment and creates/returns the appropriate NodeAssn.
         * This requires an identifier followed be an equals sign followed by
         * an expression.
         *
         * @return the parsed NodeAssn
         */
        private NodeAssn parseAssn()
        {
            Token id = curr();
            match("id");
            match("=");
            NodeExpr expr = parseExpr();
            NodeAssn assn = new NodeAssn(id.lex(), expr);
            return assn;
        }

        /**
         * Parses an relative operator and creates/returns the appropriate NodeRelOp.
         * 
         * @return the parsed NodeRelOp
         */
        private NodeRelOp parseRelOp()
        {
            if (curr().equals(new Token("<")))
            {
                match("<");
                return new NodeRelOp(pos(), "<");
            }
            else if (curr().equals(new Token("<=")))
            {
                match("<=");
                return new NodeRelOp(pos(), "<=");
            }
            else if (curr().equals(new Token(">")))
            {
                match(">");
                return new NodeRelOp(pos(), ">");
            }
            else if (curr().equals(new Token(">=")))
            {
                match(">=");
                return new NodeRelOp(pos(), ">=");
            }
            else if (curr().equals(new Token("<>")))
            {
                match("<>");
                return new NodeRelOp(pos(), "<>");
            }
            else if (curr().equals(new Token("==")))
            {
                match("==");
                return new NodeRelOp(pos(), "==");
            }
            return null;
        }

        /**
         * Parses a boolean expression and creates/returns the appropriate NodeBoolExpr.
         *
         * @return the parsed NodeBoolExpr
         */
        private NodeBoolExpr parseBoolExpr()
        {
            NodeExpr expr1 = parseExpr();
            NodeRelOp relop = parseRelOp();
            NodeExpr expr2 = parseExpr();
            return new NodeBoolExpr(expr1, relop, expr2);
        }

        /**
         * Parses a statement and creates/returns the appropriate NodeStmt.
         * View the NodeStmt class for more information on the cases
         * marked in this method.
         *
         * @return the parsed NodeStmt
         */
        private NodeStmt parseStmt()
        {
            if (curr().equals(new Token("wr")))
            {
                /* Case 2 */
                Token keyword = curr();
                match("wr");
                NodeExpr expr = parseExpr();
                return new NodeStmt(null, keyword, null,
                                    expr, null, null, null, null, pos());
            }
            else if (curr().equals(new Token("if")))
            {
                /* Case 3-4 */
                Token keyword = curr();
                match("if");
                NodeBoolExpr boolExpr = parseBoolExpr();
                match("then");
                NodeStmt stmt1 = parseStmt();
                if (curr().equals(new Token("else")))
                {
                    /* Case 4 */
                    match("else");
                    NodeStmt stmt2 = parseStmt();
                    return new NodeStmt(null, keyword, null, null,
                                        boolExpr, stmt1, stmt2, null, pos());
                }
                else
                {
                    /* Case 5 */
                    return new NodeStmt(null, keyword, null, null,
                                        boolExpr, stmt1, null, null, pos());
                }
            }
            else if (curr().equals(new Token("while")))
            {
                /* Case 5 */
                Token keyword = curr();
                match("while");
                NodeBoolExpr boolExpr = parseBoolExpr();
                match("do");
                NodeStmt stmt1 = parseStmt();
                return new NodeStmt(null, keyword, null, null,
                                    boolExpr, stmt1, null, null, pos());
            }
            else if (curr().equals(new Token("begin")))
            {
                /* Case 6 */
                Token keyword = curr();
                match("begin");
                NodeBlock block = parseBlock();
                match("end");
                return new NodeStmt(null, keyword, null, null,
                                    null, null, null,
                                    block, pos());
            }
            else
            {
                /* Case 1 */
                NodeAssn assn = parseAssn();
                NodeStmt stmt = new NodeStmt(assn, null, null, null,
                                             null, null, null, null, pos());
                return stmt;
            }
        }

        /**
         * Parses a block and creates/returns the appropriate NodeBlock
         *
         * @return the parsed NodeBlock
         */
        private NodeBlock parseBlock()
        {
            NodeStmt stmt = parseStmt();
            NodeBlock block = new NodeBlock(stmt, null);
            if (curr().equals(new Token(";")))
            {
                match(";");
                NodeBlock nextBlock = parseBlock();
                block.append(nextBlock);
                return block;
            }
            return block;
        }

        /**
         * Parses a program and creates/returns the appropriate NodeProg
         *
         * @return the parsed NodeProg
         */
        private NodeProg parseProg()
        {
            NodeBlock block = parseBlock();
            return new NodeProg(block);
        }

        /**
         * Creates and advances the scanner used in this parser,
         * and begins by parsing the statement; the first thing
         * in our Grammar.
         *
         * @return the first node in our parse tree
         */
        public Node parse(String program)
        {
            scanner = new Scanner(program);
            scanner.next();
            return parseProg();
        }
    }

}
