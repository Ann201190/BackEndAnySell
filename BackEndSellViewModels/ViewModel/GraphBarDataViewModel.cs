using System.Collections.Generic;

namespace BackEndSellViewModels.ViewModel
{
    public class GraphBarDataViewModel
    {
        public List<string> Labels { get; set; } = new List<string>();
        public List<DataSetBar> Datasets { get; set; } = new List<DataSetBar>();
    }

    public class DataSetBar
    {
        public string Label { get; set; }
        public List<double> Data { get; set; } = new List<double>();
    }
}
