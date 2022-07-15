using System.Collections.Generic;

namespace BackEndSellViewModels.ViewModel
{
    public class GraphDataViewModel
    {
        public List<string> Labels { get; set; } = new List<string>();
        public List<DataSet> Datasets { get; set; } = new List<DataSet>();
    }

    public class DataSet
    {
        public string Label { get; set; }
        public List<double> Data { get; set; } = new List<double>();
    }
}
