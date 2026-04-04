using ILearnSmartProject.Interfaces;

namespace ILearnSmartProject.Models
{
    public class SubjectClass
    {
 
        private List<IObserver> observers = new List<IObserver>();
        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }
        public void NotifyObservers(string message, string emailAddress)
        {
            foreach (var observer in observers)
            {
                observer.Notify(message,emailAddress);
            }
        }
         

    }
}
