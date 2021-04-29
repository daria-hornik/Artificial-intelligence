using System.Collections.Generic;

namespace Lab03.AlOperators
{
    class Node
    {
        public Node Parent { get; set; }
        public Board Value { get; set; }
        public List<Node> Children { get; set; }
        public int SelectedMoveIndex { get; set; }

        public Node(Node parent, Board value)
        {
            Parent = parent;
            Value = value;
            Children = new List<Node>();
        }

        public Node(Board value)
        {
            Parent = null;
            Value = value;
            Children = new List<Node>();
        }

        public void AddChild(Node kid)
        {
            Children.Add(kid);
        }

        public void BuildNextLevel(List<int> allPossibleMove, bool isOpponent)
        {
            foreach (var node in allPossibleMove)
            {
                Board kidBoard = (Board) Value.Clone();
                kidBoard.Sow(node, isOpponent);
                Node kid = new Node(kidBoard);
                kid.Parent = this;
                kid.SelectedMoveIndex = node;
                AddChild(kid);
            }
        }
    }
}
