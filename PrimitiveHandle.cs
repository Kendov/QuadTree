using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.VectorDraw;

namespace QuadTree
{
    public static class PrimitiveHandle
    {
        private static PrimitiveDrawing _primitiveDrawing;
        private static PrimitiveBatch _primitiveBatch;
        private static GraphicsDevice _graphicsDevice;
        private static Matrix _localProjection;
        private static Matrix _localView;

        public static bool Initialized { get; private set; }

        private static bool _drawing;
        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            _drawing = false;
            _graphicsDevice = graphicsDevice;
            _localProjection = Matrix.CreateOrthographicOffCenter(0f, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height, 0f, 0f, 1f);
            _localView = Matrix.Identity;
            if (_primitiveBatch == null)
                _primitiveBatch = new PrimitiveBatch(graphicsDevice);
            if (_primitiveDrawing == null)
                _primitiveDrawing = new PrimitiveDrawing(_primitiveBatch);

            Initialized = true;
        }

        public static PrimitiveDrawing Drawer
        {
            get
            {
                CheckInitialization();

                if (!_drawing)
                    _primitiveBatch.Begin(ref _localProjection, ref _localView);

                _drawing = true;
                return _primitiveDrawing;
            }
        }

        public static void Draw()
        {
            CheckInitialization();
            _primitiveBatch.End();
            _drawing = false;
        }

        private static void CheckInitialization()
        {
            if (!Initialized)
                throw new Exception("Primitive Handle is not Initialized");
        }

    }
}