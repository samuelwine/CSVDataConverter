namespace CSVDataConverter.Core.Interfaces
{
    public interface IOutputFormatSelectionService
    {
        IDataFormatConverter GetSelectedOutputFormat();
    }
}
