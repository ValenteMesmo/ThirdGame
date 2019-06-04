using Common;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ThirdGame
{
    public class QuadTree
    {
        private readonly Node Root;

        public QuadTree(Rectangle bounds, int maxDepth, int maxChildren)
        {
            var Node = new Node(bounds, 0, maxDepth, maxChildren);
            Root = Node;
        }

        public void Add(Collider item)
        {
            Root.Add(item);
        }

        public void AddRange(IEnumerable<Collider> itens)
        {
            foreach (var item in itens)
                Root.Add(item);
        }

        public Collider[] Get(Collider item)
        {
            return Root.Get(item);
        }

        public void Clear()
        {
            Root.Clear();
        }

        internal void DrawDebug()
        {
            Root.DrawDebug();
        }
    }

    public class Node
    {
        private readonly Rectangle Bounds;
        private readonly int Depth;
        private readonly int MaxDepth;
        private readonly int MaxChildren;
        private readonly List<Node> Nodes;
        private readonly List<Collider> Children;
        private readonly List<Collider> StuckChildren;
        private const int TOP_LEFT = 0;
        private const int TOP_RIGHT = 1;
        private const int BOTTOM_LEFT = 2;
        private const int BOTTOM_RIGHT = 3;

        public Node(Rectangle Bounds, int Depth, int MaxDepth, int MaxChildren)
        {
            this.Bounds = Bounds;
            this.Depth = Depth;
            this.MaxDepth = MaxDepth;
            this.MaxChildren = MaxChildren;
            Nodes = new List<Node>();
            Children = new List<Collider>();
            StuckChildren = new List<Collider>();
        }

        public void Add(Collider item)
        {
            if (Nodes.Any())
            {
                var index = FindIndex(item);
                var node = Nodes[index];

                if (item.RelativeX() >= node.Bounds.X
                    && item.RelativeX() + item.Width <= node.Bounds.X + node.Bounds.Width
                    && item.RelativeY() >= node.Bounds.Y
                    && item.RelativeY() + item.Height <= node.Bounds.Y + node.Bounds.Height)
                    Nodes[index].Add(item);
                else
                    StuckChildren.Add(item);

                return;
            }

            Children.Add(item);

            if (Depth < MaxDepth && Children.Count > MaxChildren)
            {
                Subdivide();

                for (int i = 0; i < Children.Count; i++)
                    Add(Children[i]);

                Children.Clear();
            }
        }

        public void Clear()
        {
            StuckChildren.Clear();
            Children.Clear();

            var len = Nodes.Count;

            if (!Nodes.Any())
                return;

            for (var i = 0; i < len; i++)
                Nodes[i].Clear();

            Nodes.Clear();
        }

        private Collider[] GetAllContent(List<Collider> Out = null)
        {
            if (Out == null)
                Out = new List<Collider>();

            if (Nodes.Any())
            {
                for (var i = 0; i < Nodes.Count; i++)
                {
                    Nodes[i].GetAllContent(Out);
                }
            }
            Out.AddRange(StuckChildren);
            Out.AddRange(Children);

            return Out.ToArray();
        }

        public Collider[] Get(Collider item)
        {
            var Out = new List<Collider>();

            if (Nodes.Any())
            {
                var index = FindIndex(item);
                var node = Nodes[index];

                if (item.RelativeX() >= node.Bounds.X
                    && item.RelativeX() + item.Width <= node.Bounds.X + node.Bounds.Width
                    && item.RelativeY() >= node.Bounds.Y
                    && item.RelativeY() + item.Height <= node.Bounds.Y + node.Bounds.Height)
                {
                    Out.AddRange(Nodes[index].Get(item));
                }
                else
                {
                    //Part of the item are overlapping multiple child nodes. For each of the overlapping nodes, return all containing objects.
                    if (item.RelativeX() <= Nodes[TOP_RIGHT].Bounds.X)
                    {
                        if (item.RelativeY() <= Nodes[BOTTOM_LEFT].Bounds.Y)
                            Out.AddRange(Nodes[TOP_LEFT].GetAllContent());

                        if (item.RelativeY() + item.Height > Nodes[BOTTOM_LEFT].Bounds.Y)
                            Out.AddRange(Nodes[BOTTOM_LEFT].GetAllContent());
                    }

                    if (item.RelativeX() + item.Width > Nodes[TOP_RIGHT].Bounds.X)
                    {//position+width bigger than middle x
                        if (item.RelativeY() <= Nodes[BOTTOM_RIGHT].Bounds.Y)
                            Out.AddRange(Nodes[TOP_RIGHT].GetAllContent());

                        if (item.RelativeY() + item.Height > Nodes[BOTTOM_RIGHT].Bounds.Y)
                            Out.AddRange(Nodes[BOTTOM_RIGHT].GetAllContent());
                    }
                }
            }

            Out.AddRange(StuckChildren);
            Out.AddRange(Children);

            return Out.ToArray();
        }

        private int FindIndex(Collider item)
        {
            var b = Bounds;
            var left = (item.RelativeX() > b.X + b.Width / 2) ? false : true;
            var top = (item.RelativeY() > b.Y + b.Height / 2) ? false : true;

            //top left
            var index = TOP_LEFT;
            if (left)
            {
                //left side
                if (!top)
                {
                    //bottom left
                    index = BOTTOM_LEFT;
                }
            }
            else
            {
                //right side
                if (top)
                {
                    //top right
                    index = TOP_RIGHT;
                }
                else
                {
                    //bottom right
                    index = BOTTOM_RIGHT;
                }
            }

            return index;
        }

        private void Subdivide()
        {
            var depth = Depth + 1;

            var bx = Bounds.X;
            var by = Bounds.Y;

            //floor the values
            var b_w_h = (Bounds.Width / 2); //todo: Math.floor?
            var b_h_h = (Bounds.Height / 2);
            var bx_b_w_h = bx + b_w_h;
            var by_b_h_h = by + b_h_h;

            //TOP_LEFT
            Nodes.Add(new Node(
                new Rectangle(bx, by, b_w_h, b_h_h)
                , depth
                , MaxDepth
                , MaxChildren
            ));

            //TOP_RIGHT
            Nodes.Add(new Node(
               new Rectangle(
                    bx_b_w_h
                    , by
                    , b_w_h
                    , b_h_h
                )
                , depth
                , MaxDepth
                , MaxChildren
            ));

            //BOTTOM_LEFT
            Nodes.Add(new Node(
               new Rectangle(
                    bx
                    , by_b_h_h
                    , b_w_h
                    , b_h_h
                ),
                depth
                , MaxDepth
                , MaxChildren
   ));

            //BOTTOM_RIGHT            
            Nodes.Add(new Node(
               new Rectangle(
                    bx_b_w_h
                    , by_b_h_h
                    , b_w_h
                    , b_h_h
                )
                , depth
                , MaxDepth
                , MaxChildren
   ));
        }

        internal void DrawDebug()
        {
            Game1.RectanglesToRender.Enqueue(Bounds);

            for (int i = 0; i < Nodes.Count; i++)
                Nodes[i].DrawDebug();

            var currentContent = GetAllContent();

            for (int i = 0; i < currentContent.Length; i++)
                Game1.RectanglesToRender.Enqueue(currentContent[i].AsRectangle());
        }
    }
}
