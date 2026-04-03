namespace ILearnSmartProject.Models
{
    public interface ISubscriber
    {

        // notify the subscriber of the update
        public void Notify(string message);
    }
}
