namespace AnagramSolver.Contracts.Interfaces
{
    public interface IPersistentRepository
    {
        public void PopulateDataBaseFromFile();
        public string ReadSetting(string key);
    }
}
