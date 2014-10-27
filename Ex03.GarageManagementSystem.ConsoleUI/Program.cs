namespace Ex03.GarageManagementSystem.ConsoleUI
{
     using System;
     using System.Collections.Generic;
     using System.Text;
   
     public class Program
     {
          private static void Main()
          {
               Ex03.GarageLogic.GarageManager garageManager = new Ex03.GarageLogic.GarageManager();               
               do
               {
                    Interface.eMainMenuEnum userMainMenuSelection = Interface.ShowMenuAndGetUserInput();                
                    Interface.PerformUserSelection(userMainMenuSelection, garageManager);
                    if (userMainMenuSelection == Interface.eMainMenuEnum.ExitMenu)
                    {
                         break;
                    }
               }
               while (true);               
          }
     }
}
