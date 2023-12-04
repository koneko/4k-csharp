using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics.Tracing;
using _4koneko;
using konUI;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;

namespace konSceneMan
{
    public enum Scene
    {
        Main,
        Settings
    }
    class SceneManager
    {
        public static Scene currentScene;
        public static Scene nextScene;
        public static Scene previousScene;
        public void SetScene(Scene t)
        {
            nextScene = t;
        }
        public void Update()
        {
            if(nextScene != currentScene)
            {
                previousScene = currentScene;
                currentScene = nextScene;
            }
            UI ui = konGame.ui;
            switch (currentScene)
            {
                case Scene.Main:
                    ui.ClearGuiObjects();
                    GUIButton playBtn = new(200, 200, 300, 100, "Play", true, Color.Gray, Color.WhiteSmoke, Color.WhiteSmoke);
                    //playBtn.callback += (object sender, EventArgs e) =>
                    //{;

                    //};
                    break;
                case Scene.Settings:
                    break;
            }
        }
    }
}
