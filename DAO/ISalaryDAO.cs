using fullstackCsharp.Models;
using fullstackCsharp.Models.ViewModel.Salaries;
using NuGet.Protocol.Core.Types;
using System.Drawing.Printing;

namespace fullstackCsharp.DAO
{
    public interface ISalaryDAO
    {
        public IEnumerable<TotalSalaryViewModel> GettotalSalaryViewModels(int? month, int? year);

        public IEnumerable<TotalSalaryViewModel> GetsalarySelf(SearchModel search, int idu);
    }
}
