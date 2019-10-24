using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WORKPROJECT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a start word");
            string startword = Console.ReadLine();
            //Console.WriteLine("Please enter an end word");
            //string endword = Console.ReadLine();

            List<string> wordList = File.ReadLines("wordlist.txt").Select(w => w.ToLower()).ToList();
            wordList = wordList.Where(w => w.Length == startword.Length).ToList();

            // search words that have same character length and can vary by 1 character.
            // find the list for the start word then establish the start word as the parent
            // have the children list inherit the parent list and recursively inherit the lists until reaching the final end word.

            //WordRelationalNode node = new WordRelationalNode();

            
            var setWord = startword;
            var setParent = new WordRelationalNode(setWord, null);
            //when there is no parent or parent is null you know your on the way to root node.

            //var children = wordList;
            //List<WordRelationalNode> children = new List<WordRelationalNode>();
            var children = RepeatList(setWord, wordList);

            foreach (var word in children)
            {
                WordRelationalNode newChild = new WordRelationalNode(word, setParent);
                setParent.AddChild(newChild);
                Console.WriteLine(word);
            }

            //step 1 make root node
            //step 2 make a method that takes a node and adds all children
            //step 3 for all children added execute step 2

            //public class List<WordRelationalNode> : WordRelationalNode
            //first word = start word is the root node
            // each node has a list of children and it has a string that is the actual word represented by that part
            // 2 steps, for any node that you hand in youu want to find all the childrens for that word that it is representing
            // then from that info includng all the children, the original word, and the node, you want to build a node and put it in the model.
            //infnite loop problem and make sure words arent same twice, validation issues to think about.
        }

        public static AddSingleDiffChildren(WordRelationalNode parent)
        {
            var children = RepeatList(parent.Word, parent);

            foreach (var word in children)
            {
                WordRelationalNode newChild = new WordRelationalNode(word, setParent);
                setParent.AddChild(newChild);
                Console.WriteLine(word);
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
        public static List<string> RepeatList(String startword, List<WordRelationalNode> children)
        {
            var wordlist = children.Where(w => w.Length == startword.Length).Where(w2 => StringCharDiff(startword, w2) == true).ToList();
            Console.Write(string.Join(",", wordlist));
            Console.ReadKey();

            return wordlist;
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

        public void AddAllChildren(WordRelationalNode addChild, List<WordRelationalNode> Children)
        {
            while(addChild != null)
            {
                foreach (WordRelationalNode child in Children)
                {
                    Children.Add(addChild);
                }
                break;
            }    
        }
        //public void AddChild(WordRelationalNode addChild)
        //{
        //    foreach (WordRelationalNode child in Children)
        //    {
        //        Children.Add(addChild);
        //        if (child.Word == Word)
        //        {
        //            break;
        //        }
        //        Console.WriteLine(Word);
        //    }
        //}

        //public void AddChild(WordRelationalNode addChild, List<WordRelationalNode> Children)
        //{
        //    if (addChild == null)
        //        return;

        //    Children.Add(addChild);
        //    Console.WriteLine(addChild);

        //    foreach (var n in addChild.Word)
        //    {
        //        AddChild(addChild, Children);
        //    }
        //}

    }
}

//public static void GetNodes(Node node)
//{
//    if (node == null)
//    {
//        return;
//    }
//    Console.WriteLine(node.Name);
//    foreach (var n in node.Nodes)
//    {
//        GetNodes(n);
//    }
//}
// * 


////while (startword != endword)
////{
////if (startword == endword)
////{
////    break;
////}