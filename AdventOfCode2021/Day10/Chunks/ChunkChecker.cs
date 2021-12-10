using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10.Chunks
{
    public class ChunkChecker
    {
        private Dictionary<string, string> _LegalOpenBrackets;
        private Dictionary<string, int> _LegalClosingBrackets;
        public ChunkChecker()
        {
            this._LegalOpenBrackets = new Dictionary<string, string>();
            this._LegalOpenBrackets.Add("(", ")");
            this._LegalOpenBrackets.Add("[", "]");
            this._LegalOpenBrackets.Add("{", "}");
            this._LegalOpenBrackets.Add("<", ">");

            this._LegalClosingBrackets = new Dictionary<string, int>();
            this._LegalClosingBrackets.Add(")", 3);
            this._LegalClosingBrackets.Add("]", 57);
            this._LegalClosingBrackets.Add("}", 1197);
            this._LegalClosingBrackets.Add(">", 25137);

        }

        /// <summary>
        /// Puzzle one
        /// </summary>
        /// <param name="bracketChunk"></param>
        /// <returns></returns>
        public int CheckBrackets(string bracketChunk)
        {
            // keeps track of brackets within brackets. when a child brackek is complete
            // (has its opening and closing matching), we check back on the stack to see
            // there there are any parents brackets. When then find that parents closing bracks
            // and then remove it from the stack. Keep going until there are no more parents left
            // at this point we have to look for a new opening bracket. If we found a closing bracket
            // when there is nothing left on the stack, the input data is wrong.
            Stack<string> stack = new Stack<string>();
            string BracketWeAreLookingFor = "";
            for(int eachChar = 0; eachChar < bracketChunk.Length; eachChar++)
            {


                string currentBracketLookingAt = bracketChunk[eachChar].ToString();

                // if we don't have a opening bracket
                if(BracketWeAreLookingFor == "")
                {
                    // is it a closing bracket (this would be wrong)
                    if (this._LegalOpenBrackets.ContainsKey(currentBracketLookingAt) == false)
                    {// we can't start with a closing bracket, somthing is wrong

                        // look up the closing bracket and see how many points its worth.
                        return this.GetClosingBracketPoints(currentBracketLookingAt);
                    }
                    // we have a legal opening bracket
                    else
                    {

                        // add the bracket to the stack so we can keep track
                        stack.Push(currentBracketLookingAt);
                        // find this opening brackets equivelent closing bracket
                        BracketWeAreLookingFor = this._LegalOpenBrackets[currentBracketLookingAt];
                    }
                }
                else
                {
                    // if we find the bracket we are looking for
                    if (currentBracketLookingAt == BracketWeAreLookingFor)
                    {
                        
                        // remove the opening bracket from the stack
                        stack.Pop();

                        // is there any more opening brackets in the stack
                        if (stack.Count > 0)
                            // get the opening brackets equivelents closing bracket
                            BracketWeAreLookingFor = this._LegalOpenBrackets[stack.Peek()];
                        else
                            // there are no more brackets in the stack. On the next loop around we will
                            // have to look for a new opening bracket
                            BracketWeAreLookingFor = "";
                    }
                    // we have not found the bracke we were looking for
                    else
                    {
                        // is the current bracket an opening bracket
                        if (this._LegalOpenBrackets.ContainsKey(currentBracketLookingAt) == true)
                        {// this is a bracket within a bracket.

                            // we now need to start looking for the equivelent closing bracket to this bracket
                            BracketWeAreLookingFor = this._LegalOpenBrackets[currentBracketLookingAt];
                            stack.Push(currentBracketLookingAt);

                        }
                        // current bracket must be a closing bracket.
                        else
                        {// this closing bracket should not be hear

                            // look up the closing bracket and see how many points its worth.
                            return this.GetClosingBracketPoints(currentBracketLookingAt);
                        }


                    }
                }

            }

            // everything is ok, no problems found.
            return 0;

        }

        /// <summary>
        /// Puzzle Two
        /// </summary>
        /// <param name="bracketChunk"></param>
        /// <returns></returns>
        public long CheckAndCompleteBrackets(string bracketChunk)
        {
            // keeps track of brackets within brackets. when a child brackek is complete
            // (has its opening and closing matching), we check back on the stack to see
            // there there are any parents brackets. When then find that parents closing bracks
            // and then remove it from the stack. Keep going until there are no more parents left
            // at this point we have to look for a new opening bracket. If we found a closing bracket
            // when there is nothing left on the stack, the input data is wrong.
            Stack<string> stack = new Stack<string>();
            string BracketWeAreLookingFor = "";
            for (int eachChar = 0; eachChar < bracketChunk.Length; eachChar++)
            {
                
                string currentBracketLookingAt = bracketChunk[eachChar].ToString();

                // if we don't have a opening bracket
                if (BracketWeAreLookingFor == "")
                {
                    // is it a closing bracket (this would be wrong)
                    if (this._LegalOpenBrackets.ContainsKey(currentBracketLookingAt) == false)
                    {// we can't start with a closing bracket, somthing is wrong

                        //look up the closing bracket and see how many points its worth.
                        //return this.GetClosingBracketPoints(currentBracketLookingAt);

                        // indicates this is a currupt chunk of data
                        return -1;

                    }
                    // we have a legal opening bracket
                    else
                    {

                        // add the bracket to the stack so we can keep track
                        stack.Push(currentBracketLookingAt);
                        // find this opening brackets equivelent closing bracket
                        BracketWeAreLookingFor = this._LegalOpenBrackets[currentBracketLookingAt];
                    }
                }
                else
                {
                    // if we find the bracket we are looking for
                    if (currentBracketLookingAt == BracketWeAreLookingFor)
                    {

                        // remove the opening bracket from the stack
                        stack.Pop();

                        // is there any more opening brackets in the stack
                        if (stack.Count > 0)
                            // get the opening brackets equivelents closing bracket
                            BracketWeAreLookingFor = this._LegalOpenBrackets[stack.Peek()];
                        else
                            // there are no more brackets in the stack. On the next loop around we will
                            // have to look for a new opening bracket
                            BracketWeAreLookingFor = "";
                    }
                    // we have not found the bracke we were looking for
                    else
                    {
                        // is the current bracket an opening bracket
                        if (this._LegalOpenBrackets.ContainsKey(currentBracketLookingAt) == true)
                        {// this is a bracket within a bracket.

                            // we now need to start looking for the equivelent closing bracket to this bracket
                            BracketWeAreLookingFor = this._LegalOpenBrackets[currentBracketLookingAt];
                            stack.Push(currentBracketLookingAt);

                        }
                        // current bracket must be a closing bracket.
                        else
                        {// this closing bracket should not be hear

                            // look up the closing bracket and see how many points its worth.
                            //return this.GetClosingBracketPoints(currentBracketLookingAt);

                            // indicates this is a currupt chunk of data
                            return -1;
                            
                        }


                    }
                }

            }

            string completedBrackets = new String(bracketChunk);
            long TotalScore = 0;
            foreach (string bracket in stack)
            {
                string closingBracket = this._LegalOpenBrackets[bracket];
                completedBrackets = completedBrackets.Insert(completedBrackets.Length, closingBracket);

                TotalScore *= (long)5;

                switch(closingBracket)
                {
                    case ")":
                        TotalScore += (long)1;
                        break;

                    case "]":
                        TotalScore += (long)2;
                        break;

                    case "}":
                        TotalScore += (long)3;
                        break;

                    case ">":
                        TotalScore += (long)4;
                        break;

                }
            }

            // everything is ok, no problems found.
            return TotalScore;

        }


        private int GetClosingBracketPoints(string ClosingBracket)
        {
            // look up the closing bracket and see how many points its worth.
            if (this._LegalClosingBrackets.ContainsKey(ClosingBracket) == true)
            {
                return this._LegalClosingBrackets[ClosingBracket];
            }
            else
            {// we could not find the closing bracket in our list
                return 0;
            }
        }
    }



}
