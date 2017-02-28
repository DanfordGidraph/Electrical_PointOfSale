using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace Electricals_PointOfSale.Models
{
    class Printer
    {
        private string newPrinterName;
        private double total;
        private double balance;
        private double paid;
       

        public string[] itemNames;
        public string[] itemQtys;
        public string[] itemPrices;
       


     
        public string gsPrntrName
        {
            get { return newPrinterName; }
            set { newPrinterName = value; }
        }
        public double gsTotal
        {
            get { return total; }
            set { total = value; }
        }
        public double gsBalance
        {
            get { return balance; }
            set { balance = value; }
        }
        public double gsPaid
        {
            get { return paid; }
            set { paid = value; }
        }
    



        public void callPrintReceipt()
        {
            if (newPrinterName == string.Empty)
            {
                printWithDialog(ref newPrinterName);

            }
            else
                printWithoutDialog(newPrinterName);


        }

        private void printWithoutDialog(string printerName)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += printReceipt;
            PrinterSettings PrintSett = new PrinterSettings();
            PrintSett.PrinterName = printerName;
            printDoc.PrinterSettings = PrintSett;
            printDoc.Print();
        }
        private void printWithDialog(ref string printerName)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDoc = new PrintDocument();
            printDialog.Document = printDoc;

            printDoc.PrintPage += printReceipt;

            DialogResult result = printDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                printDoc.Print();
                gsPrntrName = printDoc.PrinterSettings.PrinterName;
            }
        }
        private void printReceipt(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font smallFont = new Font("Courier New", 7);
            Font mediumFont = new Font("Courier New", 7, FontStyle.Underline);
            Font largeFont = new Font("Courier New", 7, FontStyle.Bold);

            SolidBrush blackBrush = new SolidBrush(Color.Black);
            float fontHeight = smallFont.GetHeight();
            StringFormat formater = new StringFormat();
            formater.Alignment = StringAlignment.Near;
            formater.LineAlignment = StringAlignment.Near;

            int startX = 5;
            int startY = 5;
            float offSet = 30;

            graphics.DrawString("Welcome to Trinity Electricals", largeFont, blackBrush, startX, startY);

            graphics.DrawString("ITEM".PadRight(13) + "QTY".PadRight(5) + "PRICE".PadRight(6) + "AMOUNT".PadLeft(6), mediumFont, blackBrush, startX, startY + fontHeight + 10, formater);

            for (int i = 0; i < itemNames.Length; i++)
            {
                string name = itemNames[i];
                string qty = itemQtys[i];
                string price = (double.Parse(itemPrices[i]) / double.Parse(itemQtys[i])).ToString();
                string Amount = itemPrices[i];

                if (name == string.Empty)
                {
                    break;
                }
                string fullReceiptLine = name.PadRight(13, '.') + (qty + " X").PadRight(5) + " " + price.PadRight(6);
                string amountReceiptLine = ("Ksh. " + Amount).PadLeft(30);
                graphics.DrawString(fullReceiptLine, smallFont, blackBrush, startX, startY + offSet, formater);
                offSet = offSet + fontHeight + 3;
                graphics.DrawString(amountReceiptLine, smallFont, blackBrush, startX, startY + offSet, formater);
                offSet = offSet + fontHeight + 3;

                StockUpdater.getInstance().updateStock(name, qty);
            }

            double vat = total * .16;

            offSet = offSet + 20;
            graphics.DrawString("Sub Total".PadRight(20, '_') + ("Ksh. " + total.ToString()).PadLeft(10,'_'), mediumFont, blackBrush, startX, startY + offSet);
            offSet = offSet + 10;
            graphics.DrawString("Paid".PadRight(20, '_') + ("Ksh. " + paid.ToString()).PadLeft(10, '_'), mediumFont, blackBrush, startX, startY + offSet);
            offSet = offSet + 10;
            graphics.DrawString("V.A.T".PadRight(20, '_') + ("Ksh. " + vat.ToString()).PadLeft(10, '_'), mediumFont, blackBrush, startX, startY + offSet);
            offSet = offSet + 10;
            graphics.DrawString("Balance".PadRight(20, '_') + ("Ksh. "+ balance.ToString()).PadLeft(10, '_'), mediumFont, blackBrush, startX, startY + offSet);
            offSet = offSet + 10;
            graphics.DrawString("Thank You For Shopping With Us", mediumFont, blackBrush, startX, startY + offSet);
            offSet = offSet + 10;
            graphics.DrawString("Served By: "+ UserAccessLevel.getInstance().gsCurrentUserName + " ", mediumFont, blackBrush, startX, startY + offSet);
            offSet = offSet + 20;
            graphics.DrawString("Date: " + System.DateTime.Now.ToLongDateString(), mediumFont, blackBrush, startX, startY + offSet,formater);

            PrintSuccess.getInstance().gsIsPrintSuccessfull = true;
        }

       // private void saveReceipt()
       // {
       //     FileStream fs = null;

       // }
    }
    }
