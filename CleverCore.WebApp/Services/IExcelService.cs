using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CleverCore.Application.ViewModels.Product;

namespace CleverCore.WebApp.Services
{
    public interface IExcelService
    {
        void WriteExcel(FileInfo file, List<ProductViewModel> products);
    }
}
