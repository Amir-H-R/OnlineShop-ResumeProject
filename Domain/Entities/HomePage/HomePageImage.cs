using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.HomePage
{
    public class HomePageImage:BaseEntity
    {
        public string Src{ get; set; }
        public string Link{ get; set; }
        public ImageLocation ImageLocation{ get; set; }
    }

    public enum ImageLocation
    {
        L1 = 0,
        R1 = 1,
        G1 = 2,
        G2 = 3,
        G3 = 4,
        G4 = 5,
        G5 = 6,
        G6 = 7,
    }
}
