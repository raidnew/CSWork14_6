using BankData.BankData.Exceptions;
using BankData.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankData.Models
{
    public class BankAccount : IBankAccount, ISerializable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private float _amount;
        public float Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        public BankAccount() : this(0) { }

        public BankAccount(float amount)
        {
            Amount = amount;
        }

        public BankAccount(SerializationInfo info, StreamingContext context)
        {
            Amount = (float)info.GetValue(nameof(Amount), typeof(float));
        }

        public static float operator + (BankAccount current, float count)
        {
            current.Amount += count;
            return current.Amount;
        }

        public static float operator - (BankAccount current, float count)
        {
            if (current.Amount < count) throw new TransactionException("Transfer error. No money.");
            current.Amount -= count;
            return current.Amount;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Amount), Amount);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
