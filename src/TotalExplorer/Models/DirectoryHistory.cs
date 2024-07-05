using System;
using System.Collections.Generic;

namespace TotalExplorer.Models
{
    internal class DirectoryHistory : IDirectoryHistory
    {
        private const int Size = 32;
        private LinkedListNode<string> _currentNode;

        private LinkedList<string> _directoryNodes = new();

        public DirectoryHistory(string dirName) 
        {
            var startNode = new LinkedListNode<string>(dirName);
            _currentNode = startNode;
            _directoryNodes.AddLast(startNode);
        }

        public bool CanMoveBack 
        { 
            get 
            {
                if(_currentNode.Previous == null) return false;
                return true;
            } 
        }
        
        public bool CanMoveForward
        {
            get
            {
                if (_currentNode.Next == null) return false;
                return true;
            }
        }

        public string Current => _currentNode.Value;

        public void MoveBack()
        {
            if(_currentNode.Previous is null) throw new NullReferenceException();
            _currentNode = _currentNode.Previous;
        }

        public void MoveForward()
        {
            if (_currentNode.Next is null) throw new NullReferenceException();
            _currentNode = _currentNode.Next;
        }

        public void Add(string node)
        {
            var newNode = new LinkedListNode<string>(node);

            RemoveAfter(_currentNode);
            _directoryNodes.AddAfter(_currentNode, newNode);
            _currentNode = newNode;

            if (_directoryNodes.Count > Size)
                _directoryNodes.RemoveFirst();
        }

        public void Remove(string node)
        {
            _directoryNodes.Remove(node);
        }

        public void RemoveAfter(LinkedListNode<string> node)
        {
            while(node.Next != null)
                _directoryNodes.Remove(node.Next);
        }

    }
}
