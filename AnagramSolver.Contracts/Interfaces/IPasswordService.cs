namespace AnagramSolver.Contracts.Interfaces
{
    public interface IPasswordService
    {
        public byte[] GetHash(string inputString);
        public string GetHashString(string inputString);

    }
}
