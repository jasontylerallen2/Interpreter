using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    /**
     * A class to represent a Token scanned by the scanner,
     * with the type of token and it's value (lexeme)
     * 
     * @author Jason Allen
     */
    public class Token
    {

        private String token;
        private String lexeme;

        /**
         * Construct the Token scanned by the scanner,
         * with the type of token and it's value (lexeme)
         */
        public Token(String token, String lexeme)
        {
            this.token = token;
            this.lexeme = lexeme;
        }

        /**
         * Construct the Token where the type of token and lexeme
         * are the same
         */
        public Token(String token)
        {
            this.token = token;
            this.lexeme = token;
        }

        /**
         * @return the type of token
         */
        public String Tok() { return token; }

        /**
         * @return the lexeme (value) of this Token object
         */
        public String Lex() { return lexeme; }

        /**
         * @return true if the token types are equal
         */
        public bool Equals(Token t)
        {
            return this.token == t.token;
        }

        /**
         * @return a String representation of this token.
         */
        public String ToString()
        {
            return "<" + Tok() + ", " + Lex() + ">";
        }

    }

}
