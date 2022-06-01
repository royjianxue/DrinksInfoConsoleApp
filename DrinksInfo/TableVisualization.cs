using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;

namespace DrinksInfo
{
    public class TableVisualization
    {
        //using https://github.com/minhhungit/ConsoleTableExt package for table presentation
        public static void ShowTable(List<Category> tableData)
        {
            Console.WriteLine("\n");
            ConsoleTableBuilder
            .From(tableData)
            .WithTitle("Drink Menu")
            .ExportAndWriteLine(TableAligntment.Left);
            Console.WriteLine("");
        }
    }
}
