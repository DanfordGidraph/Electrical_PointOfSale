using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electricals_PointOfSale.Models
{
    class PrintSuccess
    {
        private static PrintSuccess Instance = null;

        private PrintSuccess() { }

        public static PrintSuccess getInstance()
        {
            if (Instance == null)
            {
                Instance = new PrintSuccess();
                return Instance;
            }
            else
                return Instance;
        }

        private bool isPrintSuccessfull;
        public bool gsIsPrintSuccessfull
        {
            get { return isPrintSuccessfull; }
            set { isPrintSuccessfull = value; }
        }
    }
}
