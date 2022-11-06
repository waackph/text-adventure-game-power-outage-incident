using System;

namespace text_adventure

{
    public static class Game
    {
        public static bool gameLoop = true;

        public static void Start(){
            Console.Clear();
            Console.WriteLine("Vuerbaz!");
            DialogUtility.ContinueText();
        }
        public static void End(){
            Console.WriteLine("See ya!");
        }

        public static void Play(){
            Start();
            while(gameLoop){
                Menu.MenuDialog();
            }
            End();
        }
    }
}