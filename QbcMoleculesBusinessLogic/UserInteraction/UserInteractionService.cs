﻿using QbcMoleculesBusinessLogic.Data.DataFiles;
using QbcMoleculesBusinessLogic.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.UserInteraction
{
    public class UserInteractionService : IUserInteractionService
    {
        #region dependencies

        private IBasissetInfoRepo BasissetInfoRepo { get; }

        #endregion

        public UserInteractionService(IBasissetInfoRepo basissetInfoRepo)
        {
            BasissetInfoRepo = basissetInfoRepo;
        }

        public BasisSet? SelectBasisSet()
        {
            BasisSet? retval = null;
            List<BasisSet> basisSets = BasissetInfoRepo.GetBasisSetInfo();
            int selectedItem = basisSets.Count + 1;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose a basisSet:");
                for (int count = 0; count < basisSets.Count; count++)
                {
                    Console.WriteLine($"{count + 1}) {basisSets[count].Name}");
                }
                Console.WriteLine($"{basisSets.Count+1}) to Exit");
                Console.Write("\r\n Select an option: ");

            }
            while (!int.TryParse(Console.ReadLine(), out selectedItem));


            if (selectedItem >= basisSets.Count + 1)
            {
                Console.WriteLine($"Option {selectedItem} selected : exiting");
            }
            else
            {
                Console.WriteLine($"Option {selectedItem} selected : {basisSets[selectedItem-1].Name}");
                retval = basisSets[selectedItem - 1];
            }
            return retval;
        }


        public bool NeedGeoOpt()
        {
            bool retval = false;
            string[] replies = { "yes", "no" ,"y", "n"};
            string? reply = "";
            do
            {
                Console.Clear();
                Console.WriteLine("Do geoopt before ?");               
                Console.Write("\r\n (Y)es/(N)o: ");
                reply = Console.ReadLine();
                Console.WriteLine($"\r\n Response: {reply}");
            }
            while (!replies.Contains(reply?.ToLower()));

            if ( reply == "yes" || reply == "y")
            {
                retval = true;
            }

            return retval;
        }
    }
}
