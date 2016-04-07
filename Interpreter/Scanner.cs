﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    /**
     * The Scanner used in this interpreter, which essentially just splits
     * everything into tokens - the smallest meaningful bits of a program.
     * This requires recognizing whitespace, identifiers (start with a
     * letter), digits, comments, operators and will eventually be extended
     * to support keywords.
     * 
     * @author Jason Allen
     */
    class Scanner
    {

        private String program;
        private int position;
        private Token token;

        private HashSet<String> whitespace = new HashSet<String>();
        private HashSet<String> digits = new HashSet<String>();
        private HashSet<String> letters = new HashSet<String>();
        private HashSet<String> legits = new HashSet<String>();
        private HashSet<String> keywords = new HashSet<String>();
        private HashSet<String> operators = new HashSet<String>();
        private HashSet<String> comments = new HashSet<String>();

        /**
         * Fill the HashSets representing digits, letters, legits,
         * etc with a simple loop.
         * 
         * @return void
         */
        private void fill(HashSet<String> s, char lo, char hi)
        {
            for (char c = lo; c <= hi; c++)
            {
                s.Add(c + "");
            }
        }

         /**
         * Add spaces, new lines, and tab characters to our whitespace set
         * 
         * @return void
         */
        private void initWhitespace(HashSet<String> s)
        {
            s.Add(" ");
            s.Add("\n");
            s.Add("\t");
        }

        /**
         * Add digits 9-0 to our digits set
         * 
         * @return void
         */
        private void initDigits(HashSet<String> s)
        {
            fill(s, '0', '9');
        }

        /**
         * Add uppercase and lowercase letters to our letters set
         * 
         * @return void
         */
        private void initLetters(HashSet<String> s)
        {
            fill(s, 'A', 'Z');
            fill(s, 'a', 'z');
        }

        /**
         * Add all the letters and the digits to our "legitimates" set
         * 
         * @return void
         */
        private void initLegits(HashSet<String> s)
        {
            foreach (String l in letters)
            {
                s.Add(l);
            }
            foreach(String d in digits)
            {
                s.Add(d);
            }
        }

        /**
         * Add all the mathematical operators to our operators set
         * 
         * @return void
         */
        private void initOperators(HashSet<String> s)
        {
            s.Add("=");
            s.Add("+");
            s.Add("-");
            s.Add("*");
            s.Add("/");
            s.Add("(");
            s.Add(")");
            s.Add(";");
            s.Add("<");
            s.Add("<=");
            s.Add(">");
            s.Add(">=");
            s.Add("<>");
            s.Add("==");
        }

        /**
         * Add all the keywords to our keywords set; none are implemented yet
         * 
         * @return void
         */
        private void initKeywords(HashSet<String> s)
        {
            s.Add("wr");
            s.Add("if");
            s.Add("then");
            s.Add("else");
            s.Add("while");
            s.Add("do");
            s.Add("begin");
            s.Add("end");
        }

        /**
         * Add the symbol '#' to represent comments in our comments set
         * 
         * @return void
         */
        private void initComments(HashSet<String> s)
        {
            s.Add("#");
        }

        /**
         * Construct a Scanner given a program as a String. Use all
         * the initializers and start the pos at 0 and the current
         * token at null
         */
        public Scanner(String program)
        {
            this.program = program;
            position = 0;
            token = null;
            initWhitespace(whitespace);
            initDigits(digits);
            initLetters(letters);
            initLegits(legits);
            initKeywords(keywords);
            initOperators(operators);
            initComments(comments);
        }

        /**
         * @return a boolean indicating if we are "done" scanning the program
         */
        public Boolean done()
        {
            return position >= program.Length;
        }

        /**
         * Eat up all of a character and advances the position counter accordingly
         * 
         * @return void
         */
        private void many(HashSet<String> s)
        {
            while (!done() && s.Contains(program[position] + ""))
            {
                position++;
            }
        }

        /**
         * Set the token equal to a double if we are given digits at the start
         * of a "word". If there is a decimal, continue grabbing any digits
         * after it for our double.
         * 
         * @return void
         */
        private void nextDbl()
        {
            int old = position;
            many(digits);

            // handle double values
            if (program[position] == '.') 
            {
        	    position++;
                many(digits);
            }
            token = new Token("dbl", program.Substring(old, position - old));
        }

        /**
         * Set the token equal to an id if we are given a letter at the start
         * of a "word".
         * 
         * @return void
         */
        private void nextKwId()
        {
            int old = position;
            many(letters);
            many(legits);
            String lexeme = program.Substring(old, position - old);
            token = new Token((keywords.Contains(lexeme) ? lexeme : "id"), lexeme);
        }

        /**
         * Set the token equal to a given operator if a "word" matches one of
         * the operators in our set.
         * 
         * @return void
         */
        private void nextOp()
        {
            int old = position;
            position = old + 2;
            // We could have to be able to have 2 character long operators
            // such as "<="; this will attempt to match a 2 character
            // operator first, and then attempt to match a 1 character operator
            if (!done())
            {
                String lexeme = program.Substring(old, 2);
                if (operators.Contains(lexeme))
                {
                    token = new Token(lexeme);
                    return;
                }
            }
            position = old + 1;
            String lex = program.Substring(old, 1);
            token = new Token(lex);
        }

        /**
         * Advance past all comment markers # to the end of the line
         * 
         * @return void
         */
        private void nextComment()
        {
            while (!done() && program[position] != '\n')
            {
                position++;
            }
            next();
        }

        /**
         * Form the token based on the first letter of the word. If it's
         * whitespace, advance beyond it, if it starts with a digit,
         * call nextDbl(), if it start with a letter it ought to be an
         * identifier, so call nextKwId(), if it's a # go past it to
         * the end of the line without forming tokens, if it's an
         * operator call nextOp(). Stop when there is nothing left
         * scan.
         * 
         * @return a boolean indicating if the program is done
         */
        public Boolean next()
        {
            if (done())
            {
                return false;
            }
            many(whitespace);
            String c = program[position] + "";
            if (digits.Contains(c))
            {
                nextDbl();
            }
            else if (letters.Contains(c))
            {
                nextKwId();
            }
            else if (operators.Contains(c))
            {
                nextOp();
            }
            else if (comments.Contains(c))
            {
                 nextComment();
            }
            else
            {
                Console.Error.WriteLine("illegal character at position " + position);
                position++;
                return next();
            }
            return true;
        }

        /**
         * Test if the current token equals the given token and
         * thrown a SyntaxException if not. Advance past the char
         * otherwise.
         * 
         * @return void
         */
        public void match(Token t)
        {
            if (!t.equals(curr()))
            {
                throw new Exception("Unexpected character: " + curr().lex() + " at position " + pos() + ". Expected " + t.lex());
            }
            next();
        }

        /**
         * @return the current token
         */
        public Token curr() { return token; }

        /**
         * @return the current position
         */
        public int pos() { return position; }

    }
}
