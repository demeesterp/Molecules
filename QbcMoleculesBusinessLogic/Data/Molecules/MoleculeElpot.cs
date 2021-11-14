using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Data.Molecules
{
    public enum ElPotType
    {
        CHelgG = 1,
        GeoDisc = 2,
        Connolly = 3
    }


    public class MoleculeElpot
    {
        public int Id { get; set; }
        public int MoleculeID { get; set; }
        public int Type { get; set; }
        public decimal? PosX { get; set; }
        public decimal? PosY { get; set; }
        public decimal? PosZ { get; set; }
        public decimal? Nuclear { get; set; }
        public decimal? Electronic { get; set; }
        public decimal? Total { get; set; }
    }

}
