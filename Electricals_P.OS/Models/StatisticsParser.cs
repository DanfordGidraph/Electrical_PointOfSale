using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Electricals_PointOfSale.Models
{
    class StatisticsParser
    {
        private static StatisticsParser Instance = null;
        private  string[] itemKeys;
        private  double[] itemValues;
        List<KeyValuePair<string, double>> keyValuePairArray;


        private StatisticsParser()
        {
            keyValuePairArray = new List<KeyValuePair<string, double>>(itemValues.Length);
        }

        public static StatisticsParser getInstance()
        {
            if (Instance == null)
            {
                return Instance = new StatisticsParser();
            }
            else
                return Instance;
        }

        public string[] gsItemKeys
        {
            get { return itemKeys; }
            set { itemKeys = value; }
        }
        public double[] gsItemValues
        {
            get { return itemValues; }
            set { itemValues = value; }
        }

        
       public KeyValuePair<string , double>[] createKeyValuePair()
        {
            for(int i = 0; i < itemKeys.Length; i++)
            {
                if(itemValues[i] == 0)
                {
                    itemKeys[i] = "";
                }
                MessageBox.Show(itemKeys[i]);
                keyValuePairArray.Insert(i, new KeyValuePair<string, double>(itemKeys[i],itemValues[i]));
            }
            return keyValuePairArray.ToArray();
        }       

    }
}
