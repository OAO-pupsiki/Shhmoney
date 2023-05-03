using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shhmoney.Models
{
    public class MounthLimit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ExpenseCategoryId { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public string Currency { get; set; }
        public int Limit { get; set; }
    }
}
