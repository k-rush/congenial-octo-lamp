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
        }

        private class MinHeap<T>
        {
            HeapNode<T> Head;
            
            private void Insert(HeapNode<T> node)
            {
                AddNode(node);

                //Shape of the heap is correct, node is out of place
                Stack<HeapNode<T>> heapNodes = CreateStack(Head, node.Value);

                if (heapNodes != null)
                {

                }
            }

            private Stack<HeapNode<T>> CreateStack(HeapNode<T> Head, T value)
            {
                Stack<HeapNode<T>> heapNodes;
                heapNodes = DFSHelper(Head, new Stack<HeapNode<T>>(), value);

                return heapNodes;
            }

            private Stack<HeapNode<T>> DFSHelper(HeapNode<T> currentNode, Stack<HeapNode<T>> stack, T value)
            {
                stack.Push(currentNode);
                if (currentNode.Value.Equals(value))
                {
                    return stack;
                }
                else
                {
                    if (currentNode.Left != null)
                    {
                        DFSHelper(currentNode.Left, stack, value);
                    }

                    stack.Pop();

                    if (currentNode.Right != null)
                    {
                        DFSHelper(currentNode.Right, stack, value);
                    }

                    stack.Pop();
                }

                return null;
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
                    }

                    queue.Enqueue(node.Left);

                    if (node.Right == null)
                    {
                        node.Right = newNode;
                    }

                    queue.Enqueue(node.Right);
                }
            }
        }

        public class HeapNode<T>
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
