using System;
using System.Collections.Generic;
using System.Text;

namespace Isen.AntoineBerenguer.Library
{
    public class Node : INode
    {
        private string _value;
        private readonly Guid _id;
        private Node _parent;
        private List<Node> _children;
  
        public Node(Node parent, string value)
        {
            _parent = parent;
            _children = new List<Node>();
            _value = value;
            _id = System.Guid.NewGuid();
        }

        public Node(Node parent) : this(parent, String.Empty) { }

        public Node(string value) : this(null, value) { }

        public Node() : this(null, String.Empty) { }

        public string Value { get => _value; set => _value = value; }
        public Guid Id { get => _id; }
        public Node Parent { get => _parent; set => _parent = value; }
        public List<Node> Children { get => _children; set => _children = value; }

        public int Depth
        {
            get
            {
                if (Parent == null)
                {
                    return 0;
                }
                else
                {
                    return Parent.Depth + 1;
                }
            }
        }

        public bool Equals(Node other)
        {
            return this.Id.Equals(other.Id) && this.Value.Equals(other.Value);
        }
    }
}
