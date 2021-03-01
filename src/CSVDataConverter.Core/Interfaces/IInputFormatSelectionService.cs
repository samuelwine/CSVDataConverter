namespace CSVDataConverter.Core.Interfaces
{
    public interface IInputFormatSelectionService
    {
        IDataFormatConverter GetSelectedInputFormat();
    }
}
