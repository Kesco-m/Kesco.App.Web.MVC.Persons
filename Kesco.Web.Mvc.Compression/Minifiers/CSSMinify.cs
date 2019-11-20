
using System;
using System.IO;
using System.Text;
using Kesco.Web.Mvc.Compression.Compression;

namespace Kesco.Web.Mvc.Compression.Minifiers
{
    internal class CSSMinify : ICompress
    {
        const int EOF = -1;

        TextReader tr;
        StringBuilder sb;
        int theA;
        int theB;
        int theLookahead = EOF;

        #region ICompress Members

        /// <summary>
        /// ICompress implementation
        /// </summary>
        /// <param name="script">script to compress</param>
        /// <returns>compressed string</returns>
        public string Compress(string script)
        {
            return MinifyString(script);
        }

        #endregion


        /// <summary>
        /// Minify the input script
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public string Minify(string file)
        {
            sb = new StringBuilder();
            tr = new StreamReader(file);
            theA = '\n';
            theB = 0;
            theLookahead = EOF;
            cssmin();
            return sb.ToString();
        }

        /// <summary>
        /// Minifies from a string
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public string MinifyString(string script)
        {
            sb = new StringBuilder();
            tr = new StreamReader(new MemoryStream(new System.Text.UTF8Encoding().GetBytes(script)));
            theA = '\n';
            theB = 0;
            theLookahead = EOF;
            cssmin();
            return sb.ToString();
        }    
        

        /// <summary>
        /// Excute the actual minify
        /// </summary>
        void cssmin()
        {
            action(3);
            while (theA != EOF)
            {
                switch (theA)
                {
                    case ' ':
                        {
                            switch (theB)
                            {
                                case ' ':        //body.Replace("  ", String.Empty);
                                case '{':        //body = body.Replace(" {", "{");
                                case ':':        //body = body.Replace(" {", "{");
                                case '\n':       //body = body.Replace(" \n", "\n");
                                case '\r':       //body = body.Replace(" \r", "\r");
                                case '\t':       //body = body.Replace(" \t", "\t");
                                    action(2);
                                    break;
                                default:
                                    action(1);
                                    break;
                            }
                            break;
                        }
                    case ':':
                    case ',':
                        {
                            switch (theB)
                            {
                                case ' ':       //body.Replace(": ", ":");  body.Replace(", ", ",");  
                                    action(3);
                                    break;
                                default:
                                    action(1);
                                    break;
                            }
                            break;
                        }
                    case ';':
                        {
                            switch (theB)
                            {
                                case ' ':       //body.Replace("; ", ";");  
                                case '\n':      //body = body.Replace(";\n", ";");
                                case '\r':      //body = body.Replace(";\r", ";");
                                case '\t':      //body = body.Replace(";\t", ";");
                                    action(3);
                                    break;
                                case '}':       //body.Replace(";}", "}");
                                    action(2);
                                    break;
                                default:
                                    action(1);
                                    break;
                            }
                            break;
                        }
                    case '\t':              //body = body.Replace("\t", "");
                    case '\r':              //body = body.Replace("\r", "");
                    case '\n':              //body = body.Replace("\n", "");
                        action(2);
                        break;
                    default:
                        action(1);
                        break;
                }
            }
        }
        /* action -- do something! What you do is determined by the argument:
                1   Output A. Copy B to A. Get the next B.
                2   Copy B to A. Get the next B. (Delete A).
                3   Get the next B. (Delete B).
        */
        void action(int d)
        {
            if (d <= 1)
            {
                put(theA);
            }
            if (d <= 2)
            {
                theA = theB;
                if (theA == '\'' || theA == '"')
                {
                    for (; ; )
                    {
                        put(theA);
                        theA = get();
                        if (theA == theB)
                        {
                            break;
                        }
                        if (theA <= '\n')
                        {
                            throw new FormatException(string.Format("Error: unterminated string literal: {0}\n", theA));
                        }
                        if (theA == '\\')
                        {
                            put(theA);
                            theA = get();
                        }
                    }
                }
            }
            if (d <= 3)
            {
                theB = next();
                if (theB == '/' && (theA == '(' || theA == ',' || theA == '=' ||
                                    theA == '[' || theA == '!' || theA == ':' ||
                                    theA == '&' || theA == '|' || theA == '?' ||
                                    theA == '{' || theA == '}' || theA == ';' ||
                                    theA == '\n'))
                {
                    put(theA);
                    put(theB);
                    for (; ; )
                    {
                        theA = get();
                        if (theA == '/')
                        {
                            break;
                        }
                        else if (theA == '\\')
                        {
                            put(theA);
                            theA = get();
                        }
                        else if (theA <= '\n')
                        {
                            throw new FormatException(string.Format("Error: unterminated Regular Expression literal : {0}.\n", theA));
                        }
                        put(theA);
                    }
                    theB = next();
                }
            }
        }
        /* next -- get the next character, excluding comments. peek() is used to see
                if a '/' is followed by a '/' or '*'.
        */
        int next()
        {
            int c = get();
            if (c == '/')
            {
                switch (peek())
                {
                    case '/':
                        {
                            for (; ; )
                            {
                                c = get();
                                if (c <= '\n')
                                {
                                    return c;
                                }
                            }
                        }
                    case '*':
                        {
                            get();
                            for (; ; )
                            {
                                switch (get())
                                {
                                    case '*':
                                        {
                                            if (peek() == '/')
                                            {
                                                get();
                                                return ' ';
                                            }
                                            break;
                                        }
                                    case EOF:
                                        {
                                            throw new FormatException("Error: Unterminated comment.\n");
                                        }
                                }
                            }
                        }
                    default:
                        {
                            return c;
                        }
                }
            }
            return c;
        }
        /* peek -- get the next character without getting it.
        */
        int peek()
        {
            theLookahead = get();
            return theLookahead;
        }
        /* get -- return the next character from stdin. Watch out for lookahead. If
                the character is a control character, translate it to a space or
                linefeed.
        */
        int get()
        {
            int c = theLookahead;
            theLookahead = EOF;
            if (c == EOF)
            {
                c = tr.Read();
            }
            if (c >= ' ' || c == '\n' || c == EOF)
            {
                return c;
            }
            if (c == '\r')
            {
                return '\n';
            }
            return ' ';
        }
        void put(int c)
        {
            sb.Append((char)c);
        }

       
    }
}
