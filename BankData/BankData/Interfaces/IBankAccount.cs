using BankData.Models;
using System.ComponentModel;

namespace BankData.Interfaces
{
    public interface IBankAccount: INotifyPropertyChanged
    {
        public float Amount { get; set; }
    }
}
