using _4koneko;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;

namespace konUI
{
    public enum GUIObjectType
    {
        Button,
        Rectangle
    }
    public struct GUIObject
    {
        public GUIObjectType objectType;
        public int x;
        public int y;
        public int width;
        public int height;
        public string text;
        public Color color;
        public Color textColor;
        public Action callback;
    }
    public struct GUIButtonOptions
    {
        public string text;
        public bool isTextCentered;
        public Color backgroundColor;
        public Color textColor;
        public Color borderColor;
        public Action callback;
    }

    public class UI
    {
        List<GUIObject> objects = new();
        public bool isMousePressed;
        public bool isMouseReleased;
        
        public GUIObject CreateRectangle(int x, int y, int width, int height, Color color)
        {
            GUIObject obj = new GUIObject
            {
                objectType = GUIObjectType.Rectangle,
                x = x,
                y = y,
                width = width,
                height = height,
                color = color
            };
            objects.Add(obj);
            return obj;
        }

        public GUIObject CreateButton(int x, int y, int width, int height, GUIButtonOptions options)
        {
            GUIObject obj = CreateRectangle(x, y, width, height, options.backgroundColor);
            obj.objectType = GUIObjectType.Button;
            obj.text = options.text;
            obj.textColor = options.textColor;
            obj.callback = options.callback;
            objects.Add(obj);
            return obj;
        }
        private Texture2D PrimitiveCreateRectangle(GraphicsDevice graphicsDevice, GUIObject rect, Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, rect.width, rect.height);

            Color[] data = new Color[rect.width * rect.height];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = color;
            }

            texture.SetData(data);
            return texture;
        }

        public void ClearGuiObjects()
        {
            objects.Clear();
        }

        public void Update()
        {
            // check for click on any ui button
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                isMousePressed = true;
            }
            else if (mouseState.LeftButton == ButtonState.Released && isMousePressed)
            {
                isMouseReleased = true;
                isMousePressed = false;
            }
            else
            {
                isMousePressed = false;
                isMouseReleased = false;
            }
            if (isMouseReleased)
            {
                foreach (GUIObject obj in objects)
                {
                    if (obj.objectType == GUIObjectType.Button)
                    {
                        if (mouseState.X >= obj.x && mouseState.X <= obj.x + obj.width && mouseState.Y >= obj.y && mouseState.Y <= obj.y + obj.height)
                        {
                            obj.callback();
                        }
                    }
                }
            }

        }

        public void Draw(SpriteBatch sBatch, SpriteFont sFont)
        {
            sBatch.Begin();

            foreach (GUIObject obj in objects)
            {
                if (obj.objectType == GUIObjectType.Button)
                {
                    // Draw text for buttons
                    sBatch.DrawString(sFont, obj.text, new Vector2(obj.x, obj.y), Color.White);
                }
                else if (obj.objectType == GUIObjectType.Rectangle)
                {
                    sBatch.Draw(PrimitiveCreateRectangle(sBatch.GraphicsDevice, obj, obj.color), new Vector2(obj.x, obj.y), Color.White);
                }
            }

            sBatch.End();
        }
    }
}
