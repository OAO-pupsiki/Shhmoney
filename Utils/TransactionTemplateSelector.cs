using Shhmoney.Models;

namespace Shhmoney.Utils
{
    public class TransactionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ExpenseTemplate { get; set; }
        public DataTemplate IncomeTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is Expense)
            {
                return ExpenseTemplate;
            }
            else if (item is Income)
            {
                return IncomeTemplate;
            }
            else
            {
                throw new ArgumentException("Invalid item type");
            }
        }
    }
}
