using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.ViewModels
{
	public class SensorDataViewModel : INotifyPropertyChanged
	{
		private ObservableCollection<SensorData> _sensorData;

		public ObservableCollection<SensorData> SensorData
		{
			get => _sensorData;
			set
			{
				_sensorData = value;
				OnPropertyChanged();
			}
		}

		public SensorDataViewModel()
		{
			SensorData = new ObservableCollection<SensorData>();
		}

		public async Task LoadSensorDataAsync()
		{
			//var data = await SensorDataService.FetchSensorDataAsync();
			//SensorData.Clear();
			//foreach (var item in data)
			//{
			//	SensorData it = new SensorData();
			//	it.Timestamp = (System.DateTime)item.Packet.Datetime;
			//	it.Value = (double)item.Packet.Latitude;

			//	SensorData.Add(it);
			//}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}