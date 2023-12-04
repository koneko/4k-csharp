using _4koneko;
using konUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Xml.Linq;

namespace konUI
{

    public class GUIObject
    {
        public int x;
        public int y;
        public Color color;
    }

    public class GUIRectangle : GUIObject
    {
        public int width;
        public int height;
        public GUIRectangle(int x, int y, int width, int height, Color color)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;
            UI.Objects.Add(this);
        }
    }

    public class GUIButton : GUIObject
    {
        public string text;
        public string width;
        public string height;
        public bool isTextCentered;
        public bool hasBorder;
        public Color backgroundColor;
        public Color textColor;
        public Color borderColor;
        public GUIRectangle[] rects;
        public GUIRectangle rect;
        public event EventHandler callback;

        public GUIButton(int x, int y, int width, int height, string text, bool hasBorder, bool isTextCentered, Color backgroundColor, Color textColor, Color borderColor)
        {
            this.x = x;
            this.y = y;
            this.text = text;
            this.isTextCentered = isTextCentered;
            this.backgroundColor = backgroundColor;
            this.textColor = textColor;
            this.hasBorder = hasBorder;
            this.rect = new GUIRectangle(x, y, width, height, backgroundColor);
            if (hasBorder)
            {
                this.borderColor = borderColor;
                rects = new GUIRectangle[4];
                
            }
            UI.Objects.Add(this);
        }

        public void Invoke()
        {
            if (callback == null) return;
            callback.Invoke(this, EventArgs.Empty);
        }
    }

    public class GUIString : GUIObject
    {
        public string text;
        public bool isTextCentered;
        public GUIString(int x, int y, string text, bool isTextCentered, Color color)
        {
            this.x = x;
            this.y = y;
            this.text = text;
            this.isTextCentered = isTextCentered;
            this.color = color;
            UI.Objects.Add(this);
        }
    }

    public class UI
    {
        public static List<GUIObject> Objects = new();
        public bool isMousePressed;
        public bool isMouseReleased;

        private Texture2D PrimitiveCreateRectangle(GraphicsDevice graphicsDevice, GUIRectangle rect)
        {
            Texture2D texture = new Texture2D(graphicsDevice, rect.width, rect.height);

            Color[] data = new Color[rect.width * rect.height];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = rect.color;
            }

            texture.SetData(data);
            return texture;
        }

        public void ClearGuiObjects()
        {
            Objects.Clear();
        }

        public void Update()
        {
            // Checking for clicks
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
                foreach (GUIObject obj in Objects)
                {
                    if (obj is GUIButton button)
                    {
                        if (mouseState.X >= button.x && mouseState.X <= button.x + button.rect.width && mouseState.Y >= button.y && mouseState.Y <= button.y + button.rect.height)
                        {
                            button.Invoke();
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch sBatch, SpriteFont sFont)
        {
            sBatch.Begin();

            foreach (GUIObject obj in Objects)
            {
                if (obj is GUIButton button)
                {
                    if(button.isTextCentered)
                    {
                        int stringWidth = (int)sFont.MeasureString(button.text).X;
                        int stringHeight = (int)sFont.MeasureString(button.text).Y;
                        button.x = button.x + button.rect.width / 2 - stringWidth / 2;
                        button.y = button.y + button.rect.height / 2 - stringHeight / 2;
                    }
                    sBatch.DrawString(sFont, button.text, new Vector2(button.x, button.y), Color.White);
                }
                else if (obj is GUIRectangle rect)
                {
                    sBatch.Draw(PrimitiveCreateRectangle(sBatch.GraphicsDevice, rect), new Vector2(rect.x, rect.y), Color.White);
                }
            }
            sBatch.End();
        }
    }

}