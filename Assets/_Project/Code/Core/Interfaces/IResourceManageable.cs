namespace Trivainia
{
    public interface IResourceManageable
    {
        public event System.Action Depleted;
        public void Reset();
        public void Deplete(float amount);
        public void Restore(float amount);
    }
}