using System;
using System.Collections.Generic;
using System.Text;

namespace Isen.AntoineBerenguer.Library
{
    interface INode : IEquatable<Node>
    {
        string Value { get; set; }
        Guid Id { get; }
        Node Parent { get; set; }
        List<Node> Children { get; set; }
        int Depth { get; }
    }
}
