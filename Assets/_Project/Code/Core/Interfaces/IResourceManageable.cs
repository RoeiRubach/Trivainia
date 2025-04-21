namespace Trivainia
{
    public interface IResourceManageable
    {
        public void Reset();
        public void Deplete(float amount);
        public void Restore(float amount);
    }
}