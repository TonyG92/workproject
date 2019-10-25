using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WORKPROJECT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a start word");
            string startword = Console.ReadLine();
            Console.WriteLine("Please enter an end word");
            string endword = Console.ReadLine();
            Console.Clear();

            List<string> wordList = File.ReadLines("wordlist.txt").Where(w => w.Length == startword.Length).Select(w => w.ToLower()).ToList();     // select = map, runs the function on every single thing in the list.
            //wordList = wordList.Where(w => w.Length == startword.Length).ToList(); -- slighty more optimized above.

            // search words that have same character length and can vary by 1 character.
            // find the list for the start word then establish the start word as the parent
            // have the children list inherit the parent list and recursively inherit the lists until reaching the final end word.

            var setWord = startword;
            var setParent = new WordRelationalNode(setWord, null);      //when there is no parent or parent is null you know your on the way to root node.
            AddSingleDiffChildren(setParent, wordList, endword);


            //var children = RepeatList(setParent, wordList, endword);

            //foreach (var word in children)
            //{
            //    WordRelationalNode newChild = new WordRelationalNode(word, setParent);
            //    setParent.AddChild(newChild);
            //    Console.WriteLine(word);
            //}

            //step 1 make root node
            //step 2 make a method that takes a node and adds all children
            //step 3 for all children added execute step 2

            //first word = start word is the root node
            // each node has a list of children and it has a string that is the actual word represented by that part
            // 2 steps, for any node that you hand in youu want to find all the childrens for that word that it is representing
            // then from that info includng all the children, the original word, and the node, you want to build a node and put it in the model.
            //infnite loop problem and make sure words arent same twice, validation issues to think about.

            //Breadth First Searching is the last node is written first

            Queue<WordRelationalNode> q = new Queue<WordRelationalNode>(); // put the parent node into a Queue, FIFO
            q.Enqueue(setParent);
            WordRelationalNode current = null; //store nothing in current node

            while (q.Count != 0)  // as long as there is stuff in the queue keep doing work
            {
                current = q.Dequeue();    /// store the first node in queue in current
                
                foreach (WordRelationalNode child in current.Children)    // child is the next node in the list of current children 
                {
                    q.Enqueue(child);   // put next node in queue
                }
            }

            Stack<WordRelationalNode> s = new Stack<WordRelationalNode>();  // stack = LIFO last in first out


            while (current != null)
            {
                s.Push(current); // this pushes the current into a stack.
                //Console.WriteLine(current.Word);
                current = current.Parent;  /// flips order by pushing into stack
            }

            while (s.Count != 0)             // as long as there is stuff in the stack keep doing work
            {
                Console.WriteLine(s.Pop().Word); // removes top node out and in this case top word out.
            }
           
           


        }

        public static void AddSingleDiffChildren(WordRelationalNode parent, List<string> wordList, string endword)
        {
            var children = RepeatList(parent, wordList, endword);

            foreach (var word in children)
            {
                WordRelationalNode newChild = new WordRelationalNode(word, parent);
                parent.AddChild(newChild);
                AddSingleDiffChildren(newChild, wordList, endword);
                //Console.WriteLine(word);
            }
        }

        public static bool StringCharDiff(string start, string match)
        {
            //find the word in the list and move to the method which defines the criteria comparing the word 
            {
                int counter = 0;
                for (int i = 0; i < start.Length; i++) //start at the first letter and keep going through all the letters until we check all the letters (length)
                {
                    if (start[i] != match[i]) //if the letters do not match 
                    {
                        counter++; //how many letters are diff between the words
                    }
                }
                if (counter == 1)
                {
                    return true;
                }
                return false;
            }
        }
        public static List<string> RepeatList(WordRelationalNode currentNode, List<string> wordList, string endword)
        {
            int curDistance = Distance(currentNode.Word, endword);

            var wordlist = wordList.Where(w => StringCharDiff(currentNode.Word, w)).Where(w =>
            {
                if (currentNode.Parent == null)
                {
                    return true;
                }
                return w != currentNode.Parent.Word;
            })
            .Where(w => Distance(w, endword) < curDistance)
            .ToList();

            //Console.Write(string.Join(",", wordlist));

            return wordlist;
        }

        
        /// Count and return the number of times w1
        /// characters do not match w2
        private static int Distance(string w1, string w2)
        {
            return w1.ToCharArray()
                .Zip(w2.ToCharArray(), (c1, c2) => new { c1, c2 })
                .Count(m => m.c1 != m.c2);
        }


    }
    public class WordRelationalNode
    {
        //Inheritantly recursive 
        public WordRelationalNode() { return; }
        public WordRelationalNode(string setWord, WordRelationalNode setParent) : this()
        {
            Word = setWord;
            Parent = setParent; //parent is cat
            //generate list from cat
        }
        //Parent, Children and word are all properties 
        //parent generates the children
        //Everyone of the children is a copy of the node

        //next step is cat is the parent then make its own list, find the word and return a list of words until you get to dog 
        public WordRelationalNode Parent { get; set; } = null;
        public List<WordRelationalNode> Children { get; set; } = new List<WordRelationalNode>();
        public string Word { get; set; }
        //Next steps is to read in a list from a text file, instantiate the class , pass in word
        //Do methods call on each one and return the class

        //step 1 make root node == startword
        //step 2 make a method that takes a node and adds all children
        //step 3 for all children added execute step 2

        public void AddChild(WordRelationalNode addChild)
        {
            Children.Add(addChild);
        }
    }
}