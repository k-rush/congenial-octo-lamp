using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Program
    {
        
        static void Main(string[] args)
        {
            MinHeap<int> heap = new MinHeap<int>(10);
            Console.WriteLine("Initial Heap:");
            heap.Print();
            Console.WriteLine("Adding 7 to heap:");
            heap.Insert(new HeapNode<int>(7));
            Console.WriteLine("Adding 8 to heap:");
            heap.Insert(new HeapNode<int>(8));
            Console.WriteLine("Adding 3 to heap:");
            heap.Insert(new HeapNode<int>(3));
            Console.WriteLine("Adding 5 to heap:");
            heap.Insert(new HeapNode<int>(5));
            Console.WriteLine("Adding 2 to heap:");
            heap.Insert(new HeapNode<int>(2));
            Console.WriteLine("Adding 1 to heap:");
            heap.Insert(new HeapNode<int>(1));

        }

        private class MinHeap<T> where T : IComparable<T>
        {
            private HeapNode<T> Head;
            public MinHeap(T value)
            {
                Head = new HeapNode<T>(value);
            }
            public void Insert(HeapNode<T> node)
            {
                AddNode(node);
                Console.WriteLine($"Heap after adding node to first open position:");
                Print();

                //Shape of the heap is correct, node is out of place
                Stack<HeapNode<T>> heapNodes = DFSHelper(Head, new Stack<HeapNode<T>>(), node.Value);
                Console.WriteLine("Stack of nodes leading to value being added:");
                PrintStack(heapNodes);
                Console.WriteLine();

                HeapNode<T> current = heapNodes.Pop();
                int i = 0;
                
                //While the current node is less than the parent, we need to do something.
                //Also, if we've reached the end of the stack, our inserted value is now the root.
                if (heapNodes.Count > 0)
                {
                    HeapNode<T> parent;
                    
                    
                    while (current.Value.CompareTo((parent = heapNodes.Pop()).Value) < 0)
                    {
                        i++;
                        // swap the values (this maintains the tree structure, and we don't have to worry about left-right, all we're doing is swapping values.)
                        T tempValue = parent.Value;
                        parent.Value = current.Value;
                        current.Value = tempValue;
                        current = parent;  // Now we can move on to the next level
                        Console.WriteLine($"******** Step {i} ********");
                        Print();
                        
                        if (heapNodes.Count == 0) break;
                    }
                }
                
            }

            //Construct a stack trace of a DFS to find the value we're seaking.
            private Stack<HeapNode<T>> DFSHelper(HeapNode<T> currentNode, Stack<HeapNode<T>> stack, T value, bool isLeft = true)
            {
                
                if (currentNode == null)
                {
                    return null;
                }
                stack.Push(currentNode);

                if (currentNode.Value.Equals(value))
                    return stack;
                else
                {
                    //Is there a ebtter way to do this? Does this even work?
                    Stack<HeapNode<T>> tempStack = DFSHelper(currentNode.Left, stack, value, true) ?? DFSHelper(currentNode.Right, stack, value, false);

                    if (tempStack == null)
                    {
                        stack.Pop();
                        return null;
                    }
                    else return tempStack;
                }
            }

            private void AddNode(HeapNode<T> newNode)
            {
                var queue = new Queue<HeapNode<T>>();
                queue.Enqueue(Head);
                while (queue.Count != 0)
                {
                    HeapNode<T> node = queue.Dequeue();
                    if (node.Left == null)
                    {
                        node.Left = newNode;
                        return;
                    }

                    queue.Enqueue(node.Left);

                    if (node.Right == null)
                    {
                        node.Right = newNode;
                        return;
                    }

                    queue.Enqueue(node.Right);
                }
            }

            public static void PrintStack(Stack<HeapNode<T>> stack)
            {
                Stack<HeapNode<T>> tempStack = new Stack<HeapNode<T>>(stack);
                while (tempStack.Count > 0)
                {
                    Console.Write($"{tempStack.Pop().Value}   ");
                }
            }

            public static void PrintRecursive(HeapNode<T> node, string indent, bool right)
            {
                string bullet = right ? "R- " : "L- ";
                Console.WriteLine(indent + bullet + node.Value);
                indent += right ? "   " : "|  ";
                if (node.Left != null) PrintRecursive(node.Left, indent, false);
                if (node.Right != null) PrintRecursive(node.Right, indent, true);
            }

            public void Print()
            {
                PrintRecursive(Head, "", false);
            }
        }

        public class HeapNode<T> where T : IComparable<T>
        {
            public HeapNode<T> Left;
            public HeapNode<T> Right;
            public T Value;

            public HeapNode(T value)
            {
                Value = value;
            }

            
        }
    }
}
