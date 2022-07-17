using System.Collections.Generic;

namespace BackEndSellViewModels.ViewModel
{
    public class GraphLineDataViewModel
    {
        public List<string> Labels { get; set; } = new List<string>();
        public List<DataSetLine> Datasets { get; set; } = new List<DataSetLine>();
    }

    public class DataSetLine: DataSetBar
    {
        public double Tension { get; set; }
    }
}
