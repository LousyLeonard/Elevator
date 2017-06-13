using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Node<T>
    {
        private T node;
        private Node<T> next;
        private Node<T> prev;

        public Node(T element)
        {
            node = element;
        }

        public void setNext(Node<T> node)
        {
            next = node;
            node.prev = this;
        }

        public void setPrev(Node<T> node)
        {
            prev = node;
            node.next = this;
        }

        public T getElement()
        {
            return node;
        }

        public Node<T> getNext()
        {
            return next;
        }

        public Node<T> getPrev()
        {
            return prev;
        }
    }
}
