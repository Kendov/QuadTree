using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended.VectorDraw;

namespace QuadTree
{
    public class QuadTree
    {
        public List<Point> Points { get; private set; }
        public Rectangle Boundary { get; set; }
        public int Capacity { get; set; }

        private QuadTree TopLeft;
        private QuadTree TopRight;
        private QuadTree BotLeft;
        private QuadTree BotRight;
        private bool subdivided;

        public QuadTree(Rectangle boundary, int capacity)
        {
            Boundary = boundary;
            Capacity = capacity;
            Points = new List<Point>();
            subdivided = false;
        }

        public void Subdivide()
        {
            float x = Boundary.Position.X;
            float y = Boundary.Position.Y;
            float width = Boundary.Width / 2f;
            float height = Boundary.Height / 2f;

            TopLeft = new QuadTree(new Rectangle(new Vector2(x - width / 2f, y + height / 2f), width, height), Capacity);
            TopRight = new QuadTree(new Rectangle(new Vector2(x + width / 2f, y + height / 2f), width, height), Capacity);
            BotLeft = new QuadTree(new Rectangle(new Vector2(x - width / 2f, y - height / 2f), width, height), Capacity);
            BotRight = new QuadTree(new Rectangle(new Vector2(x + width / 2f, y - height / 2f), width, height), Capacity);
            subdivided = true;
        }

        public void Insert(Point point)
        {
            if (!Boundary.IsInside(point.Position))
                return;

            if (Points.Count < Capacity)
            {
                Points.Add(point);
            }
            else
            {
                if (!subdivided)
                {
                    Subdivide();
                }

                TopLeft.Insert(point);
                TopRight.Insert(point);
                BotLeft.Insert(point);
                BotRight.Insert(point);

            }

        }

        public void Draw()
        {
            var drawPosition = new Vector2(Boundary.Position.X - Boundary.Width / 2, Boundary.Position.Y - Boundary.Height / 2);
            PrimitiveHandle.Drawer.DrawRectangle(drawPosition, Boundary.Width, Boundary.Height, Color.Green);
            foreach (var item in Points)
            {
                PrimitiveHandle.Drawer.DrawSolidCircle(item.Position, 1f, Color.White);
            }
            if (subdivided)
            {
                TopLeft.Draw();
                TopRight.Draw();
                BotLeft.Draw();
                BotRight.Draw();
            }
        }

    }

    public class Point
    {
        public Vector2 Position { get; set; }

        public Point(Vector2 position)
        {
            Position = position;
        }
    }

    public class Rectangle
    {
        public Rectangle(Vector2 position, float width, float height)
        {
            Position = position;
            Width = width;
            Height = height;
        }

        public Vector2 Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public bool IsInside(Vector2 point)
        {
            return
                point.X > Position.X - Width / 2 &&
                point.X < Position.X + Width / 2 &&
                point.Y > Position.Y - Height / 2 &&
                point.Y < Position.Y + Height / 2;
        }
    }


}