using System;


namespace ILearnSmartProject.Interfaces
{
    public interface IObserver
    {

        public  Task Notify(string message, string emailAddress);


    }

}
