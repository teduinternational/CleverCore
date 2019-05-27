using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CleverCore.Application.ViewModels.Product;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace CleverCore.WebApp.Services
{
    public class ExcelService : IExcelService
    {
        public void WriteExcel(FileInfo file,List<ProductViewModel> products)
        {
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Products");
                worksheet.Cells["A1"].LoadFromCollection(products, true, TableStyles.Light1);
                worksheet.Cells.AutoFitColumns();
                package.Save(); //Save the workbook.
            }
        }
    }
}
