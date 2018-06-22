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

        /// <summary>
        /// Main constructor for the node. Defines its parent and value, sets its children to an empty list and generates its GUID
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="value"></param>
        public Node(Node parent, string value)
        {
            _parent = parent;
            // We'll update the parent node if there is one
            if (parent != null)
            {
                parent.AddChildNode(this);
            }
            _children = new List<Node>();
            _value = value;
            _id = System.Guid.NewGuid();
        }
        /// <summary>
        /// Constructor to be used if you wish to set a node's parent but not its value.
        /// </summary>
        /// <param name="parent"></param>
        public Node(Node parent) : this(parent, String.Empty) { }
        /// <summary>
        /// Constructor to be used if you wish to set a node's value but not its parent. Recommended for setting a root node.
        /// </summary>
        /// <param name="value"></param>
        public Node(string value) : this(null, value) { }
        /// <summary>
        /// Constructor to be used if you wish to neither set a node's parent nor value.
        /// </summary>
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

        public void AddChildNode(Node child)
        {
            // We don't want to be trying to access child if it's null
            if (child != null)
            {
                _children.Add(child);
                child.Parent = this;
            }
        }

        public void AddNodes(IEnumerable<Node> nodeList)
        {
            foreach (Node child in nodeList)
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

        public void RemoveChildNode(Node node)
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

        public Node FindTraversing(Guid id)
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

        public Node FindTraversing(Node node)
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


    }
}
