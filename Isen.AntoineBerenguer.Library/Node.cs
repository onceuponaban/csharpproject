using System;
using System.Collections.Generic;
using System.Text;

namespace Isen.AntoineBerenguer.Library
{
    public class Node<T> : INode<T>
    {
        private T _value;
        private readonly Guid _id;
        private Node<T> _parent;
        private List<Node<T>> _children;

        /// <summary>
        /// Main constructor for the node. Defines its parent and value, sets its children to an empty list and generates its GUID
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="value"></param>
        public Node(Node<T> parent, T value)
        {
            _parent = parent;
            // We'll update the parent node if there is one
            if (parent != null)
            {
                parent.AddChildNode(this);
            }
            _children = new List<Node<T>>();
            _value = value;
            _id = System.Guid.NewGuid();
        }
        /// <summary>
        /// Constructor to be used if you wish to set a node's parent but not its value.
        /// </summary>
        /// <param name="parent"></param>
        public Node(Node<T> parent) : this(parent, default(T)) { }
        /// <summary>
        /// Constructor to be used if you wish to set a node's value but not its parent. Recommended for setting a root node.
        /// </summary>
        /// <param name="value"></param>
        public Node(T value) : this(null, value) { }
        /// <summary>
        /// Constructor to be used if you wish to neither set a node's parent nor value.
        /// </summary>
        public Node() : this(null, default(T)) { }

        public T Value { get => _value; set => _value = value; }
        public Guid Id { get => _id; }
        public Node<T> Parent { get => _parent; set => _parent = value; }
        public List<Node<T>> Children { get => _children; set => _children = value; }

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

        public bool Equals(Node<T> other)
        {
            return this.Id.Equals(other.Id) && this.Value.Equals(other.Value);
        }

        public void AddChildNode(Node<T> child)
        {
            // We don't want to be trying to access child if it's null
            if (child != null)
            {
                _children.Add(child);
                child.Parent = this;
            }
        }

        public void AddNodes(IEnumerable<Node<T>> nodeList)
        {
            foreach (Node<T> child in nodeList)
            {
                this.AddChildNode(child);
            }
        }

        public void RemoveChildNode(Guid id)
        {
            // Iterate over the children list
            for (int i = 0; i < Children.Count; i++)
            {
                // If the GUID matches the one provided
                if (Children[i].Id.Equals(id))
                {
                    // Remove the child's parent
                    Children[i].Parent = null;
                    // Remove the matching child in the list. Note: if for some reason there is more than one child matching this GUID, they will all get removed.
                    Children.RemoveAt(i);
                }
            }
        }

        public void RemoveChildNode(Node<T> node)
        {
            // Iterate over the children list
            for (int i = 0; i < Children.Count; i++)
            {
                // If the child is identical to the node provided
                if (Children[i].Equals(node))
                {
                    // Remove the child's parent
                    Children[i].Parent = null;
                    // Remove the matching child in the list.
                    Children.RemoveAt(i);
                }
            }
        }

        public Node<T> FindTraversing(Guid id)
        {
            // If this is the node that matches the ID, return it
            if (this.Id.Equals(id))
            {
                return this;
            }
            // else, iterate over its children until you find one that does
            for (int i = 0; i < Children.Count; i++)
            {
                // if one of the children returns something that isn't null, return it
                if (Children[i].FindTraversing(id) != null)
                {
                    return Children[i].FindTraversing(id);
                }
            }
            // if nothing was found, return null
            return null;
        }

        public Node<T> FindTraversing(Node<T> node)
        {
            // If this is the node that matches the parameter, return it
            if (this.Equals(node))
            {
                return this;
            }
            // else, iterate over its children until you find one that does
            for (int i = 0; i < Children.Count; i++)
            {
                // if one of the children returns something that isn't null, return it
                if (Children[i].FindTraversing(node) != null)
                {
                    return Children[i].FindTraversing(node);
                }
            }
            // if nothing was found, return null
            return null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Depth; i++)
            {
                sb.Append("|-");
            }
            sb.Append($"{Value} {{{Id}}}\n");
            foreach (Node<T> child in Children)
            {
                sb.Append(child.ToString());
            }
            return sb.ToString();
        }

    }
}
