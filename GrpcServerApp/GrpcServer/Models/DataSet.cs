namespace GrpcServer.Models
{
    public class DataSet
    {
        private static readonly Lazy<DataSet> lazy = new Lazy<DataSet>(() => new DataSet());

        public static DataSet Instance { get { return lazy.Value; } }

        public int Count { get; set; }
    }
}
