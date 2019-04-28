using Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ThirdGame
{
    public class Quadtree
    {
        private int MAX_OBJECTS = 1;//10;
        private int MAX_LEVELS = 5;

        private int level;
        private List<Collider> objects;
        private Rectangle bounds;
        private Quadtree[] nodes;

        public Quadtree(int pLevel, Rectangle pBounds)
        {
            level = pLevel;
            objects = new List<Collider>();
            bounds = pBounds;
            nodes = new Quadtree[4];
        }

        public void clear()
        {
            objects.Clear();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].clear();
                    nodes[i] = null;
                }
            }
        }

        private void split()
        {
            int subWidth = bounds.Width / 2;
            int subHeight = bounds.Height / 2;
            int x = bounds.X;
            int y = bounds.Y;

            nodes[0] = new Quadtree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            nodes[1] = new Quadtree(level + 1, new Rectangle(x, y, subWidth, subHeight));
            nodes[2] = new Quadtree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            nodes[3] = new Quadtree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        private int getIndex(float X, float Y, float Width, float Height)
        {
            int index = -1;
            double verticalMidpoint = bounds.X + (bounds.Width / 2);
            double horizontalMidpoint = bounds.Y + (bounds.Height / 2);

            // Object can completely fit within the top quadrants
            var topQuadrant = (Y < horizontalMidpoint && Y + Height < horizontalMidpoint);
            // Object can completely fit within the bottom quadrants
            var bottomQuadrant = (Y > horizontalMidpoint);

            // Object can completely fit within the left quadrants
            if (X < verticalMidpoint && X + Width < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            // Object can completely fit within the right quadrants
            else if (X > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }

        public void insert(Collider collider)
        {
            if (nodes[0] != null)
            {
                int index = getIndex(collider.X, collider.Y, collider.Width, collider.Height);

                if (index != -1)
                {
                    nodes[index].insert(collider);

                    return;
                }
            }

            objects.Add(collider);

            if (objects.Count > MAX_OBJECTS && level < MAX_LEVELS)
            {
                if (nodes[0] == null)
                {
                    split();
                }

                int i = 0;
                while (i < objects.Count)
                {
                    int index = getIndex(objects[i].X, objects[i].Y, objects[i].Width, objects[i].Height);
                    if (index != -1)
                    {
                        nodes[index].insert(objects[i]);
                        objects.Remove(objects[i]);
                    }
                    else
                    {
                        i++;
                    }
                }

            }
        }

        public List<Collider> retrieve(float X, float Y, float Width, float Height, List<Collider> returnObjects = null)
        {
            if (returnObjects == null)
                returnObjects = new List<Collider>();

            int index = getIndex(X, Y, Width, Height);
            if (index != -1 && nodes[0] != null)
                nodes[index].retrieve(X, Y, Width, Height, returnObjects);

            returnObjects.AddRange(objects);

            return returnObjects;
        }

        internal void DrawDebug()
        {
            NewMethod(this);
        }

        private void NewMethod(Quadtree quad)
        {
            if (quad == null)
                return;

            foreach (var item in quad.nodes)
                NewMethod(item);

            Game1.RectanglesToRender.Enqueue(quad.bounds); 

            foreach (var obj in quad.objects)
                Game1.RectanglesToRender.Enqueue(obj.AsRectangle());
        }
    }
}
