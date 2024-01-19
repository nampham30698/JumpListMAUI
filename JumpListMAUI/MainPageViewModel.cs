using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace JumpListMAUI
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<GroupMyModel> dataCollection;

        public List<string> AlphabetList;

        public async Task InitialSetup()
        {
            AlphabetList = new();

            var data = await GetDataFromApi();

            var sorted = from item in data
                         orderby item.Name
                         group item by item.Name[0].ToString().ToUpperInvariant() into itemGroup
                         select itemGroup;

            var dataGroup = new List<MyModel>();

            foreach (var item in sorted)
            {
                AlphabetList.Add(item.Key);

                dataGroup.Add(new MyModel()
                {
                    Name = item.Key,
                    IsGrouped = true,
                    IsItem = false
                });

                dataGroup.AddRange(item.ToList());
            }

            var collection = new ObservableCollection<GroupMyModel>()
            {
                new GroupMyModel("Categories", dataGroup)
            };

            DataCollection = collection;
        }

        private async Task<IEnumerable<MyModel>> GetDataFromApi()
        {
            // get your data here.

            await Task.Delay(10);

            var data = new List<MyModel>()
            {
                new MyModel(){ Code = "123", Count = 123, Name = "Biofuel"},
                new MyModel(){ Code = "123", Count = 123, Name = "CO2 Allowance"},
                new MyModel(){ Code = "123", Count = 123, Name = "Coal"},
                new MyModel(){ Code = "123", Count = 123, Name = "Crude Oil"},
                new MyModel(){ Code = "123", Count = 123, Name = "Diesel"},
                new MyModel(){ Code = "123", Count = 123, Name = "Duties"},
                new MyModel(){ Code = "123", Count = 123, Name = "Electricty"},
                new MyModel(){ Code = "123", Count = 123, Name = "Freight"},
                new MyModel(){ Code = "123", Count = 123, Name = "Fuel Oil"},
                new MyModel(){ Code = "123", Count = 123, Name = "Gas Oil"},
                new MyModel(){ Code = "123", Count = 123, Name = "Gas Oil"},
                new MyModel(){ Code = "123", Count = 123, Name = "Heavy Fuel"},
                new MyModel(){ Code = "123", Count = 123, Name = "Iol Fuel"},
                new MyModel(){ Code = "123", Count = 123, Name = "Jet Fuel"},
                new MyModel(){ Code = "123", Count = 123, Name = "Kerosone"},
                new MyModel(){ Code = "123", Count = 123, Name = "LGP"},
                new MyModel(){ Code = "123", Count = 123, Name = "Naphtha"},
                new MyModel(){ Code = "123", Count = 123, Name = "Rail"},
                new MyModel(){ Code = "123", Count = 123, Name = "Road"},
                new MyModel(){ Code = "123", Count = 123, Name = "Shipping"},
                new MyModel(){ Code = "123", Count = 123, Name = "Teamperature"},
                new MyModel(){ Code = "123", Count = 123, Name = "Hola"},
            };

            return data;
        }
    }

    public class GroupMyModel : ObservableCollection<MyModel>
    {
        public string GroupHeaderName { get; set; }
        public GroupMyModel(string groupHeaderName, IEnumerable<MyModel> children) : base(children)
        {
            GroupHeaderName = groupHeaderName;
        }
    }

    public class MyModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public bool IsGrouped { get; set; } = false;
        public bool IsItem { get; set; } = true;
    }
}
