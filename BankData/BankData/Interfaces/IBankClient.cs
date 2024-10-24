﻿using BankData.Models;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace BankData.Interfaces;
public interface IBankClient : ISerializable, INotifyPropertyChanged
{
    public int ID { get; set; }
    public string FirstName { get; }
    public string LastName { get; }
    public string ThirdName { get; }
    public BankAccount Account { get; }
    public BankAccount DepositAccount { get; }

    bool TransferMoney(IBankAccount account, int amount);
    bool ReceiveMoney(IBankAccount account, int amount);

}

