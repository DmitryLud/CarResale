using CarResale.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Doc;
using System.IO;

namespace CarResale.WordHelper
{
    class Contract
    {
        public static void ReplaceText(Order order)
        {
            Document document = new Document(Directory.GetCurrentDirectory() + @"\contract.docx").Clone();

            document.Replace("<day>", DateTime.Now.Day.ToString(), false, true);
            document.Replace("<month>", DateTime.Now.Month.ToString(), false, true);
            document.Replace("<year>", DateTime.Now.Year.ToString(), false, true);
            document.Replace("<clientSNP>", order.Customer.Surname + " " + order.Customer.Name, false, true);
            document.Replace("<mark>", order.Car.Model.Mark.Name, false, true);
            document.Replace("<model>", order.Car.Model.Name, false, true);
            document.Replace("<carYear>", order.Car.Year.ToString(), false, true);
            document.Replace("<VIN>", order.Car.VIN, false, true);
            document.Replace("<price>", order.Sale_price.ToString(), false, true);

            document.SaveToFile(@"C:\Users\Asus\Desktop\contract.docx", FileFormat.Docx);
            System.Diagnostics.Process.Start(@"C:\Users\Asus\Desktop\contract.docx");
        }
    }
}
