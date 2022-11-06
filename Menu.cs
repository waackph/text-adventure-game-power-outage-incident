using System;
using System.Runtime.Serialization;
namespace text_adventure
{
    public static class Menu
    {
        public static bool run = false;
        public static void MenuDialog(){
            Console.Clear();
            Console.WriteLine("Choose (N)ew Game, (C)ontinue, (S)ave, (L)oad, (Q)uit");
            string input = DialogUtility.InteractionText().ToLower();
            if(input == "q"){
                Game.gameLoop = false;
            }
            else{
                run = true;
                if(input == "n"){
                    InteractionManager.InitilizeRoom();
                    while(run){
                        StoryProgress.progressListener();
                        InteractionManager.InteractionDialog();
                        StoryProgress.progressListener();
                    }
                }
                else if(input == "c"){
                    if(InteractionManager.isInitilized){
                        InteractionManager.EnterRoom();
                        while(run){
                            StoryProgress.progressListener();
                            InteractionManager.InteractionDialog();
                            StoryProgress.progressListener();
                        }
                    }
                    else{
                        Console.WriteLine("There is no state initilized. Please press (P)lay or (L)oad to initilize game");
                        DialogUtility.ContinueText();
                    }
                }
                else if(input == "s"){
                    InteractionManager.SaveState();
                }
                else if(input == "l"){
                    Console.WriteLine("Enter date and time of SaveState in Format yyyymmdd-hhmm:");
                    string saveDate = Console.ReadLine();
                    InteractionManager.LoadState(saveDate);
                    while(run){
                        StoryProgress.progressListener();
                        InteractionManager.InteractionDialog();
                        StoryProgress.progressListener();
                    }
                }
                else{
                    Console.WriteLine("Please enter one of the given Options");
                    DialogUtility.ContinueText();
                }
            }
        }

    }
}