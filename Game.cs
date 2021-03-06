using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QuadTree
{
    public class Source : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Rectangle _boundary;
        private QuadTree _quadtree;
        public int _width;
        public int _height;

        private TimeSpan placementCD = new TimeSpan();

        public Source()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _width = GraphicsDevice.Viewport.Width;
            _height = GraphicsDevice.Viewport.Height;
            _boundary = new Rectangle(new Vector2(_width / 2, _height / 2), _width, _width);
            _quadtree = new QuadTree(_boundary, 4);
            PrimitiveHandle.Initialize(_graphics.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && placementCD <= new TimeSpan())
            {
                System.Console.WriteLine(Mouse.GetState().Position);
                _quadtree.Insert(new Point(Mouse.GetState().Position.ToVector2()));
                placementCD = new TimeSpan(0, 0, 0, 0, 5);
            }

            placementCD -= gameTime.ElapsedGameTime;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _quadtree.Draw();
            PrimitiveHandle.Draw();


            base.Draw(gameTime);
        }
    }
}
