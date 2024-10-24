using BankData.BankData.Exceptions;
using BankData.Interfaces;
using BankData.Logger;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;

namespace BankData.Models
{
    public class BankClients : IStorage<IBankClient>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public Action OnStorageReady { get; set; }
        private int _lastId = 0;
        private ObservableCollection<IBankClient> _dataCollecton;
        public ReadOnlyObservableCollection<IBankClient> DataCollection { 
            get 
            {
                return new ReadOnlyObservableCollection<IBankClient>(_dataCollecton);
            }
        }

        private static string _fileName = "users.dat";
        private FileStorage _fileStorage;

        public BankClients()
        {
            _dataCollecton = new ObservableCollection<IBankClient>();
            _fileStorage = new FileStorage(_fileName);
            _fileStorage.OnFileLoaded += OnUsersFileLoaded;
        }

        public void Init()
        {
            _fileStorage.LoadFile();
        }

        public void AddItem(IBankClient client)
        {
            if (client.ID == -1) client.ID = _lastId++;
            else if (client.ID >= _lastId) _lastId = client.ID + 1;
            _dataCollecton.Add(client);
        }

        public void RemoveItem(IBankClient client)
        {
            _dataCollecton.Remove(client);
        }

        public void TransferMoney(IBankClient destinationClient, BankAccount destinationAccount, IBankClient sourceClient, BankAccount sourceAccount, float amount)
        {
            try
            {
                float count1 = destinationAccount - amount;
                float count2 = sourceAccount + amount;
            }
            catch (TransactionException e)
            {
                Trace.TraceError("Error transfer money");
            }

            Save();
        }

        public void Save()
        {
            List<string> usersStrings = new List<string>();
            foreach (IBankClient bankClient in DataCollection)
                usersStrings.Add(JsonSerializer.Serialize(bankClient));
            _fileStorage.SaveFile(usersStrings);
        }

        private void OnUsersFileLoaded(List<string> usersStrings)
        {
            foreach (string userString in usersStrings)
                AddItem(JsonSerializer.Deserialize<BankClient>(userString));
            OnStorageReady?.Invoke();
        }
    }
}
