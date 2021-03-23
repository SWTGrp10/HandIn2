using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public class RFIDReaderEventArgs : EventArgs
    {
        public RFIDReader pulseData { get; set; }
        private RFIDReader rfidReader;
        public event EventHandler<RFIDReaderEventArgs> RFIDValueEvent; 
    }
    
    abstract class StationControlSubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        


        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        protected void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
